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
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Controllers
{
    public class PgadmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public PgadmissionsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // GET: pgadmissions
        public async Task<IActionResult> Index()
        {
            var sessions = (from s in _context.Sessions select s).ToList();
            ViewBag.sessions = sessions;

            var applicationDbContext = _context.PgApplicants;
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Postgraduate()
        {
            return View();
        }
        public async Task<IActionResult> List(string? id)
        {
            ViewBag.currentSession = id;

            var sessions = (from s in _context.Sessions select s).ToList();
            ViewBag.sessions = sessions;

            var ListofApplicants = _context.PgApplicants.Where(i => i.YearOfAdmissions.Name == id).Include(i => i.YearOfAdmissions);
            return View(await ListofApplicants.ToListAsync());

        }
        public IActionResult Login()
        {
            return View();
        }
        // GET: pgadmissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PgApplicants == null)
            {
                return NotFound();
            }

            var pgApplicant = await _context.PgApplicants
                .FirstOrDefaultAsync(m => m.id == id);
            if (pgApplicant == null)
            {
                return NotFound();
            }

            return View(pgApplicant);
        }

        // GET: pgadmissions/Create
        public IActionResult Createprofile()
        {
            return View();
        }

        // POST: pgadmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PgApplicant pgApplicant)
        {
            if (ModelState.IsValid)
            {
                pgApplicant.Passport = "avatar.png";
                _context.Add(pgApplicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pgApplicant);
        }

        /// <summary>
        /// //Steps to complete for the application
        /// </summary>
        public async Task<IActionResult> Step1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicants = await _context.PgApplicants.FindAsync(id);
            if (applicants == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
            ViewData["LgaId"] = new SelectList(_context.Lgas, "Id", "Name");
            return View(applicants);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step1(int id)
        {
            var applicants = await _context.PgApplicants.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var ApplicatantToUpdate = await _context.PgApplicants
                .FirstOrDefaultAsync(c => c.id == id);

                if (await TryUpdateModelAsync<PgApplicant>(ApplicatantToUpdate, "", c => c.Surname, c => c.FirstName, c => c.OtherName,
                    c => c.Sex, c => c.DOB, c => c.MaritalStatus, c => c.PlaceOfBirth,
                    c => c.ContactAddress, c => c.PermanentHomeAddress, c => c.Nationality, c => c.StateOfOrigin, c => c.LGA,
                    c => c.PhoneNumber, c => c.Email, c => c.PrimarySchool, c => c.SecondarySchool))
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
                    return RedirectToAction("step2", "pgadmissions", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        //Step 2
        public async Task<IActionResult> Step2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicants = await _context.PgApplicants.FindAsync(id);
            if (applicants == null)
            {
                return NotFound();
            }
            ViewData["SSCESubjectId"] = new SelectList(_context.SsceSubjects, "Id", "SubjectName");
            ViewData["SSCEGradeId"] = new SelectList(_context.SSCEGrades, "Id", "Grade");
            return View(applicants);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ste2(int id)
        {
            var applicants = await _context.PgApplicants.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
          
            try
            {
                var ApplicatantToUpdate = await _context.PgApplicants
                .FirstOrDefaultAsync(c => c.id == id);

                if (await TryUpdateModelAsync<PgApplicant>(ApplicatantToUpdate, "", c => c.NoOfSittings, c => c.Ssce1Type,
                    c => c.Ssce1Year, c => c.Ssce1Number, c => c.Ssce1Subject1, c => c.Ssce1Subject1Grade, c => c.Ssce1Subject2,
                    c => c.Ssce1Subject2Grade, c => c.Ssce1Subject3, c => c.Ssce1Subject3Grade, c => c.Ssce1Subject4,
                    c => c.Ssce1Subject4Grade, c => c.Ssce1Subject5, c => c.Ssce1Subject5Grade, c => c.Ssce1Subject6,
                    c => c.Ssce1Subject6Grade, c => c.Ssce1Subject7, c => c.Ssce1Subject7Grade, c => c.Ssce1Subject8,
                    c => c.Ssce1Subject8Grade, c => c.Ssce1Subject9, c => c.Ssce1Subject9Grade, c => c.Ssce2Type,
                    c => c.Ssce2Year, c => c.Ssce2Number, c => c.Ssce2Subject1, c => c.Ssce2Subject1Grade, c => c.Ssce2Subject2,
                    c => c.Ssce2Subject2Grade, c => c.Ssce2Subject3, c => c.Ssce2Subject3Grade, c => c.Ssce2Subject4,
                    c => c.Ssce2Subject4Grade, c => c.Ssce2Subject5, c => c.Ssce2Subject5Grade, c => c.Ssce2Subject6,
                    c => c.Ssce2Subject6Grade, c => c.Ssce2Subject7, c => c.Ssce2Subject7Grade, c => c.Ssce2Subject8,
                    c => c.Ssce2Subject8Grade, c => c.Ssce2Subject9, c => c.Ssce2Subject9Grade))
                {
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
                    return RedirectToAction("Step4", "Applicants", new { id });
                }



                //Context.Update(applicant);
                //await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        // GET: Applicants/Edit/Parent Guardian Information Page
        public async Task<IActionResult> Step3(int? id)
        {
            if (id == null || _context.PgApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.PgApplicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        // POST: Applicants1/Edit/Parent Guardian Information Page
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step3(int id)
        {
            var applicants = await _context.PgApplicants.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            try
            {

                var ApplicatantToUpdate = await _context.PgApplicants
                .FirstOrDefaultAsync(c => c.id == id);

                if (await TryUpdateModelAsync<PgApplicant>(ApplicatantToUpdate, "", c => c.FullNameOfNextOfKin, c => c.AddressOfNextOfKin,
                    c => c.PhoneNumber, c => c.AlternatePhoneNumberOfNextOfKin, c => c.EmailOfNextOfKin, c => c.OccupationOfNextOfKin))
                {
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
                    return RedirectToAction("Step4", "PgApplicants", new { id });
                }

                //Context.Update(applicant);
                //await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }

        // GET: Applicants/Edit/File Upload Page
        public async Task<IActionResult> Step4(int? id)
        {
            if (id == null || _context.PgApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.PgApplicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step4(int id, IFormFile passport, IFormFile higherdegree, IFormFile ssce, IFormFile birthcertificate, IFormFile lga, IFormFile nysc, IFormFile ref1, IFormFile ref2, IFormFile ref3, PgApplicant model)
        {
            var applicants = await _context.PgApplicants.FindAsync(id);

            // var applicants = await _context.PgApplicants.FindAsync(model.id);
            if (model.id == null)
            {
                return NotFound();
            }

            try
            {

                if (passport != null && passport.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(passport.FileName);
                    var extension = Path.GetExtension(passport.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        passport.CopyTo(fs);
                        applicants.Passport = fileName;

                    }
                }
                if (higherdegree != null && higherdegree.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(higherdegree.FileName);
                    var extension = Path.GetExtension(higherdegree.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        higherdegree.CopyTo(fs);
                        applicants.HigherDegrees = fileName;

                    }
                }
                if (ssce != null && ssce.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(ssce.FileName);
                    var extension = Path.GetExtension(ssce.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        ssce.CopyTo(fs);
                        applicants.Ssce1 = fileName;

                    }
                }
                if (birthcertificate != null && birthcertificate.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(birthcertificate.FileName);
                    var extension = Path.GetExtension(birthcertificate.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        birthcertificate.CopyTo(fs);
                        applicants.BirthCertificate = fileName;

                    }
                }
                if (lga != null && lga.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(lga.FileName);
                    var extension = Path.GetExtension(lga.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        lga.CopyTo(fs);
                        applicants.LGAUpload = fileName;

                    }
                }
                if (nysc != null && nysc.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(nysc.FileName);
                    var extension = Path.GetExtension(nysc.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        nysc.CopyTo(fs);
                        applicants.NYSC = fileName;

                    }
                }
                if (ref1 != null && ref1.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(ref1.FileName);
                    var extension = Path.GetExtension(ref1.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        ref1.CopyTo(fs);
                        applicants.Ref1 = fileName;

                    }
                }if (ref2 != null && ref2.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(ref2.FileName);
                    var extension = Path.GetExtension(ref2.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        ref2.CopyTo(fs);
                        applicants.Ref2 = fileName;

                    }
                }
                if (ref3 != null && ref3.Length > 0)
                {
                    var uploadDir = @"files/PgApplicants/passport";
                    var fileName = Path.GetFileNameWithoutExtension(ref3.FileName);
                    var extension = Path.GetExtension(ref3.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        ref3.CopyTo(fs);
                        applicants.Ref3 = fileName;

                    }
                }
                await TryUpdateModelAsync<PgApplicant>(applicants);    
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
            ViewBag.ID = id;
            //  return View();
            return RedirectToAction("Step4", "pgadmissions", new { model.id });

        }
        // Admit PG Applicant
        public async Task<IActionResult> Admit(int? id)
        {
            var applicants = _context.PgApplicants.Where(x => x.id == id).FirstOrDefault();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            return PartialView("_admissionPartial", applicants);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Admit(int id, PgStudent student, Session session)
        {
            try
            {
                var applicant = await _context.PgApplicants.FindAsync(id);
                var Session = (from T in _context.Sessions
                               where T.IsActive == true
                               select T).ToList();
                var dept = (from g in _context.Departments where g.Id == applicant.AdmittedInto select g).ToList();
                //Moving Applicant to student table
                student.Fullname = applicant.Surname + " " + applicant.FirstName + " " + applicant.OtherName;
                student.Picture = applicant.Passport;
                student.Sex = applicant.Sex;
                student.DOB = applicant.DOB;
                student.Sex = applicant.Sex;
                student.Phone = applicant.PhoneNumber;
                student.Email = applicant.Email;
                student.NationalityId = applicant.Nationality;
                student.StateOfOriginId = applicant.StateOfOrigin;
                student.LGAId = applicant.LGA;
                student.PlaceOfBirth = applicant.PlaceOfBirth;
                student.PermanentHomeAddress = applicant.PermanentHomeAddress;
                student.ContactAddress = applicant.ContactAddress;
                student.MaritalStatus = applicant.MaritalStatus;
               
                foreach (var item in Session)
                {
                    student.SchoolEmailAddress = applicant.Surname + item.suffix + "." + applicant.FirstName + "@edouniversity.edu.ng";
                    student.YearOfAdmission = item.Id;
                    student.CurrentSession = item.Id;
                }
                foreach (var item in dept)
                {
                    student.Department = item.Id;
                }
                
                student.Level = applicant.LevelAdmittedTo;
               // student.ModeOfAdmission = applicant.m;
                student.Cleared = true;
                student.CreatedAt = DateTime.Now;
                _context.PostGraduateStudents.Add(student);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    Email = student.SchoolEmailAddress,
                    UserName = student.SchoolEmailAddress,
                    StudentsId = student.Id,
                    EmailConfirmed = true
                };
                var r = await userManager.CreateAsync(user, "Password@1");
                if (r.Succeeded)
                {
                    Console.WriteLine("It worked");
                }
                Console.WriteLine("E no work");
                return RedirectToAction("index", "admissions");
            }
            catch (DbUpdateConcurrencyException)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return View("Error");
        }
        public async Task<IActionResult> Reject(int? id)
        {
            var applicants = _context.PgApplicants.Where(x => x.id == id).FirstOrDefault();
            return PartialView("_reject", applicants);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int? id, int a)
        {
            var applicants = await _context.PgApplicants.FindAsync(id);


            try
            {
                var ApplicatantToUpdate = await _context.PgApplicants
                .FirstOrDefaultAsync(c => c.id == id);

                if (await TryUpdateModelAsync<PgApplicant>(ApplicatantToUpdate, "", c => c.id))
                {
                    applicants.Status = MainStatus.Declined;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "pgadmissions");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("Index", "pgadmissions");

        }
        public async Task<IActionResult> Cancel(int? id)
        {
            var applicants = _context.PgApplicants.Where(x => x.id == id).FirstOrDefault();
            return PartialView("_cancel", applicants);
        }
        // GET: pgadmissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PgApplicants == null)
            {
                return NotFound();
            }

            var pgApplicant = await _context.PgApplicants.FindAsync(id);
            if (pgApplicant == null)
            {
                return NotFound();
            }
            return View(pgApplicant);
        }

        // POST: pgadmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,UserId,Surname,FirstName,OtherName,Sex,DOB,MaritalStatus,PlaceOfBirth,ContactAddress,PermanentHomeAddress,Nationality,StateOfOrigin,LGA,PhoneNumber,PhoneNumber2,Email,PrimarySchool,SecondarySchool,ProgramApplyingFor,PreviousInstitution,CurrentQualification,ClassOfDegree,YearGraduated,PercentageOfResult,AreaOfSpecialization,EmploymentHistory,ResearchExperience,Status,NoOfSittings,Ssce1Type,Ssce1Year,Ssce1Number,Ssce1Subject1,Ssce1Subject2,Ssce1Subject3,Ssce1Subject4,Ssce1Subject5,Ssce1Subject6,Ssce1Subject7,Ssce1Subject8,Ssce1Subject9,Ssce1Subject1Grade,Ssce1Subject2Grade,Ssce1Subject3Grade,Ssce1Subject4Grade,Ssce1Subject5Grade,Ssce1Subject6Grade,Ssce1Subject7Grade,Ssce1Subject8Grade,Ssce1Subject9Grade,Ssce2Type,Ssce2Year,Ssce2Number,Ssce2Subject1,Ssce2Subject2,Ssce2Subject3,Ssce2Subject4,Ssce2Subject5,Ssce2Subject6,Ssce2Subject7,Ssce2Subject8,Ssce2Subject9,Ssce2Subject1Grade,Ssce2Subject2Grade,Ssce2Subject3Grade,Ssce2Subject4Grade,Ssce2Subject5Grade,Ssce2Subject6Grade,Ssce2Subject7Grade,Ssce2Subject8Grade,Ssce2Subject9Grade,FullNameOfNextOfKin,AddressOfNextOfKin,PhoneNumberOfNextOfKin,AlternatePhoneNumberOfNextOfKin,EmailOfNextOfKin,OccupationOfNextOfKin,Passport,HigherDegrees,Ssce1,Ssce2,BirthCertificate,LGAUpload,NYSC,Ref1,Ref2,Ref3")] PgApplicant pgApplicant)
        {
            if (id != pgApplicant.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pgApplicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PgApplicantExists(pgApplicant.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pgApplicant);
        }

        // GET: pgadmissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PgApplicants == null)
            {
                return NotFound();
            }

            var pgApplicant = await _context.PgApplicants
                .FirstOrDefaultAsync(m => m.id == id);
            if (pgApplicant == null)
            {
                return NotFound();
            }

            return PartialView("_delete",pgApplicant);
        }

        // POST: pgadmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PgApplicants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PgApplicants'  is null.");
            }
            var pgApplicant = await _context.PgApplicants.FindAsync(id);
            if (pgApplicant != null)
            {
                _context.PgApplicants.Remove(pgApplicant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Download Files
        public FileResult DownloadFile(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/passport/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadBirthCertificate(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/birthCertificate/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadDegree(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/degree/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadLga(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/lga/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadNYSC(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/NYSC/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadRef1(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/ref1/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadRef2(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/ref2/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadRef3(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/ref3/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadSsce1(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/ssce1/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadSsce2(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "PgApplicants/ssce2/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }

        private bool PgApplicantExists(int? id)
        {
          return _context.PgApplicants.Any(e => e.id == id);
        }
    }
}
