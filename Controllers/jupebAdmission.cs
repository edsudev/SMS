using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using EDSU_SYSTEM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using static EDSU_SYSTEM.Models.Enum;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Xml;

namespace EDSU_SYSTEM.Controllers
{
    public class JupebAdmission : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public JupebAdmission(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment,RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
       
       
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        // GET: admissions
        public async Task<IActionResult> Index()
        {
            var sessions = (from s in _context.Sessions select s).ToList();
            ViewBag.sessions = sessions;
            
            var applicationDbContext = _context.JupebApplicants;
            return View(await applicationDbContext.ToListAsync());
         
        } 
        public async Task<IActionResult> Jupeb()
        {
            ViewBag.err = TempData["err"];
            return View();
         
        }
       
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        public async Task<IActionResult> List(string? id)
        {
            ViewBag.currentSession = id;

            var sessions = (from s in _context.Sessions select s).ToList();
            ViewBag.sessions = sessions;
            
            var ListofApplicants = _context.JupebApplicants.Where(i => i.YearOfAdmissions.Name == id).Include(i => i.YearOfAdmissions);
            return View(await ListofApplicants.ToListAsync());
     
        }
        public IActionResult Instructions()
        {
            return View();
        } 
        public IActionResult Requirements()
        {
            return View();
        }
        public IActionResult FeesDetails()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, Applicant applicant)
        {
            var user = (from s in _context.JupebApplicants where s.Email == email && s.Password == password select s).FirstOrDefault();
            
            if (user == null)
            {
                
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var id = user.ApplicantId;
                return RedirectToAction("step1", "admissions", new {id});
            }
            //return RedirectToAction(nameof(Dashboard));
        }
        // GET: admissions/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants.Include(x => x.Nationalities).Include(x => x.States).Include(x => x.LGAs)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: admissions/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: admissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Applicant applicant, string pword, string cpword)
        {

            if (pword != cpword || (pword == null))
            {
                TempData["err"] = "Something went wrong, make sure you provide data as required!";
                //return RedirectToAction(nameof(Undergraduate));
            }
            applicant.Password = pword;
            applicant.ApplicantId =Guid.NewGuid().ToString() + DateTime.Now.Millisecond;
            var id = applicant.ApplicantId;
            _context.Add(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction("step1", "admissions", new { id });

            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", applicant.LGAId);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", applicant.NationalityId);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", applicant.StateOfOriginId);
            return View(applicant);
        }
        public async Task<IActionResult> Step1(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }
            
            ViewData["Country"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["State"] = new SelectList(_context.States, "Id", "Name");
            ViewData["Lga"] = new SelectList(_context.Lgas, "Id", "Name");

            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step1(string id, Applicant applicant)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantToUpdate = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);

            if (applicantToUpdate == null)
            {
                return NotFound();
            }

           
                applicantToUpdate.Surname = applicant.Surname;
                applicantToUpdate.FirstName = applicant.FirstName;
                applicantToUpdate.OtherName = applicant.OtherName;
                applicantToUpdate.Sex = applicant.Sex;
                applicantToUpdate.DOB = applicant.DOB;
                applicantToUpdate.MaritalStatus = applicant.MaritalStatus;
                applicantToUpdate.PlaceOfBirth = applicant.PlaceOfBirth;
                applicantToUpdate.ContactAddress = applicant.ContactAddress;
                applicantToUpdate.PermanentHomeAddress = applicant.PermanentHomeAddress;
                applicantToUpdate.NationalityId = applicant.NationalityId;
                applicantToUpdate.StateOfOriginId = applicant.StateOfOriginId;
                applicantToUpdate.LGAId = applicant.LGAId;
                applicantToUpdate.PhoneNumber = applicant.PhoneNumber;
                applicantToUpdate.Email = applicant.Email;
                applicantToUpdate.PrimarySchool = applicant.PrimarySchool;
                applicantToUpdate.SecondarySchool = applicant.SecondarySchool;
                applicantToUpdate.Indigine = applicant.Indigine;
                applicantToUpdate.ModeOfEntry = applicant.ModeOfEntry;

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

