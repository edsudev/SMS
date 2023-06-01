using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using EDSU_SYSTEM.Data;
using static EDSU_SYSTEM.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace EDSU_SYSTEM.Controllers
{
    public class vacanciesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public vacanciesController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // GET: vacancies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vacancies;
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Login()
        {
            return View();
;       }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var applicantEmail = await _context.Vacancies.FirstOrDefaultAsync(m => m.Email == email);
            
            if (email == null)
            {
                return NotFound();
            }
            var cred = (from c in _context.Vacancies where c.Email == email && c.Password == password select c).FirstOrDefault();
            if (cred != null)
            {
                var id = cred.Id;
                return RedirectToAction("step1", "vacancies", new { id });
            }
            else
            {
                return View();
            } 
                //return View();

        }
        // GET: vacancies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vacancies == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .Include(v => v.Positions)
                .Include(v => v.LGAs)
                .Include(v => v.Nationalities)
                .Include(v => v.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // GET: vacancies/Create
        public async Task<IActionResult> Academic()
        {
            var applicationDbContext = _context.VacancyLists
                .Include(i => i.Departments).ThenInclude(i => i.Faculties).Include(i => i.Positions) ;
            return View(await applicationDbContext.ToListAsync());
            
        }
        
        public async Task<IActionResult> NonAcademic()
        {
            var applicationDbContext = _context.VacancyLists
                .Include(i => i.Units).Include(i => i.Positions);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Academic_apply(string position, string department, Vacancy vacancy)
        {
            ViewBag.err = TempData["err"];
            TempData["position"] = (from d in _context.Positions where d.Name == position select d.Id).FirstOrDefault();
            TempData["department"] = (from d in _context.Departments where d.Name == department select d.Id).FirstOrDefault();

            return View();
        }
        // POST: vacancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Academic_apply(Vacancy vacancy, string pword, string cpword)
        {
            try
            {
                if (pword != cpword || (pword == null))
                {
                    TempData["err"] = "Something went wrong, make sure you provide data as required!";
                    return RedirectToAction(nameof(Academic_apply));
                }
                
                var emailExist = (from s in _context.Vacancies where s.Email == vacancy.Email select s.Id).FirstOrDefault();
                if (emailExist == null)
                {
                    Random r = new();
                    var session = (from s in _context.Sessions where s.IsActive == true select s).ToList();
                    foreach (var item in session)
                    {
                        vacancy.ApplicantId = "EDSU-V-" + item.suffix + "-" + DateTime.Now.Millisecond;
                    }
                    vacancy.Position = (int?)TempData["position"];
                    vacancy.DepartmentId = (int?)TempData["department"];
                    vacancy.Type = VacancyType.Academic;
                    _context.Add(vacancy);
                    await _context.SaveChangesAsync();
                    var id = vacancy.ApplicantId;
                    return RedirectToAction("step1", "vacancies", new { id });
                }
            }
            catch (Exception)
            {
                TempData["err"] = "There was a problem with your application. Contact the ICT unit";
                throw;
                
            }
            return View();
        }

        public async Task<IActionResult> Step1(string? id)
        {
            if (id == null || _context.Vacancies == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (vacancy == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", vacancy.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", vacancy.FacultyId);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Name", vacancy.LGAId);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Name", vacancy.NationalityId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", vacancy.StateId);
            return View(vacancy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step1(string id, int a)
        {
            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(i => i.ApplicantId == id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var vacancyToUpdate = await _context.Vacancies
                .FirstOrDefaultAsync(c => c.ApplicantId == id);

                if (await TryUpdateModelAsync<Vacancy>(vacancyToUpdate, "", c => c.Type, c => c.LastName,
                    c => c.FirstName, c => c.OtherName,
                c => c.DOB, c => c.Sex, c => c.Phone,c => c.Email, c => c.ContactAddress, c => c.NationalityId,c => c.StateId, c => c.LGAId
                ))
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
                    return RedirectToAction("Step2", "vacancies", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("Step2", "vacancies", new { id });
        }
        public async Task<IActionResult> Step2(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (vacancy == null)
            {
                return NotFound();
            }
            return View(vacancy);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step2(string? id, int a)
        {
            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(i => i.ApplicantId == id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var vacancyToUpdate = await _context.Vacancies
                .FirstOrDefaultAsync(c => c.ApplicantId == id);

                if (await TryUpdateModelAsync<Vacancy>(vacancyToUpdate, "", c => c.HighestQualification, c => c.FieldOfStudy,
                    c => c.AreaOfSpecialization, c => c.WorkedInHigherInstuition,
                c => c.CurrentPlaceOfWork, c => c.PositionAtCurrentPlaceOfWork, c => c.YearsOfExperience))
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
                    return RedirectToAction("Step3", "vacancies", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("Step3", "vacancies", new { id });

        }
        public async Task<IActionResult> Step3(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(i => i.ApplicantId == id);
            if (vacancy == null)
            {
                return NotFound();
            }
            return View(vacancy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step3(int id, IFormFile certificate, IFormFile cv, IFormFile pic, Vacancy vacancy)
        {
            var v = await _context.Vacancies.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var vacancyToUpdate = await _context.Vacancies
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Vacancy>(vacancyToUpdate, ""))
                {
                    try
                    {
                        if (pic != null && pic.Length > 0)
                        {
                            var uploadDir = @"files/vacancy/passport";
                            var fileName = Path.GetFileNameWithoutExtension(pic.FileName);
                            var extension = Path.GetExtension(pic.FileName);
                            var webRootPath = _hostingEnvironment.WebRootPath;
                            fileName = vacancy.ApplicantId + extension;

                            //fileName = fileName + extension;
                            var path = Path.Combine(webRootPath, uploadDir, fileName);
                            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                            {
                                pic.CopyTo(fs);
                                vacancy.Picture = fileName;

                            }
                        }
                        if (certificate != null && certificate.Length > 0)
                        {
                            var uploadDir = @"files/vacancy/certificate";
                            var fileName = Path.GetFileNameWithoutExtension(certificate.FileName);
                            var extension = Path.GetExtension(certificate.FileName);
                            var webRootPath = _hostingEnvironment.WebRootPath;
                            fileName = vacancy.ApplicantId + extension;

                            var path = Path.Combine(webRootPath, uploadDir, fileName);
                            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                            {
                                certificate.CopyTo(fs);
                                vacancy.CertUpload = fileName;

                            }
                        }
                        if (cv != null && cv.Length > 0)
                        {
                            var uploadDir = @"files/vacancy/cv";
                            var fileName = Path.GetFileNameWithoutExtension(cv.FileName);
                            var extension = Path.GetExtension(cv.FileName);
                            var webRootPath = _hostingEnvironment.WebRootPath;
                            fileName = vacancy.ApplicantId + extension;

                            var path = Path.Combine(webRootPath, uploadDir, fileName);
                            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                            {
                                cv.CopyTo(fs);
                                vacancy.CVUpload = fileName;

                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction("Step3", "vacancies", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        public IActionResult Employ(string? id)
        {
            var applicants = _context.Vacancies.Where(x => x.ApplicantId == id).FirstOrDefault();

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");

            return PartialView("_employPartial", applicants);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Employ(string id, Staff staff, Vacancy vacancy)
        {
            var applicant = await _context.Vacancies.FirstOrDefaultAsync(i=>i.ApplicantId == id);

                staff.FirstName = applicant.LastName;
            staff.Surname = applicant.FirstName;
            staff.MiddleName = applicant.OtherName;
                staff.SchoolEmail = applicant.LastName + "." + applicant.FirstName + "@edouniversity.edu.ng";
            staff.NationalityId = applicant.NationalityId;
            staff.StateId = applicant.StateId;
            staff.LGAId = applicant.LGAId;
            //staff.DOB = applicant.DOB;
           // staff.MaritalStatus = applicant.MaritalStatus;
            staff.Email = applicant.Email;
            staff.Phone = applicant.Phone;
            staff.Picture = applicant.Picture;
            staff.Sex = applicant.Sex;
            staff.Religion = applicant.Religion;
            staff.FacultyId = vacancy.FacultyId;
            staff.DepartmentId = vacancy.DepartmentId;
            staff.Position = vacancy.Position;
                _context.Staffs.Add(staff);
                await _context.SaveChangesAsync();
                var user = new ApplicationUser
                {
                    Email = staff.SchoolEmail,
                    UserName = staff.SchoolEmail,
                    StaffId = staff.Id,
                    Type = 2,
                    EmailConfirmed = true
                };
                var r = await userManager.CreateAsync(user, "Password@1");
                return RedirectToAction("Index", "Vacancies");
        }
        // GET: vacancies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vacancies == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", vacancy.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", vacancy.FacultyId);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", vacancy.LGAId);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", vacancy.NationalityId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", vacancy.StateId);
            return View(vacancy);
        }

        // POST: vacancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Position,FacultyId,DepartmentId,FirstName,LastName,OtherName,Email,DOB,Sex,NationalityId,StateId,LGAId,Phone,ContactAddress,HighestQualification,FieldOfStudy,AreaOfSpecialization,WorkedInHigherInstuition,CurrentPlaceOfWork,PositionAtCurrentPlaceOfWork,YearsOfExperience,CertUpload,CVUpload,Picture,CreatedAt,UpdatedAt,isEmployed")] Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyExists(vacancy.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", vacancy.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", vacancy.FacultyId);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", vacancy.LGAId);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", vacancy.NationalityId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Id", vacancy.StateId);
            return View(vacancy);
        }
        public async Task<IActionResult> Cancel(int? id)
        {
            var applicants = _context.Vacancies.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_cancel", applicants);
        }
        public async Task<IActionResult> Reject(int? id)
        {
            var applicants = _context.Vacancies.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("_reject", applicants);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int? id, int a)
        {
            var applicants = await _context.Vacancies.FindAsync(id);


            try
            {
                var ApplicatantToUpdate = await _context.Vacancies
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Vacancy>(ApplicatantToUpdate, "", c => c.Id))
                {
                    applicants.IsEmployed = false;
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
        // GET: vacancies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vacancies == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .Include(v => v.Departments)
                .Include(v => v.Faculties)
                .Include(v => v.LGAs)
                .Include(v => v.Nationalities)
                .Include(v => v.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return PartialView("_delete",vacancy);
        }

        // POST: vacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vacancies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vacancies'  is null.");
            }
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy != null)
            {
                _context.Vacancies.Remove(vacancy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacancyExists(int? id)
        {
          return _context.Vacancies.Any(e => e.Id == id);
        }
    }
}
