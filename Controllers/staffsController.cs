using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.Authorization;

namespace EDSU_SYSTEM.Controllers
{
    public class StaffsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public StaffsController(UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "staff, superAdmin")]
        // GET: staffs
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;
            //Checks works table and get where status == pending or in progress   
            var worksPending = (from c in _context.Works where c.Status == Models.Enum.WorksStatus.Pending select c).ToList();
            var worksInProgress = (from c in _context.Works where c.Status == Models.Enum.WorksStatus.In_Progress select c).ToList();
            ViewBag.worksPending = worksPending.Count();
            ViewBag.worksInProgress = worksInProgress.Count();
            
            var ICTPending = (from c in _context.IctComplaints where c.Status == Models.Enum.WorksStatus.Pending select c).ToList();
            var ICTInProgress = (from c in _context.IctComplaints where c.Status == Models.Enum.WorksStatus.In_Progress select c).ToList();
            ViewBag.ictPending = ICTPending.Count();
            ViewBag.ictInProgress = ICTInProgress.Count();

            var ChiefPortalpendingReq = (from s in _context.Exeats where s.ChiefPortalStatus == Models.Enum.ChiefPortalStatus.Pending select s).ToList();
            var HallMasterPendingReq = (from s in _context.Exeats where s.HallMasterStatus == Models.Enum.MainStatus.Pending select s).ToList();
            ViewBag.portalPending = ChiefPortalpendingReq.Count();
            ViewBag.HMPending = HallMasterPendingReq.Count();

            var LevelAdviser = (from l in _context.LevelAdvisers where l.StaffId == userId select l).ToList();
            //Console.Write(LevelAdviser.Count());
            foreach (var item in LevelAdviser)
            {
                var pendingCourseReg = (from s in _context.CourseRegistrations where s.Students.Level == item.LevelId && s.Status == Models.Enum.MainStatus.Pending select s).ToList();
                ViewBag.pendingCourseReg = pendingCourseReg.Count();
                Console.Write(item.StaffId);
            }

            
            //Staff Evaluation
            var staffCourses = (from s in _context.CourseAllocations where s.LecturerId == userId select s).Include(i => i.Courses).ToList();
            var json22 = JsonSerializer.Serialize(staffCourses);
            Console.Write(json22);

            var sortedCourses = staffCourses.OrderBy(x => x.Courses.Code);
            var courses = (from c in sortedCourses select c.Courses.Code).ToList();
            var json = JsonSerializer.Serialize(courses);
            ViewBag.courses = json;

            Console.Write(ViewBag.courses);

            var totalGradesList = new List<int>();
            var StaffEvaluations = _context.Evaluations.Where(x => x.LecturerId == userId).ToList();
            var sortedEvaluations = StaffEvaluations.OrderBy(x => x.CourseId);
            var evaluations = (from s in sortedEvaluations select s).ToList();
            foreach (var evaluation in evaluations)
            {
                // After suming all grades, we go ahead to convert them to percentage
                var totalSum = new[] { evaluation.Grade1, evaluation.Grade2, evaluation.Grade3, evaluation.Grade4, evaluation.Grade5, 
                    evaluation.Grade6, evaluation.Grade7, evaluation.Grade8, evaluation.Grade9, evaluation.Grade10,
                    evaluation.Grade11, evaluation.Grade12, evaluation.Grade13, evaluation.Grade14, evaluation.Grade15, 
                    evaluation.Grade16, evaluation.Grade17, evaluation.Grade18, evaluation.Grade19, evaluation.Grade20 }.Sum();
                var result = totalSum * 20 / 100;
                totalGradesList.Add((int)result);
            }
            var json2 = JsonConvert.SerializeObject(totalGradesList);
            Console.WriteLine(json2);
            ViewBag.Percentage = json2;


            //Results not found!....
            ViewBag.Results = TempData["NoCourses"];
            return View();
        }
        public async Task<IActionResult> AllStaffs()
        {
            var applicationDbContext = _context.Staffs.Include(i => i.Departments).Include(i => i.Positions);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> Stats()
        {
            var male = (from c in _context.Students where c.Sex == "M" select c).ToList();
            var female = (from c in _context.Students where c.Sex == "F" select c).ToList();
            ViewBag.MaleStudents = male.Count();
            ViewBag.FemaleStudents = female.Count();

            //Staffs
            var maleStaff = (from c in _context.Staffs where c.Sex == "M" select c).ToList();
            var femaleStaff = (from c in _context.Staffs where c.Sex == "F" select c).ToList();
            ViewBag.MaleStaff = maleStaff.Count();
            ViewBag.FemaleStaff = femaleStaff.Count();

            //Staffs
            var AcademicStaff = (from c in _context.Staffs where c.Type == 5 select c).ToList();
            var NonAcademicStaff = (from c in _context.Staffs where c.Type == 6 select c).ToList();
            ViewBag.AcadStaff = AcademicStaff.Count();
            ViewBag.NonAcadStaff = NonAcademicStaff.Count();

            var departments = (from c in _context.Departments select c).ToList();
            var deptStudentsCounts = new Dictionary<string, int>();
            foreach (var dept in departments)
            {
                var deptStudents = (from c in _context.Students where c.Department == dept.Id select c).ToList();
                deptStudentsCounts[dept.Name] = deptStudents.Count();
            }
            ViewBag.DeptStudentsCounts = deptStudentsCounts;

            return View();
        }
        [Authorize(Roles = "staff, superAdmin")]
        // GET: students/Details/5
        public async Task<IActionResult> Profile()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;

            var staff = (from s in _context.Staffs where s.Id == userId select s)
                .Include(s => s.Departments)
                .Include(s => s.Faculties)
                .Include(s => s.LGAs)
                .Include(s => s.Nationalities)
                .Include(s => s.States)
                .FirstOrDefault();
            
            return View(staff);
        }
       
        // GET: staffs/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(s => s.Departments)
                .Include(s => s.Faculties)
                .Include(s => s.LGAs)
                .Include(s => s.Nationalities)
                .Include(s => s.States)
                .FirstOrDefaultAsync(m => m.SchoolEmail == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }
        // Staff Directory/Details
        public IActionResult Directory(string id)
        {
            var staff = _context.Staffs.Include(x => x.Positions).FirstOrDefault(x => x.SchoolEmail == id);
            return View(staff);
        }
        // GET: staffs/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Name");
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
            return View();
        }

        // POST: staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http:/sta/go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Position,FacultyId,DepartmentId,FirstName,LastName,OtherName,Email,SchoolEmail,DOB,Sex,NationalityId,StateId,LGAId,Phone,ContactAddress,HighestQualification,FieldOfStudy,AreaOfSpecialization,WorkedInHigherInstuition,CurrentPlaceOfWork,PositionAtCurrentPlaceOfWork,YearsOfExperience,CertUpload,CVUpload,Picture,CreatedAt,UpdatedAt,IsEmployed,EmployedBy,ORCID,ResearcherId,GoogleScholar,ResearchGate,Academia,LinkedIn,Mendeley,Scopus")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", staff.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", staff.FacultyId);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", staff.LGAId);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", staff.NationalityId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", staff.StateId);
            return View(staff);
        }

        // GET: staffs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", staff.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", staff.FacultyId);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Name", staff.LGAId);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Name", staff.NationalityId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", staff.StateId);
            return PartialView("_editpartial",staff);
        }

        // POST: staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "staff, superAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Staff staff)
        {
            var applicants = await _context.Staffs.FindAsync(id);
            try
            {
                if (await TryUpdateModelAsync<Staff>(applicants, "", 
                    c => c.Picture, c => c.Surname, c => c.FirstName, c => c.MiddleName, c => c.DOB, c => c.Sex, 
                    c => c.Phone, c => c.Email, c => c.SchoolEmail,
                    c => c.ContactAddress, c => c.NationalityId, c => c.StateId, 
                    c => c.LGAId,c => c.Type, c => c.FacultyId, c => c.DepartmentId, 
                    c => c.HighestQualification, c => c.FieldOfStudy, c => c.AreaOfSpecialization,
                    c => c.WorkedInHigherInstuition, c => c.CurrentPlaceOfWork, 
                    c => c.PositionAtCurrentPlaceOfWork, c => c.YearsOfExperience, c => c.ORCID,
                    c => c.ResearcherId, c => c.GoogleScholar, c => c.ResearchGate, c => c.Academia, c => c.LinkedIn,
                    c => c.Mendeley))
                   
                    {
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StaffExists(staff.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("profile", "staffs");
                }
            }
            catch (Exception ex)
            {
               
                ex.ToString();
                return RedirectToAction("badreq", "error");
            }
            return RedirectToAction("profile", "staffs");

        }
        [Authorize(Roles = "staff, superAdmin")]
        public async Task<IActionResult> Upload(IFormFile passport, int id)
        {
            var staffData = await _context.Staffs.FindAsync(id);
            if (passport != null && passport.Length > 0)
            {
                var uploadDir = @"files/vacancyUploads/passport";
                var fileName = Path.GetFileNameWithoutExtension(passport.FileName);
                var extension = Path.GetExtension(passport.FileName);
                var webRootPath = _hostingEnvironment.WebRootPath;
                fileName = staffData.SchoolEmail + extension;

                var path = Path.Combine(webRootPath, uploadDir, fileName);
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    passport.CopyTo(fs);
                    staffData.Picture = fileName;

                }

                try
                {
                    _context.Update(staffData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    return RedirectToAction("badreq", "error");
                }
            }
            return RedirectToAction(nameof(Profile));
        }

        // GET: staffs/Delete/5
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(s => s.Departments)
                .Include(s => s.Faculties)
                .Include(s => s.LGAs)
                .Include(s => s.Nationalities)
                .Include(s => s.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }
        [Authorize(Roles = "superAdmin")]
        // POST: staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Staffs'  is null.");
            }
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        /// /////////////////////////
        public async Task<IActionResult> Publication(int? id)
        {

            //var staff = _context.Staffs.Where(x => x.Id == id).FirstOrDefault();
            var staff = _context.Staffs.Where(x => x.Id == id).FirstOrDefault();

            return PartialView("_publicationPartial", staff);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publication(int id)
        {


            if (id == null)
            {
                return NotFound();
            }

            try
            {

                var StaffToUpdate = await _context.Staffs
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Staff>(StaffToUpdate, "", c => c.ORCID, c => c.ResearcherId, c => c.GoogleScholar,
                    c => c.ResearchGate, c => c.Academia, c => c.LinkedIn, c => c.Mendeley,
                    c => c.Scopus))
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction("Index", "Staffs");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        [Authorize(Roles = "staff, superAdmin")]
        public async Task<IActionResult> Documents(int? id)
        {

            //var staff = _context.Staffs.Where(x => x.Id == id).FirstOrDefault();
            var staff = _context.Staffs.Where(x => x.Id == id).FirstOrDefault();

            return PartialView("_documents", staff);
        }
        [Authorize(Roles = "staff, superAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Documents(IFormFile passport, IFormFile cv, IFormFile certificate, Staff model)
        {
            var staff = await _context.Staffs.FindAsync(model.Id);

            if (model.Id == null)
            {
                return NotFound();
            }
            try
            {
                if (passport != null && passport.Length > 0)
                {
                    var uploadDir = @"files/staffs/passport";
                    var fileName = Path.GetFileNameWithoutExtension(passport.FileName);
                    var extension = Path.GetExtension(passport.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = staff.SchoolEmail + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        passport.CopyTo(fs);
                        staff.Picture = fileName;

                    }
                }
                if (cv != null && cv.Length > 0)
                {
                    var uploadDir = @"files/staff/cv";
                    var fileName = Path.GetFileNameWithoutExtension(cv.FileName);
                    var extension = Path.GetExtension(cv.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        cv.CopyTo(fs);
                        staff.CVUpload = fileName;

                    }
                }
                if (certificate != null && certificate.Length > 0)
                {
                    var uploadDir = @"files/staff/certificate";
                    var fileName = Path.GetFileNameWithoutExtension(certificate.FileName);
                    var extension = Path.GetExtension(certificate.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        certificate.CopyTo(fs);
                        staff.CertUpload = fileName;

                    }

                }
                await TryUpdateModelAsync<Staff>(staff);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            //  return View();
            return RedirectToAction("Profile", "Staffs", new { model.Id });

        }
       
        private bool StaffExists(int? id)
        {
          return _context.Staffs.Any(e => e.Id == id);
        }
    }
}