                return RedirectToAction("Step2", "admissions", new { id });
            
        }
       
        public async Task<IActionResult> Step2(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }


            List<SsceSubjects> Ssce = new();
            Ssce = (from c in _context.SsceSubjects select c).ToList();
            ViewBag.message3 = Ssce;
            List<SSCEGrade> grade = new();
            grade = (from c in _context.SSCEGrades select c).ToList();
            ViewBag.message4 = grade;
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step2(string id, int a)
        {
            var applicants = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);


            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var ApplicatantToUpdate = await _context.JupebApplicants
                .FirstOrDefaultAsync(c => c.ApplicantId == id);

                if (await TryUpdateModelAsync<Jupeb>(ApplicatantToUpdate, "", c => c.NoOfSittings, c => c.Ssce1Type,
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
                    return RedirectToAction("step4", "admissions", new { id });
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
        public async Task<IActionResult> Step3(string? id)
        {
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);
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
        public async Task<IActionResult> Step3(string id, int a)
        {
            var applicants = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
               
                var ApplicatantToUpdate = await _context.JupebApplicants
                .FirstOrDefaultAsync(c => c.ApplicantId == id);

                if (await TryUpdateModelAsync<Jupeb>(ApplicatantToUpdate, "", c => c.ParentFullName, c => c.ParentAddress,
                    c => c.ParentPhoneNumber, c => c.ParentAlternatePhoneNumber, c => c.ParentEmail, c => c.ParentOccupation))
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
                    return RedirectToAction("step5", "admissions", new { id });
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
        public async Task<IActionResult> Step4(string? id)
        {
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step4(IFormFile passport, IFormFile jamb, IFormFile ssce1, IFormFile birtCertificate, IFormFile directEntryUpload, IFormFile lga, string ApplicantId)
        {
            var applicants = await _context.JupebApplicants.FirstOrDefaultAsync(i => i.ApplicantId == ApplicantId);

            if (ApplicantId == null)
            {
                return NotFound();
            }
            try
            {
                if (passport != null && passport.Length > 0)
                {
                    var uploadDir = @"files/applicantUploads/passports";
                    var fileName = Path.GetFileNameWithoutExtension(passport.FileName);
                    var extension = Path.GetExtension(passport.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = applicants.Email + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        passport.CopyTo(fs);
                        applicants.PassportUpload = fileName;

                    }
                }
                
                if (ssce1 != null && ssce1.Length > 0)
                {
                    var uploadDir = @"files/applicantUploads/ssce";
                    var fileName = Path.GetFileNameWithoutExtension(ssce1.FileName);
                    var extension = Path.GetExtension(ssce1.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    fileName = applicants.Email + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        ssce1.CopyTo(fs);
                        applicants.Ssce1 = fileName;

                    }

                }
                if (birtCertificate != null && birtCertificate.Length > 0)
                {
                    var uploadDir = @"files/applicantUploads/birthcertificates";
                    var fileName = Path.GetFileNameWithoutExtension(birtCertificate.FileName);
                    var extension = Path.GetExtension(birtCertificate.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    fileName = applicants.Email + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        birtCertificate.CopyTo(fs);
                        applicants.BirthCertUpload = fileName;

                    }
                }
                if (directEntryUpload != null && directEntryUpload.Length > 0)
                {
                    var uploadDir = @"files/applicantUploads/directentry";
                    var fileName = Path.GetFileNameWithoutExtension(directEntryUpload.FileName);
                    var extension = Path.GetExtension(directEntryUpload.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    fileName = applicants.Email + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        directEntryUpload.CopyTo(fs);
                        applicants.DirectEntryUpload = fileName;

                    }

                }
                if (lga != null && lga.Length > 0)
                {
                    var uploadDir = @"files/applicantUploads/lga";
                    var fileName = Path.GetFileNameWithoutExtension(lga.FileName);
                    var extension = Path.GetExtension(lga.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    fileName = applicants.Email + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        lga.CopyTo(fs);
                        applicants.LGAUpload = fileName;

                    }
                }
                await TryUpdateModelAsync<Jupeb>(applicants);

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
            return RedirectToAction("step5", "admissions", new { ApplicantId });

        }

        public async Task<IActionResult> Eligibility(string? id)
        {
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }
            var applicant = await _context.JupebApplicants.Include(i => i.Nationalities).Include(i => i.States).Include(i => i.LGAs).FirstOrDefaultAsync(i => i.ApplicantId == id);

            if (applicant == null)
            {
                return NotFound();
            }
            try
            {
                
                return View(applicant);
            }
            catch (Exception)
            {
                return RedirectToAction("badreq","error");
                throw;
            }
            
        } 
        public async Task<IActionResult> Summary(string? id)
        {
            
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }
            var applicant = await _context.JupebApplicants.Include(i => i.Nationalities).Include(i => i.States).Include(i => i.LGAs).FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (applicant == null)
            {
                ViewBag.err = "Applicant with this ID does not exist";
                return RedirectToAction("badreq", "error");
            }
            if (applicant.Paid == true)
            {
                return View(applicant);
            }
            TempData["err"] = "Make sure to have paid your application fee before attempting to access this resource.";
            return RedirectToAction("badreq", "error");

        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        public async Task<IActionResult> Cancel(int? id)
        {
            var applicants = _context.JupebApplicants.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_cancel", applicants);
        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        public async Task<IActionResult> Reject(int? id)
        {
            var applicants = _context.JupebApplicants.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_reject", applicants);
        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int? id, int a)
        {
            var applicants = await _context.JupebApplicants.FindAsync( id);

           
            try
            {
                var ApplicatantToUpdate = await _context.JupebApplicants
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Jupeb>(ApplicatantToUpdate, "", c => c.Id))
                {
                    applicants.Status = MainStatus.Declined;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "admissions");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("Index", "admissions");

        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        public async Task<IActionResult> Admit(int? id)
        {
            var applicants = _context.JupebApplicants.Where(x => x.Id == id).FirstOrDefault();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "NameOfProgram");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            return PartialView("_admissionPartial", applicants);
        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Admit(int? id, UgMainWallet allwallet)
        {
            var applicants = await _context.JupebApplicants.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var ApplicatantToUpdate = await _context.JupebApplicants
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Jupeb>(ApplicatantToUpdate, "", c => c.ProgrameId, c => c.AdmittedInto, c => c.LevelAdmittedTo))
                {
                    var activeSession = (from s in _context.Sessions where s.IsActive == true select s).ToList();
                    
                    applicants.Status = MainStatus.Approved;
                    applicants.Screened = true;
                    foreach (var item in activeSession)
                    {
                        applicants.YearOfAdmission = item.Id;

                    }
                    try
                    {
                        //Creates a bulk wallet
                        allwallet.ApplicantId = applicants.Id;
                        allwallet.Name = applicants.Surname + " " + applicants.FirstName + " " + applicants.OtherName;
                        allwallet.WalletId = applicants.ApplicantId;
                        allwallet.BulkDebitBalanace = 0;
                        allwallet.CreditBalance = 0;
                        allwallet.Status = false;
                        allwallet.DateCreated = DateTime.Now;
                        _context.UgMainWallets.Add(allwallet);
                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }

                    return RedirectToAction("Index", "admissions", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("Index", "admissions");

        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission, ugClearance")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearApplicant(string? id, JupebStudent student)
        {
            //Get the person who performed this action
            //var loggedInUser = await userManager.GetUserAsync(HttpContext.User);
            //var userId = loggedInUser.StaffId;

            try
            {
                var applicant = await _context.JupebApplicants.FirstOrDefaultAsync(x => x.ApplicantId == id);
                //change the cleared status to true in the applicants table
                applicant.Cleared = true;
                var Session = (from T in _context.Sessions
                               where T.IsActive == true
                               where T.IsActive == true
                               select T).ToList();
                var dept = (from g in _context.Departments where g.Id == applicant.AdmittedInto select g).ToList();
                //Moving Applicant to student table
                student.ApplicantId = applicant.Id;
                student.StudentId = applicant.ApplicantId;
                student.Fullname = applicant.Surname + " " + applicant.FirstName + " " + applicant.OtherName;
                student.Picture = applicant.PassportUpload;
                student.Sex = applicant.Sex;
                student.DOB = applicant.DOB;
                student.Phone = applicant.PhoneNumber;
                student.Email = applicant.Email;
                student.NationalityId = applicant.NationalityId;
                student.StateOfOriginId = applicant.StateOfOriginId;
                student.LGAId = applicant.LGAId;
                student.PlaceOfBirth = applicant.PlaceOfBirth;
                student.PermanentHomeAddress = applicant.PermanentHomeAddress;
                student.ContactAddress = applicant.ContactAddress;
                student.MaritalStatus = applicant.MaritalStatus;
                student.ParentName = applicant.ParentFullName;
                student.ParentOccupation = applicant.ParentOccupation;
                student.ParentPhone = applicant.ParentPhoneNumber;
                student.ParentAltPhone = applicant.ParentAlternatePhoneNumber;
                student.ParentEmail = applicant.ParentEmail;
                student.ParentAddress = applicant.ParentAddress;
                student.StudentStatus = 1;
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
                student.ModeOfAdmission = applicant.ModeOfEntry;
                student.Cleared = true;
                //student.ClearedBy = userId;
                student.CreatedAt = DateTime.Now;
                _context.Add(student);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    Email = student.SchoolEmailAddress,
                    UserName = student.SchoolEmailAddress,
                    StudentsId = student.Id,
                    Type = 1,
                    EmailConfirmed = true
                };
                var r = await userManager.CreateAsync(user, "Password@1");

                return RedirectToAction("index", "admissions");
            }
            catch (Exception)
            {

                throw;
            }
                
          
        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission, ugClearance")]

        //Upon activation of wallet, this module creates the first debt row in the 
        //wallets table and updates the applicants/student's Main wallet.
        public async Task<IActionResult> ActivateWallet(string? id, UgSubWallet myWallet)
        {
            var applicant = await _context.JupebApplicants
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive == true);
            myWallet.SessionId = session.Id;

            //Since this module only activates wallet for freshers, regardless of the level
            //you're admitted to, you'd pay the amount the 100l are paying for that session
            var fee = (from s in _context.AllFees where s.DepartmentId == applicant.AdmittedInto select s).FirstOrDefault();
            var TuitionFee = fee.Tuition;
            myWallet.Name = applicant.Surname + " " + applicant.FirstName + " " + applicant.OtherName;
            myWallet.ApplicantId = applicant.Id;
            myWallet.Pic = applicant.PassportUpload;
           // myWallet.RegNo = applicant.UTMENumber;
            myWallet.Level = applicant.LevelAdmittedTo;
            myWallet.Department = applicant.AdmittedInto;
            myWallet.EDHIS = fee.EDHIS;
            myWallet.LMS = fee.LMS;
            myWallet.SRC = fee.SRC;
            myWallet.AcceptanceFee = fee.Acceptance;
            myWallet.Tuition = TuitionFee;
            //If applicant is a transfer student and if the he/she belongs to either MBBS, BNSc, Law.
            //The integer values below are the IDs of MBBS, BNSc and Law respectively in the Departments table.
            if (applicant.ModeOfEntry == "Transfer" && (applicant.AdmittedInto == 5 || applicant.AdmittedInto == 6 || applicant.AdmittedInto == 9))
            {
                myWallet.Tuition2 = myWallet.Tuition;
            }
            else
            {
                myWallet.Tuition2 = 0;
            }
            myWallet.Debit = myWallet.Tuition + myWallet.Tuition2 + myWallet.LMS + myWallet.SRC + myWallet.AcceptanceFee + myWallet.EDHIS;
            myWallet.FortyPercent = myWallet.Tuition * 40 / 100;
            myWallet.SixtyPercent = myWallet.Tuition * 60 / 100;
            myWallet.CreditBalance = 0;
            myWallet.WalletId = applicant.ApplicantId;
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


            return RedirectToAction(nameof(Index));
        }

        // GET: admissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            ViewData["AdmittedInto"] = new SelectList(_context.Departments, "Id", "Id", applicant.AdmittedInto);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", applicant.LGAId);
            ViewData["LevelAdmittedTo"] = new SelectList(_context.Levels, "Id", "Id", applicant.LevelAdmittedTo);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", applicant.NationalityId);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", applicant.StateOfOriginId);
            return View(applicant);
        }

        // POST: admissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,FirstName,OtherName,Sex,DOB,MaritalStatus,PlaceOfBirth,ContactAddress,PermanentHomeAddress,NationalityId,StateOfOriginId,LGAId,PhoneNumber,AltPhoneNumber,PrimarySchool,SecondarySchool,Indigine,ModeOfEntry,PreviousInstitution,PreviousLevel,PreviousGrade,Email,Password,UTMENumber,CourseChoseInJamb,UTMESubject1,UTMESubject2,UTMESubject3,UTMESubject4,UTMESubject1Score,UTMESubject2Score,UTMESubject3Score,UTMESubject4Score,UTMETotal,FirstChoice,SecondChoice,ThirdChoice,NoOfSittings,Ssce1Type,Ssce1Year,Ssce1Number,Ssce1Subject1,Ssce1Subject2,Ssce1Subject3,Ssce1Subject4,Ssce1Subject5,Ssce1Subject6,Ssce1Subject7,Ssce1Subject8,Ssce1Subject9,Ssce1Subject1Grade,Ssce1Subject2Grade,Ssce1Subject3Grade,Ssce1Subject4Grade,Ssce1Subject5Grade,Ssce1Subject6Grade,Ssce1Subject7Grade,Ssce1Subject8Grade,Ssce1Subject9Grade,Ssce2Type,Ssce2Year,Ssce2Number,Ssce2Subject1,Ssce2Subject2,Ssce2Subject3,Ssce2Subject4,Ssce2Subject5,Ssce2Subject6,Ssce2Subject7,Ssce2Subject8,Ssce2Subject9,Ssce2Subject1Grade,Ssce2Subject2Grade,Ssce2Subject3Grade,Ssce2Subject4Grade,Ssce2Subject5Grade,Ssce2Subject6Grade,Ssce2Subject7Grade,Ssce2Subject8Grade,Ssce2Subject9Grade,ParentFullName,ParentAddress,ParentPhoneNumber,ParentAlternatePhoneNumber,ParentEmail,ParentOccupation,PassportUpload,JambUpload,Ssce1,BirthCertUpload,DirectEntryUpload,LGAUpload,Status,Screened,LevelAdmittedTo,AdmittedInto,YearOfAdmission,Cleared,CreatedAt")] Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.Id))
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
            ViewData["AdmittedInto"] = new SelectList(_context.Departments, "Id", "Id", applicant.AdmittedInto);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", applicant.LGAId);
            ViewData["LevelAdmittedTo"] = new SelectList(_context.Levels, "Id", "Id", applicant.LevelAdmittedTo);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", applicant.NationalityId);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", applicant.StateOfOriginId);
            return View(applicant);
        }

        // GET: admissions/Delete/5
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JupebApplicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.JupebApplicants
                .Include(a => a.Departments)
                .Include(a => a.LGAs)
                .Include(a => a.Levels)
                .Include(a => a.Nationalities)
                .Include(a => a.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return PartialView("_delete",applicant);
        }
        [Authorize(Roles = "staff, superAdmin, jupebAdmission")]
        // POST: admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JupebApplicants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JupebApplicants'  is null.");
            }
            var applicant = await _context.JupebApplicants.FindAsync(id);
            if (applicant != null)
            {
                _context.JupebApplicants.Remove(applicant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        //public async Task<IActionResult> Wallet(string id)
        //{
        //    var applicant = await _context.JupebApplicants.Where(x => x.ApplicantId == id).FirstOrDefaultAsync();
        //    var wallet = await _context.UgMainWallets.Where(x => x.WalletId == id).FirstOrDefaultAsync();
        //    var model = new AdmissionWalletVM
        //    {
        //        MainWallet = wallet,
        //        Applicant = applicant
        //    };
        //    return View(model);
        //}
        public async Task<IActionResult> Debts(string id)
        {
            var wallet = (from s in _context.UgSubWallets where s.WalletId == id select s).Include(c => c.Sessions).ToList();

            if (wallet == null)
            {
                return RedirectToAction("pagenotfound", "error");
            }

            return View(wallet);

        }
        public IActionResult Hostelreq(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = _context.JupebApplicants.FirstOrDefault(i => i.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }
        public IActionResult LMSreq(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = _context.JupebApplicants.FirstOrDefault(i => i.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        } 
        public IActionResult Clearance(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = _context.JupebApplicants.FirstOrDefault(i => i.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }
        private bool ApplicantExists(int? id)
        {
          return _context.JupebApplicants.Any(e => e.Id == id);
        }
    }
}
