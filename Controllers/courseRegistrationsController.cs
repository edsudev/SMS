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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Controllers
{
    public class CourseRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public CourseRegistrationsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: courseRegistrations
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            var courses = (from c in _context.CourseRegistrations where c.StudentId == userId select c).Include(c => c.Courses).Include(c => c.Students);
            ViewData["student"] = userId;

            return View(await courses.ToListAsync());
        }
        public async Task<IActionResult> History()
        {
            
            var sessions = (from c in _context.Sessions select c);
           
            return View(await sessions.ToListAsync());
        }
        //[Authorize(Roles = "Level Adviser")]
        // GET: My students
        public async Task<IActionResult> Pending()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var id = loggedInUser.StaffId;
            var staff = (from c in _context.Staffs where c.Id == id select c.DepartmentId).FirstOrDefault();
            var AdviserLevel = (from l in _context.LevelAdvisers where l.StaffId == id select l.LevelId).FirstOrDefault();
            var Levelstudents = (from c in _context.Students where c.Department == staff && c.Level == AdviserLevel select c).ToList();
            
            return View(Levelstudents);
        }
        public async Task<IActionResult> Approved()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var id = loggedInUser.StaffId;
            var staff = (from c in _context.Staffs where c.Id == id select c.DepartmentId).FirstOrDefault();
            var AdviserLevel = (from l in _context.LevelAdvisers where l.StaffId == id select l.LevelId).FirstOrDefault();
            var Levelstudents = (from c in _context.Students where c.Department == staff && c.Level == AdviserLevel select c).ToList();
            
            return View(Levelstudents);
        }
        public async Task<IActionResult> Summary(string id)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var studentId = loggedInUser.StudentsId;
                var student = (from s in _context.Students where s.Id == studentId select s)
                    .Include(c => c.Departments).Include(c => c.Levels).Include(c => c.Sessions).FirstOrDefault();
                ViewBag.name = student.Fullname;
                ViewBag.email = student.SchoolEmailAddress;
                ViewBag.mat = student.MatNumber;
                ViewBag.dept = student.Departments.Name;
                ViewBag.session = student.Sessions.Name;
                //ViewBag.program = student.Programs.NameOfProgram;
                ViewBag.level = student.Levels.Name;
                var approvedCourses = (from c in _context.CourseRegistrations
                                       where c.StudentId == studentId &&
                                        c.Status == MainStatus.Approved &&
                                        c.Sessions.Name == id
                                       select c).Include(c => c.Courses).Include(c => c.Students).ToList();

                var creditunit = (from c in _context.CreditUnits where c.DepartmentId == student.Department && c.LevelId == student.Level && c.Sessions.Name == id select c).FirstOrDefault();
                ViewBag.min = creditunit.Min;
                ViewBag.max = creditunit.Max;
                TempData["max"] = creditunit.Max;
                TempData["min"] = creditunit.Min;

                var TotalCredit = (from c in approvedCourses select c.Courses.CreditUnit).ToList();
                ViewBag.sum = TotalCredit.Sum();
                if (approvedCourses.Count() == 0)
                {
                    return RedirectToAction("resourcenotfound", "error");
                }
                else
                {
                    return View("Summary", approvedCourses);
                }
               
            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "error");
                throw;
            }
            
        }
        public IActionResult Pendingreg(int id)
        {
            var students = (from c in _context.CourseRegistrations where c.StudentId == id && c.Status == MainStatus.Pending select c).Include(c => c.Courses).Include(c => c.Students).ToList();
            return View(students);
        }
        public IActionResult Approvedreg(int id)
        {
            var students = (from c in _context.CourseRegistrations where c.StudentId == id && c.Status == MainStatus.Approved select c).Include(c => c.Courses).ThenInclude(i => i.Semesters).Include(c => c.Students).ToList();
            return View(students);
        }
        public async Task<IActionResult> RejectCourse(int id)
        {

            if (id == null || _context.CourseRegistrations == null)
            {
                return NotFound();
            }
            var courseRegistration = await _context.CourseRegistrations.FindAsync(id);
            if (courseRegistration == null)
            {
                return NotFound();
            }
            return PartialView("_reject",courseRegistration);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var course = await _context.CourseRegistrations.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var courseToUpdate = await _context.CourseRegistrations
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<CourseRegistration>(courseToUpdate, "", c => c.Comment))
                {
                    course.Status = MainStatus.Declined;
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
                    return RedirectToAction("pendingreg", "courseregistrations", new {id});
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("pendingreg", "courseregistrations", new { id });
        }
        
        public async Task<IActionResult> Approve(int? id)
        {
            var course = await _context.CourseRegistrations.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var courseToUpdate = await _context.CourseRegistrations
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<CourseRegistration>(courseToUpdate, ""))
                {
                    
                    course.Status = MainStatus.Approved;
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
                    return RedirectToAction("pending", "courseregistrations");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction("index", "courseregistrations");
        }
            
        // GET: courseRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseRegistrations == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistrations
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            return View(courseRegistration);
        }

        // GET: courseRegistrations/Create
        public async Task<IActionResult> First()
        {
            try
            {
                var userId = await _userManager.GetUserAsync(HttpContext.User);
            var student = (from c in _context.Students where c.Id == userId.StudentsId select c).FirstOrDefault();
            var courses = (from d in _context.Courses where d.DepartmentId == student.Department && d.Semester == 1 select d)
                .Include(c => c.Departments).Include(c => c.Semesters).Include(c => c.Levels);

            //Getting Credit unit Info
            var creditunit = (from c in _context.CreditUnits where c.DepartmentId == student.Department && c.LevelId == student.Level select c).FirstOrDefault();
            ViewBag.max = creditunit.Max;

            var NoOfCoursesRegInFirstSemester = (from c in _context.CourseRegistrations where c.StudentId == userId.StudentsId && c.Courses.Semester == 1 select c).Include(c => c.Courses).ToList();
            ViewBag.No = NoOfCoursesRegInFirstSemester.Count();
            
            var TotalCredit = (from c in NoOfCoursesRegInFirstSemester select c.Courses.CreditUnit).ToList();
            ViewBag.sum = TotalCredit.Sum();

            ViewBag.sumLeft = ViewBag.max - ViewBag.sum;
            TempData["sum"] = ViewBag.sumLeft;

            return View(await courses.ToListAsync());
            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "Error");
                throw;
            }
        }
        public async Task<IActionResult> Second()
        {
            try
            {
                var userId = await _userManager.GetUserAsync(HttpContext.User);
                var student = (from c in _context.Students where c.Id == userId.StudentsId select c).FirstOrDefault();
                var courses = (from d in _context.Courses where d.DepartmentId == student.Department && d.Semester == 2 select d)
                    .Include(c => c.Departments).Include(c => c.Semesters).Include(c => c.Levels);

                //Getting Credit unit Info
                var creditunit = (from c in _context.CreditUnits where c.DepartmentId == student.Department && c.LevelId == student.Level select c).FirstOrDefault();
                ViewBag.max = creditunit.Max;

                var NoOfCoursesRegInSecondSemester = (from c in _context.CourseRegistrations where c.StudentId == userId.StudentsId && c.Courses.Semester == 2 select c).Include(c => c.Courses).ToList();
                ViewBag.No = NoOfCoursesRegInSecondSemester.Count();

                var TotalCredit = (from c in NoOfCoursesRegInSecondSemester select c.Courses.CreditUnit).ToList();
                ViewBag.sum = TotalCredit.Sum();

                ViewBag.sumLeft = ViewBag.max - ViewBag.sum;
                TempData["sum"] = ViewBag.sumLeft;

                return View(await courses.ToListAsync());
            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "Error");
                throw;
            }
            

        }

        // POST: courseRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId,Comment,Status,CreatedAt,DateApproved")] CourseRegistration courseRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseRegistration.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseRegistration.StudentId);
            return View(courseRegistration);
        }
       
        public async Task<IActionResult> AddCourse(string id, CourseRegistration courseRegistration)
        {
            try
            {
                var userId = await _userManager.GetUserAsync(HttpContext.User);
                if (id == null || _context.CourseRegistrations == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses
                    .FirstOrDefaultAsync(m => m.Code == id);
                if (course == null)
                {
                    return NotFound();
                }
                var session = (from c in _context.Sessions where c.IsActive == true select c.Id).FirstOrDefault();
                courseRegistration.CourseId = course.Id;
                courseRegistration.StudentId = userId.StudentsId;
                courseRegistration.SessionId = session;
                courseRegistration.Status = MainStatus.Pending;
                courseRegistration.CreatedAt = DateTime.Now;
                _context.CourseRegistrations.Add(courseRegistration);
                ViewBag.courseunit = courseRegistration.Courses.CreditUnit;
                if (ViewBag.courseunit > (int)TempData["sum"])
                {
                    return BadRequest("Credit unit cannot be more than what is left");
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(First));
            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "Error");
                throw;
            }
            
        }
        // GET: courseRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourseRegistrations == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistrations.FindAsync(id);
            if (courseRegistration == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseRegistration.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseRegistration.StudentId);
            return View(courseRegistration);
        }

        // POST: courseRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,StudentId,Comment,Status,CreatedAt,DateApproved")] CourseRegistration courseRegistration)
        {
            if (id != courseRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseRegistrationExists(courseRegistration.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseRegistration.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", courseRegistration.StudentId);
            return View(courseRegistration);
        }

        // GET: courseRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseRegistrations == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistrations
                .Include(c => c.Courses)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            return View(courseRegistration);
        }

        // POST: courseRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseRegistrations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CourseRegistrations'  is null.");
            }
            var courseRegistration = await _context.CourseRegistrations.FindAsync(id);
            if (courseRegistration != null)
            {
                _context.CourseRegistrations.Remove(courseRegistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseRegistrationExists(int? id)
        {
          return _context.CourseRegistrations.Any(e => e.Id == id);
        }
    }
}
