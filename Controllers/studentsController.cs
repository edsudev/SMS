using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using static EDSU_SYSTEM.Models.Enum;
using JsonSerializer = System.Text.Json.JsonSerializer;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using CanvasApi.Client.Users.Models;

namespace EDSU_SYSTEM.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public StudentsController(UserManager<ApplicationUser> userManager, UserManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<IActionResult> studentRole()
        {
            try
            {
                var id = "3970d7df-4069-40ee-a778-57a181a8e2c5";
                var users = _userManager.Users.Where(x => x.Type == 4).ToList();
                var role = await _roleManager.FindByIdAsync(id);
                foreach (var item in users)
                {
                    await _userManager.AddToRoleAsync(item, role.Name);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        public async Task<IActionResult> UsersMigrate()
        {
            try
            {
                var users = (from s in _context.Staffs where s.SchoolEmail == "dev.edostateuniversity@gmail.com" select s).FirstOrDefault();
               
                    try
                    {
                    
                        var user = new ApplicationUser
                        {
                            Email = users.SchoolEmail,
                            UserName = users.SchoolEmail,
                            StaffId = users.Id,
                            PhoneNumber = users.Phone,
                            PhoneNumberConfirmed = true,
                            Type = 2,
                            EmailConfirmed = true
                        };
                        var r = await _userManager.CreateAsync(user, "Password@1");
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                
            }
            catch (Exception)
            {

                throw;
            }


            return View();
        }
        public async Task<IActionResult> WalletMigrate(ConversionMainWallet mainWallet)
        {
            var students = (from s in _context.ConversionStudents select s).ToList();
            foreach (var item in students)
            {
                try
                {
                    mainWallet.Id = item.Id;
                    //mainWallet.UTME = item.UTMENumber;
                    mainWallet.Name = item.Fullname;
                    mainWallet.CreditBalance = 0;
                    mainWallet.BulkDebitBalanace = 0;
                    mainWallet.Status = true;
                    mainWallet.WalletId = item.UTMENumber;
                    mainWallet.DateCreated = DateTime.Now;
                    _context.ConversionMainWallets.Add(mainWallet);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return View();
        }
        //For migrated Students
        public async Task<IActionResult> ActivateWallet(string? id, UgSubWallet myWallet)
        {
            var students = (from s in _context.Students select s).ToList();
            foreach (var item in students)
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive == true);
                myWallet.SessionId = session.Id;

                myWallet.Name = item.Fullname;
                myWallet.Pic = item.Picture;
                myWallet.RegNo = item.UTMENumber;
                myWallet.Level = item.Level ;
                myWallet.Department = item.Department;
                myWallet.CreditBalance = 0;
                myWallet.WalletId = item.UTMENumber;
                myWallet.DateCreated = DateTime.Now;
                myWallet.Status = true;
                _context.Add(myWallet);
                await _context.SaveChangesAsync();
                //////////////////////////////////

                //FirstOrDefaultAsync works when the id coming in is not of type int.
                //FindAsync works when the id coming in and the one being compared with is also int.

                if (myWallet.WalletId == null)
                {
                    return NotFound();
                }

                var WalletToUpdate = await _context.UgMainWallets
                .FirstOrDefaultAsync(c => c.WalletId == myWallet.WalletId);

                WalletToUpdate.BulkDebitBalanace = myWallet.Debit;
                WalletToUpdate.Status = true;
                WalletToUpdate.DateUpdated = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles ="student, superAdmin")]
        // GET: students
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            var student = (from c in _context.Students where c.Id == userId select c).Include(i => i.Departments).FirstOrDefault();
            var wallet = (from c in _context.UgMainWallets where c.UTME == student.UTMENumber select c).Include(i=>i.Applicants).ThenInclude(i => i.Departments).FirstOrDefault();
            ViewBag.Name = wallet.Name;
            ViewBag.Department = student.Departments.Name;
            var approvedCourses = (from c in _context.CourseRegistrations
                                   where c.StudentId == userId &&
                            c.Status == MainStatus.Approved &&
                            c.SessionId == student.CurrentSession
                                   select c).Include(c => c.Courses).ToList();
            var timetable = (from c in _context.TimeTables where c.DepartmetId == student.Department && c.LevelId ==
                             student.Level
                                   select c).Include(c => c.Courses).ThenInclude(s => s.Courses).ToList();
            
            //After getting the courses from coursereg and Scores from Results table, we sorted them using the course code before serializing them
            //so that the courses can align with the courses since they are coming from the same table.
            var grades = (from g in _context.Results where g.StudentId == student.MatNumber select g).ToList();
            var sortedCourses = approvedCourses.OrderBy(s => s.Courses.Code);
            var sortedGrades = grades.OrderBy(c => c.CourseId);

            var CourseCode = (from c in sortedCourses select c.Courses.Code).ToList();
            var TestScores = (from v in sortedGrades select v.CA).ToList();
            foreach (var item in CourseCode)
            {
                Console.WriteLine("This is it",item);
            }
           
            var json = JsonSerializer.Serialize(CourseCode);
            var json2 = JsonSerializer.Serialize(TestScores);


            ViewBag.courses = json;
            ViewBag.grade = json2;

            var model = new StudentDashboardVM
            {
                MainWallet = wallet,
                Courses = approvedCourses,
                TimeTables = timetable
            };

            return View(model);
           
        }
        public async Task<IActionResult> Allstudents()
        {
            ViewBag.err = TempData["err"];
            var applicationDbContext = _context.Students.Include(s => s.Departments)
                .Include(s => s.Levels);
            return View(await applicationDbContext.ToListAsync());
           
        }
        public async Task<IActionResult> Graduated()
        {
            ViewBag.err = TempData["err"];
            var applicationDbContext = _context.Students.Where(x => x.StudentStatus == 2).Include(s => s.Departments)
                .Include(s => s.Levels);
            return View(await applicationDbContext.ToListAsync());
           
        } 
        public async Task<IActionResult> Expelled()
        {
            ViewBag.err = TempData["err"];
            var applicationDbContext = _context.Students.Where(x => x.StudentStatus == 3).Include(s => s.Departments)
                .Include(s => s.Levels);
            return View(await applicationDbContext.ToListAsync());
           
        }

        public async Task<IActionResult> MyStudents()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;
            var HOD = (from i in _context.Staffs where i.Id == userId select i).FirstOrDefault();
            var applicationDbContext = _context.Students.Where(i => i.Department == HOD.DepartmentId).Include(s => s.Departments)
                .Include(s => s.Levels);
            return View(await applicationDbContext.ToListAsync());

        }
        public async Task<IActionResult> UpdateStudents()
        {
            return _context.Departments != null ?
                           View(await _context.Departments.ToListAsync()) :
                           Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }
        //This module updates students session and level 
        //and it is done based on department

        public async Task<IActionResult> update(string id, UgSubWallet myWallet)
        {
            Console.WriteLine("This is the department" + id);
            var dpt = (from d in _context.Departments where d.ShortCode == id select d.Id).FirstOrDefault();
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var students = (from s in _context.Students where s.Department == dpt select s).ToList();
                foreach (var item in students)
                {
                    //var StudentId = item.Id;
                    var Session = (from s in _context.Sessions where s.IsActive == true select s).FirstOrDefault();
                    
                        item.CurrentSession = Session.Id;
                        var level = item.Level + 1;
                        item.Level = level;
                        await _context.SaveChangesAsync();

                    if (item.Level == 1)
                    {
                        var TuitionFee = (from t in _context.Fees
                                          where t.DepartmentId == item.Department
                                          select t.Level1).Sum();
                        myWallet.Tuition = TuitionFee;
                    }
                    else if (item.Level == 2)
                    {
                        var TuitionFee = (from t in _context.Fees
                                          where t.DepartmentId == item.Department
                                          select t.Level2).Sum();
                        myWallet.Tuition = TuitionFee;
                    }
                    else if (item.Level == 3)
                    {
                        var TuitionFee = (from t in _context.Fees
                                          where t.DepartmentId == item.Department
                                          select t.Level3).Sum();
                        myWallet.Tuition = TuitionFee;
                    }
                    else if (item.Level == 4)
                    {
                        var TuitionFee = (from t in _context.Fees
                                          where t.DepartmentId == item.Department
                                          select t.Level4).Sum();
                        myWallet.Tuition = TuitionFee;
                    }
                    else if (item.Level == 5)
                    {
                        var TuitionFee = (from t in _context.Fees
                                          where t.DepartmentId == item.Department
                                          select t.Level5).Sum();
                        myWallet.Tuition = TuitionFee;
                    }
                    else if (item.Level == 6)
                    {
                        var TuitionFee = (from t in _context.Fees
                                          where t.DepartmentId == item.Department
                                          select t.Level6).Sum();
                        myWallet.Tuition = TuitionFee;
                    }
                    //myWallet.StudentId = item.Id;
                    myWallet.Name = item.Fullname;
                    myWallet.Department = item.Department;
                    myWallet.RegNo = item.UTMENumber;
                    myWallet.EDHIS = (decimal)20000.00;
                    myWallet.LMS = (decimal)25000.00;
                    myWallet.SRC = (decimal)2500.00;
                    myWallet.Debit = myWallet.Tuition + myWallet.Tuition2 + myWallet.LMS + myWallet.SRC;
                    myWallet.FortyPercent = myWallet.Tuition * 40 / 100;
                    myWallet.SixtyPercent = myWallet.Tuition * 60 / 100;
                    myWallet.CreditBalance = 0;
                    myWallet.WalletId = item.UTMENumber;
                    myWallet.DateCreated = DateTime.Now;
                    myWallet.Status = true;
                    myWallet.Level = item.Level;
                    myWallet.Pic = item.Picture;
                    myWallet.ApplicantId = item.Id;
                    //Next line updates the bulk wallet
                    //var bulkwallet = await _context.MainWallets.FirstOrDefaultAsync(i => i.WalletId == myWallet.WalletId);
                    //var newBulkDebit = bulkwallet.BulkDebitBalanace + myWallet.Debit;
                    //bulkwallet.BulkDebitBalanace = newBulkDebit;

                    _context.Add(myWallet);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Students");

            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();
        }
        //Graduate Student by changing their status
        public async Task<IActionResult> Graduate(string id, Student student)
        {
            try
            {
                var graduate = _context.Students.FirstOrDefault(x => x.SchoolEmailAddress == id);
                if (graduate == null)
                {
                    TempData["err"]= "Student Record not found!";
                    return RedirectToAction(nameof(Allstudents));
                }
                graduate.StudentStatus = 2;
                _context.Students.Update(graduate);
                await _context.SaveChangesAsync();
                return RedirectToAction("allstudents", "students");

            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "error");
                throw;
            }
        }
        //Graduate Student by changing their status
        public async Task<IActionResult> Return(string id, Student student)
        {
            try
            {
                var graduate = _context.Students.FirstOrDefault(x => x.SchoolEmailAddress == id);
                if (graduate == null)
                {
                    TempData["err"]= "Student Record not found!";
                    return RedirectToAction(nameof(Allstudents));
                }
                graduate.StudentStatus = 1;
                _context.Students.Update(graduate);
                await _context.SaveChangesAsync();
                return RedirectToAction("allstudents", "students");

            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "error");
                throw;
            }
        }
        [Authorize(Roles = "student, superAdmin")]
        // GET: students/Details/5
        public async Task<IActionResult> Profile()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;

            var student = (from s in _context.Students where s.Id == userId select s)    
                .Include(s => s.Departments)
                .Include(s => s.Faculties)
                .Include(s => s.LGAs)
                .Include(s => s.Levels)
                .Include(s => s.Nationalities)
                .Include(s => s.States)
                .Include(s => s.Sessions)
                .FirstOrDefault();
            //Console.WriteLine(student.Fullname);
            return View(student);
        } 
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Departments)
                .Include(s => s.Faculties)
                .Include(s => s.LGAs)
                .Include(s => s.Levels)
                .Include(s => s.Nationalities)
                .Include(s => s.Sessions)
                .Include(s => s.States)
                .FirstOrDefaultAsync(m => m.SchoolEmailAddress == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: students/Create
        public IActionResult Create()
        {
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id");
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id");
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id");
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id");
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Picture,Fullname,Sex,DOB,Religion,Phone,AltPhoneNumber,Email,NationalityId,StateOfOriginId,LGAId,PlaceOfBirth,PermanentHomeAddress,ContactAddress,MaritalStatus,ParentName,ParentOccupation,ParentPhone,ParentAltPhone,ParentEmail,ParentAddress,SchoolEmailAddress,UTMENumber,MatNumber,Faculty,Level,ModeOfAdmission,YearOfAdmission,Department,CurrentSession,CreatedAt,Cleared,ClearedBy")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", student.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", student.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", student.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", student.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", student.NationalityId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", student.CurrentSession);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", student.StateOfOriginId);
            return View(student);
        }

        // GET: students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = (from c in _context.Students where c.Id == id select c).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Name", student.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Name", student.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Name", student.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Name", student.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Name", student.NationalityId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Name", student.CurrentSession);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Name", student.StateOfOriginId);
            return PartialView("_editPartial",student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            
            if (id != student.Id)
            {
                return NotFound();
            }
            try
            {
                var studentToUpdate = await _context.Students
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Student>(student, "", 
                    c => c.Fullname, c => c.DOB, c => c.Sex, c => c.Religion, c => c.Phone, 
                    c => c.AltPhoneNumber, c => c.Email, c => c.NationalityId, c => c.StateOfOriginId, c => c.LGAId, 
                    c => c.PlaceOfBirth, c => c.ContactAddress,c => c.PermanentHomeAddress, c => c.MaritalStatus, c => c.ParentName,
                    c => c.ParentOccupation, c => c.ParentPhone, c => c.ParentAltPhone,c => c.ParentEmail, c => c.ParentAddress,
                    c => c.SchoolEmailAddress, c => c.UTMENumber, c => c.MatNumber, c => c.Faculty, c => c.Level, 
                    c => c.ModeOfAdmission, c => c.YearOfAdmission, c => c.Department, c => c.CurrentSession, c => c.IsStillAStudent,
                    c => c.ProgrameId, c => c.StudentStatus
                    ))
                {
                    try
                    {
                        student.Cleared = true;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction(nameof(Profile));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
             ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", student.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", student.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", student.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", student.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", student.NationalityId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", student.CurrentSession);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", student.StateOfOriginId);
            return RedirectToAction("profile");
        }
        public async Task<IActionResult> Upload(IFormFile passport, int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (passport != null && passport.Length > 0)
            {
                var uploadDir = @"files/applicantUploads/passports";
                var fileName = Path.GetFileNameWithoutExtension(passport.FileName);
                var extension = Path.GetExtension(passport.FileName);
                var webRootPath = _hostingEnvironment.WebRootPath;
                fileName = student.UTMENumber + extension;

                //fileName = fileName + extension;
                var path = Path.Combine(webRootPath, uploadDir, fileName);
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    passport.CopyTo(fs);
                    student.Picture = fileName;

                }
               
                try
                {
                   // await TryUpdateModelAsync<Student>(student);
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    return RedirectToAction("badreq", "error");
                }
            }
            return RedirectToAction(nameof(Profile));
        }

        // GET: students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Departments)
                .Include(s => s.Faculties)
                .Include(s => s.LGAs)
                .Include(s => s.Levels)
                .Include(s => s.Nationalities)
                .Include(s => s.Sessions)
                .Include(s => s.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }
        private bool StudentExists(int? id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
