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
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Courses.Include(c => c.Departments).Include(c => c.Levels).Include(c => c.Semesters);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> MyCourses()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var staff = loggedInUser.StaffId;
            var courses = (from s in _context.CourseAllocations where s.LecturerId == staff && s.Courses.Semesters.IsActive == true && s.Courses.Sessions.IsActive == true select s).Include(x => x.Courses).ThenInclude(x => x.Semesters).ToList();
            return View(courses);
        }

        // GET: courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Departments)
                .Include(c => c.Levels)
                .Include(c => c.Semesters)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: courses/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Name");
            ViewData["Semester"] = new SelectList(_context.Semesters, "Id", "Name");
            return View();
        }

        // POST: courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Code,Level,Semester,CreditUnit,Status,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.CreatedAt = DateTime.Now;
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", course.DepartmentId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", course.Level);
            ViewData["Semester"] = new SelectList(_context.Semesters, "Id", "Id", course.Semester);
            return View(course);
        }

        // GET: courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Name", course.Level);
            ViewData["Semester"] = new SelectList(_context.Semesters, "Id", "Name", course.Semester);
            return View(course);
        }

        // POST: courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Code,Level,Semester,CreditUnit,Status,DepartmentId,CreatedAt,UpdatedAt")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", course.DepartmentId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", course.Level);
            ViewData["Semester"] = new SelectList(_context.Semesters, "Id", "Id", course.Semester);
            return View(course);
        }

        // GET: courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Departments)
                .Include(c => c.Levels)
                .Include(c => c.Semesters)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return PartialView("_delete", course);
        }

        // POST: courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int? id)
        {
          return _context.Courses.Any(e => e.Id == id);
        }
    }
}
