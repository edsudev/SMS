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
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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
            var applicationDbContext = _context.Evaluations.Include(e => e.Courses).Include(e => e.Grades1).Include(e => e.Lecturers);
            return View(await applicationDbContext.ToListAsync());
        }
       // [Authorize(Roles = "student")]
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
        public IActionResult Results(string? id, string code)
        {
        
            var staffId = _context.Staffs.Where(x => x.SchoolEmail == id).Select(x => x.Id).FirstOrDefault();
            var staffCourses = (from s in _context.CourseAllocations where s.LecturerId == staffId select s).Include(c => c.Courses).ToList();
            var sortedCourses = staffCourses.OrderBy(x => x.Courses.Code);
            var courses = (from c in sortedCourses select c.Courses.Code).ToList();
            var json = JsonSerializer.Serialize(courses);
            ViewBag.courses = json;

            Console.Write(ViewBag.courses);

            var totalGradesList = new List<int>();
            var StaffEvaluations = _context.Evaluations.Where(x => x.LecturerId == staffId).ToList();
            var sortedEvaluations = StaffEvaluations.OrderBy(x => x.CourseId);
            var evaluations = (from s in sortedEvaluations select s).ToList();
            foreach (var evaluation in evaluations)
            {
                // After suming all grades, we go ahead to convert them to percentage
                var totalSum = new[] { evaluation.Grade1, evaluation.Grade2, evaluation.Grade3, evaluation.Grade4, evaluation.Grade5 }.Sum();
                var result = totalSum * 20 / 100;
                totalGradesList.Add((int)result);
            }
            var json2 = JsonConvert.SerializeObject(totalGradesList);
            Console.WriteLine(json2);
            ViewBag.Percentage = json2;
            return View();
        }
        // GET: evaluations/Details/5
        public async Task<IActionResult> MyCourses()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var staffId = loggedInUser.StaffId;
           // var staffId = _context.Staffs.Where(x => x.SchoolEmail == id).Select(x => x.Id).FirstOrDefault();
            var staffCourses = (from s in _context.CourseAllocations where s.LecturerId == staffId select s).Include(c => c.Courses).ToList();
            var sortedCourses = staffCourses.OrderBy(x => x.Courses.Code);
            var courses = (from c in sortedCourses select c.Courses.Code).ToList();
            var json = JsonSerializer.Serialize(courses);
            ViewBag.courses = json;

            Console.Write(ViewBag.courses);

            var totalGradesList = new List<int>();
            var StaffEvaluations = _context.Evaluations.Where(x => x.LecturerId == staffId).ToList();
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
            return View();
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
        public async Task<IActionResult> Create(string id, string question1, string question2,string question3, string question4,string question5, 
            string question6,string question7, string question8,string question9,string question10,
            string question11,string question12,string question13,string question14,string question15,
            string question16, string question17,string question18,string question19, string question20, Evaluation evaluation)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var studentId = loggedInUser.StudentsId;
            var course = _context.Courses.Where(x => x.Code == id)
                                            
                                            .FirstOrDefault();

            evaluation.StudentId = studentId;
            evaluation.CourseId = course.Id;
            evaluation.SemesterId = course.Semester;
            evaluation.CreatedAt = DateTime.Now;
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive == true);
            evaluation.SessionId = session.Id;
           

           
            Dictionary<string, int> gradeMappings = new Dictionary<string, int>()
            {
                { "1", 5 },
                { "2", 4 },
                { "3", 3 },
                { "4", 2 },
                { "5", 1 }
            };

            evaluation.Grade1 = gradeMappings.ContainsKey(question1) ? gradeMappings[question1] : 0;
            evaluation.Grade2 = gradeMappings.ContainsKey(question2) ? gradeMappings[question2] : 0;
            evaluation.Grade3 = gradeMappings.ContainsKey(question3) ? gradeMappings[question3] : 0;
            evaluation.Grade4 = gradeMappings.ContainsKey(question4) ? gradeMappings[question4] : 0;
            evaluation.Grade5 = gradeMappings.ContainsKey(question5) ? gradeMappings[question5] : 0;
            evaluation.Grade6 = gradeMappings.ContainsKey(question6) ? gradeMappings[question6] : 0;
            evaluation.Grade7 = gradeMappings.ContainsKey(question7) ? gradeMappings[question7] : 0;
            evaluation.Grade8 = gradeMappings.ContainsKey(question8) ? gradeMappings[question8] : 0;
            evaluation.Grade9 = gradeMappings.ContainsKey(question9) ? gradeMappings[question9] : 0;
            evaluation.Grade10 = gradeMappings.ContainsKey(question10) ? gradeMappings[question10] : 0;
            evaluation.Grade11 = gradeMappings.ContainsKey(question11) ? gradeMappings[question11] : 0;
            evaluation.Grade12 = gradeMappings.ContainsKey(question12) ? gradeMappings[question12] : 0;
            evaluation.Grade13 = gradeMappings.ContainsKey(question13) ? gradeMappings[question13] : 0;
            evaluation.Grade14 = gradeMappings.ContainsKey(question14) ? gradeMappings[question14] : 0;
            evaluation.Grade15 = gradeMappings.ContainsKey(question15) ? gradeMappings[question15] : 0;
            evaluation.Grade16 = gradeMappings.ContainsKey(question16) ? gradeMappings[question16] : 0;
            evaluation.Grade17 = gradeMappings.ContainsKey(question17) ? gradeMappings[question17] : 0;
            evaluation.Grade18 = gradeMappings.ContainsKey(question18) ? gradeMappings[question18] : 0;
            evaluation.Grade19 = gradeMappings.ContainsKey(question19) ? gradeMappings[question19] : 0;
            evaluation.Grade20 = gradeMappings.ContainsKey(question20) ? gradeMappings[question20] : 0;

            _context.Add(evaluation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
          
        }
        //HOD to see all his lecturers
        /// <summary>
        /// The authorization would allow HOD see this
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Staffs()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var staffId = loggedInUser.StaffId;
            var HOD = _context.Staffs.Where(x => x.Id == staffId).Select(x => x.DepartmentId).FirstOrDefault();
            var departmentStaffs = _context.Staffs.Where(x => x.DepartmentId == HOD).Include(x => x.Departments).Include(x => x.Positions).ToList();

            return View(departmentStaffs);
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
