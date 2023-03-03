using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using static EDSU_SYSTEM.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace EDSU_SYSTEM.Controllers
{
    public class EvaluationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EvaluationsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: evaluations
        public async Task<IActionResult> Courses()
        {
            var applicationDbContext = _context.Evaluations.Include(e => e.Courses).Include(e => e.Grades1).Include(e => e.Lecturers).Include(e => e.Questions);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var studentId = loggedInUser.StudentsId;
            var session = (from c in _context.Sessions where c.IsActive == true select c.Id).FirstOrDefault();
            var approvedCourses = (from c in _context.CourseRegistrations
                                    where c.StudentId == studentId &&
                                    c.Status == MainStatus.Approved &&
                                    c.SessionId == session
                                   select c).Include(c => c.Courses).Include(c => c.Students).ToList();
            return View(approvedCourses);
        }

        // GET: evaluations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .Include(e => e.Courses)
                .Include(e => e.Grades1)
                .Include(e => e.Lecturers)
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // GET: evaluations/Create
        public async Task<IActionResult> Create(string id)
        {
            var staff = (from s in _context.CourseAllocations where s.Courses.Code == id select s.Staff).ToList();
            ViewData["LecturerId"] = new SelectList(staff, "Id", "Name");
            return View();
        }

        // POST: evaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, string grade1, Evaluation evaluation)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var studentId = loggedInUser.StudentsId;
            var course = _context.Courses.Where(x => x.Code == id)
                                            .Select(x => x.Id)
                                            .FirstOrDefault();

            evaluation.StudentId = studentId;
            evaluation.CourseId = course;
            evaluation.CreatedAt = DateTime.Now;
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive == true);
            evaluation.SessionId = session.Id;

            if (grade1 == "1")
            {
                evaluation.Grade1 = 5;
            }
            else if (grade1 == "2")
            {
                evaluation.Grade1 = 4;
            }
            else if (grade1 == "3")
            {
                evaluation.Grade1 = 3;
            }
            else if (grade1 == "4")
            {
                evaluation.Grade1 = 2;
            }
            else if (grade1 == "5")
            {
                evaluation.Grade1 = 1;
            }
            else
            {
                evaluation.Grade1 = 0;
            }
                _context.Add(evaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        // GET: evaluations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", evaluation.CourseId);
            ViewData["Grade"] = new SelectList(_context.EvaluationGrades, "Id", "Id", evaluation.Grade1);
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "Id", evaluation.LecturerId);
            ViewData["QuestionId"] = new SelectList(_context.EvaluationQuestions, "Id", "Id", evaluation.QuestionId);
            return View(evaluation);
        }

        // POST: evaluations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Grade,QuestionId,LecturerId,CourseId,CreatedAt")] Evaluation evaluation)
        {
            if (id != evaluation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationExists(evaluation.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", evaluation.CourseId);
            ViewData["Grade"] = new SelectList(_context.EvaluationGrades, "Id", "Id", evaluation.Grade1);
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "Id", evaluation.LecturerId);
            ViewData["QuestionId"] = new SelectList(_context.EvaluationQuestions, "Id", "Id", evaluation.QuestionId);
            return View(evaluation);
        }

        // GET: evaluations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .Include(e => e.Courses)
                .Include(e => e.Grades1)
                .Include(e => e.Lecturers)
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // POST: evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evaluations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Evaluations'  is null.");
            }
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation != null)
            {
                _context.Evaluations.Remove(evaluation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationExists(int? id)
        {
          return _context.Evaluations.Any(e => e.Id == id);
        }
    }
}
