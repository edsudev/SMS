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
    public class conversionStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public conversionStudentsController(UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _context = context;
        }
        // GET: conversionStudents
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.ConversionStudent;
            var student = (from c in _context.ConversionStudents where c.Id == userId select c).Include(i => i.Departments).FirstOrDefault();
            var wallet = (from c in _context.UgMainWallets where c.UTME == student.UTMENumber select c).Include(i => i.Applicants).ThenInclude(i => i.Departments).FirstOrDefault();

            var mainWallet = (from s in _context.ConversionMainWallets where s.WalletId == student.UTMENumber select s).FirstOrDefault(); ;

            var approvedCourses = (from c in _context.ConversionCourseRegs
                                   where c.StudentId == userId &&
                            c.Status == MainStatus.Approved &&
                            c.SessionId == student.CurrentSession
                                   select c).Include(c => c.Courses).ToList();

            var timetable = (from c in _context.TimeTables
                             where c.Courses.Courses.DepartmentId == student.Department && c.Courses.Courses.Level ==
                             student.Level
                             select c).Include(c => c.Courses).ThenInclude(c => c.Courses).Include(c => c.Staffs).ThenInclude(c => c.Staff).ToList();

            var model = new ConversionDashboardVM
            {
                MainWallet = mainWallet,
                Courses = approvedCourses,
                TimeTables = timetable
            };
            return View(model);
        }
        public async Task<IActionResult> Allstudents()
        {
            var applicationDbContext = _context.ConversionStudents.Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Faculties).Include(c => c.LGAs).Include(c => c.Levels).Include(c => c.Nationalities).Include(c => c.Programs).Include(c => c.Sessions).Include(c => c.Staffs).Include(c => c.States).Include(c => c.YearOfAdmissions);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Graduated()
        {
            var applicationDbContext = _context.ConversionStudents.Where(x => x.StudentStatus == 2).Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Faculties).Include(c => c.LGAs).Include(c => c.Levels).Include(c => c.Nationalities).Include(c => c.Programs).Include(c => c.Sessions).Include(c => c.Staffs).Include(c => c.States).Include(c => c.YearOfAdmissions);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Expelled()
        {
            var applicationDbContext = _context.ConversionStudents.Where(x => x.StudentStatus == 3).Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Faculties).Include(c => c.LGAs).Include(c => c.Levels).Include(c => c.Nationalities).Include(c => c.Programs).Include(c => c.Sessions).Include(c => c.Staffs).Include(c => c.States).Include(c => c.YearOfAdmissions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: conversionStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConversionStudents == null)
            {
                return NotFound();
            }

            var conversionStudent = await _context.ConversionStudents
                .Include(c => c.Applicants)
                .Include(c => c.Departments)
                .Include(c => c.Faculties)
                .Include(c => c.LGAs)
                .Include(c => c.Levels)
                .Include(c => c.Nationalities)
                .Include(c => c.Programs)
                .Include(c => c.Sessions)
                .Include(c => c.Staffs)
                .Include(c => c.States)
                .Include(c => c.YearOfAdmissions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionStudent == null)
            {
                return NotFound();
            }

            return View(conversionStudent);
        }

        // GET: conversionStudents/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id");
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id");
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id");
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id");
            ViewData["ProgrameId"] = new SelectList(_context.Programs, "Id", "Id");
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id");
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id");
            ViewData["YearOfAdmission"] = new SelectList(_context.Sessions, "Id", "Id");
            return View();
        }

        // POST: conversionStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicantId,StudentId,Picture,Fullname,Sex,DOB,Religion,Phone,AltPhoneNumber,Email,NationalityId,StateOfOriginId,LGAId,PlaceOfBirth,PermanentHomeAddress,ContactAddress,MaritalStatus,ParentName,ParentOccupation,ParentPhone,ParentAltPhone,ParentEmail,ParentAddress,SchoolEmailAddress,UTMENumber,MatNumber,Faculty,Level,ModeOfAdmission,ProgrameId,YearOfAdmission,Department,CurrentSession,CreatedAt,Cleared,ClearedBy,IsStillAStudent,StudentStatus")] ConversionStudent conversionStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversionStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionStudent.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", conversionStudent.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", conversionStudent.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", conversionStudent.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", conversionStudent.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", conversionStudent.NationalityId);
            ViewData["ProgrameId"] = new SelectList(_context.Programs, "Id", "Id", conversionStudent.ProgrameId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", conversionStudent.CurrentSession);
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id", conversionStudent.ClearedBy);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", conversionStudent.StateOfOriginId);
            ViewData["YearOfAdmission"] = new SelectList(_context.Sessions, "Id", "Id", conversionStudent.YearOfAdmission);
            return View(conversionStudent);
        }

        // GET: conversionStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConversionStudents == null)
            {
                return NotFound();
            }

            var conversionStudent = await _context.ConversionStudents.FindAsync(id);
            if (conversionStudent == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionStudent.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", conversionStudent.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", conversionStudent.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", conversionStudent.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", conversionStudent.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", conversionStudent.NationalityId);
            ViewData["ProgrameId"] = new SelectList(_context.Programs, "Id", "Id", conversionStudent.ProgrameId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", conversionStudent.CurrentSession);
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id", conversionStudent.ClearedBy);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", conversionStudent.StateOfOriginId);
            ViewData["YearOfAdmission"] = new SelectList(_context.Sessions, "Id", "Id", conversionStudent.YearOfAdmission);
            return View(conversionStudent);
        }

        // POST: conversionStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ApplicantId,StudentId,Picture,Fullname,Sex,DOB,Religion,Phone,AltPhoneNumber,Email,NationalityId,StateOfOriginId,LGAId,PlaceOfBirth,PermanentHomeAddress,ContactAddress,MaritalStatus,ParentName,ParentOccupation,ParentPhone,ParentAltPhone,ParentEmail,ParentAddress,SchoolEmailAddress,UTMENumber,MatNumber,Faculty,Level,ModeOfAdmission,ProgrameId,YearOfAdmission,Department,CurrentSession,CreatedAt,Cleared,ClearedBy,IsStillAStudent,StudentStatus")] ConversionStudent conversionStudent)
        {
            if (id != conversionStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversionStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversionStudentExists(conversionStudent.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionStudent.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", conversionStudent.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", conversionStudent.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", conversionStudent.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", conversionStudent.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", conversionStudent.NationalityId);
            ViewData["ProgrameId"] = new SelectList(_context.Programs, "Id", "Id", conversionStudent.ProgrameId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", conversionStudent.CurrentSession);
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id", conversionStudent.ClearedBy);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", conversionStudent.StateOfOriginId);
            ViewData["YearOfAdmission"] = new SelectList(_context.Sessions, "Id", "Id", conversionStudent.YearOfAdmission);
            return View(conversionStudent);
        }

        // GET: conversionStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConversionStudents == null)
            {
                return NotFound();
            }

            var conversionStudent = await _context.ConversionStudents
                .Include(c => c.Applicants)
                .Include(c => c.Departments)
                .Include(c => c.Faculties)
                .Include(c => c.LGAs)
                .Include(c => c.Levels)
                .Include(c => c.Nationalities)
                .Include(c => c.Programs)
                .Include(c => c.Sessions)
                .Include(c => c.Staffs)
                .Include(c => c.States)
                .Include(c => c.YearOfAdmissions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionStudent == null)
            {
                return NotFound();
            }

            return View(conversionStudent);
        }

        // POST: conversionStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.ConversionStudents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConversionStudents'  is null.");
            }
            var conversionStudent = await _context.ConversionStudents.FindAsync(id);
            if (conversionStudent != null)
            {
                _context.ConversionStudents.Remove(conversionStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversionStudentExists(int? id)
        {
          return (_context.ConversionStudents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
