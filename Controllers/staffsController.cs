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

        // GET: staffs
        public async Task<IActionResult> Index()
        {
            //Checks works table and get where status == pending or in progress   
            var worksPending = (from c in _context.Works where c.Status == Models.Enum.WorksStatus.Pending select c).ToList();
            var worksInProgress = (from c in _context.Works where c.Status == Models.Enum.WorksStatus.In_Progress select c).ToList();
            ViewBag.worksPending = worksPending.Count();
            ViewBag.worksInProgress = worksInProgress.Count();
                
                
            var exeat = (from c in _context.Exeats where c.HallMasterStatus == Models.Enum.MainStatus.Pending select c).ToList();
            ViewBag.exeat = exeat.Count();
            return View();  
        }
        public async Task<IActionResult> AllStaffs()
        {
            var applicationDbContext = _context.Staffs;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Stats()
        {
            var male = (from c in _context.Students where c.Sex == "Male" select c).ToList();
            var female = (from c in _context.Students where c.Sex == "Female" select c).ToList();
            ViewBag.MaleStudents = male.Count();
            ViewBag.FemaleStudents = female.Count();

            //Staffs
            var maleStaffs = (from c in _context.Staffs where c.Sex == "Male" select c).ToList();
            var femaleStaffs = (from c in _context.Staffs where c.Sex == "Female" select c).ToList();
            ViewBag.MaleStaffs = maleStaffs.Count();
            ViewBag.FemaleStaffs = femaleStaffs.Count();

            var department = (from c in _context.Departments select c).ToList();
            ViewBag.dpt = department;
            foreach (var item in department)
            {
                var male1 = (from c in _context.Students where c.Department == item.Id select c).ToList();
                ViewBag.DeptStudents = male1.Count();
            }
            return View();
        }
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
        public async Task<IActionResult> Details(int? id)
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
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public async Task<IActionResult> Edit(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile picture, Staff staff)
        {
            var applicants = await _context.Staffs.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            if (id != staff.Id)
            {
                return NotFound();
            }
            try
            {
                if (picture != null && picture.Length > 0)
                {
                    var uploadDir = @"files/vacancyUploads/passport";
                    var fileName = Path.GetFileNameWithoutExtension(picture.FileName);
                    var extension = Path.GetExtension(picture.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = staff.SchoolEmail + extension;

                    //fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        picture.CopyTo(fs);
                        staff.Picture = fileName;

                    }
                }
                var StaffToUpdate = await _context.Staffs
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Staff>(StaffToUpdate, "", 
                    c => c.Picture, c => c.Name, c => c.DOB, c => c.Sex, 
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
                    return RedirectToAction("profile", "staffs", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();
           
        }

        // GET: staffs/Delete/5
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
        public async Task<IActionResult> Documents(int? id)
        {

            //var staff = _context.Staffs.Where(x => x.Id == id).FirstOrDefault();
            var staff = _context.Staffs.Where(x => x.Id == id).FirstOrDefault();

            return PartialView("_documents", staff);
        }
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
                    fileName = staff.Name + extension;

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
