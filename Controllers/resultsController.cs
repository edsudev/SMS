using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Microsoft.AspNetCore.Identity;
using static EDSU_SYSTEM.Models.Enum;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace EDSU_SYSTEM.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ResultsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<List<Result>> ImportCA(string id, IFormFile file, Result result)
        {
            var list = new List<Result>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    //The row is starting from 2 because the first row is the header
                    for (int i = 2; i <= rowcount; i++)
                    {
                        var session = (from c in _context.Sessions where c.IsActive == true select c.Id).FirstOrDefault();
                        try
                        {
                            var test = new Result
                            {
                                StudentId = (string)(worksheet.Cells[i, 1].Value ?? string.Empty),
                                CA = ((double)worksheet.Cells[i, 2].Value),
                                CreatedAt = DateTime.Now,
                                CourseId = id,
                                SessionId = session,
                                ResultId = Guid.NewGuid().ToString(),
                        };
                            _context.Results.Add(test);
                            _context.SaveChanges();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                       
                    }
                }

            }
            return list;
        }
        public IActionResult UploadExams(string id)
        {
            var result = (from d in _context.Results where d.CourseId == id select d).ToList();
            //var records = _context.Results.Where
            return View(result);
        }
        public class ExamRecordViewModel
        {
            public string StudentId { get; set; }
            public string CourseId { get; set; }
            public double Score { get; set; }
        }
        public async Task<IActionResult> ImportEXAM(string id, IFormFile file)
        {
            // Read the Excel file into a byte array
            byte[] fileBytes;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                fileBytes = stream.ToArray();
            }

            // Parse the Excel data into the ViewModel
            var examRecords = new List<ExamRecordViewModel>();
            using (var package = new ExcelPackage(new MemoryStream(fileBytes)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    var examRecord = new ExamRecordViewModel();
                    examRecord.StudentId = worksheet.Cells[row, 1].Value?.ToString();
                    examRecord.Score = ((double)worksheet.Cells[row, 2].Value);
                    examRecords.Add(examRecord);
                }
            }

            for (int i = 0; i < examRecords.Count; i++)
            {
                var examRecord = examRecords[i];
                var existingResult = await _context.Results.FirstOrDefaultAsync(r => r.StudentId == examRecord.StudentId && r.CourseId == examRecord.CourseId);
                if (existingResult != null)
                {
                    existingResult.Exam = examRecord.Score;
                    // Update any other properties as needed
                    _context.Results.Update(existingResult);
                }
                else
                {
                    var result = new Result
                    {
                        StudentId = examRecord.StudentId,
                        CourseId = examRecord.CourseId,
                        Exam = examRecord.Score
                    };
                    _context.Results.Add(result);
                }
            }

            await _context.SaveChangesAsync();

            // Redirect back to the view
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Index()
        {
            ViewBag.err = TempData["err"];
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var id = loggedInUser.StaffId;
            var staff = (from c in _context.Staffs where c.Id == id select c.DepartmentId).FirstOrDefault();
            var AdviserLevel = (from l in _context.LevelAdvisers where l.StaffId == id select l.LevelId).FirstOrDefault();
            var Levelstudents = (from c in _context.Students where c.Department == staff && c.Level == AdviserLevel select c);
            return View(await Levelstudents.ToListAsync());
        }
        public async Task<IActionResult> Mycourses()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var id = loggedInUser.StaffId;
            try
            {
                var staff = (from c in _context.Staffs where c.Id == id select c).FirstOrDefault();
                var AdviserLevel = (from l in _context.LevelAdvisers where l.StaffId == id select l.LevelId).FirstOrDefault();
                var courses = (from c in _context.CourseAllocations where c.LecturerId == staff.Id select c).Include(i => i.Courses).ThenInclude(i => i.Semesters);
                return View(await courses.ToListAsync());
            }
            catch (Exception)
            {
                TempData["NoCourses"] = "Oops! It seems you do not have any courses.";
                return RedirectToAction("Index", "staffs");
                throw;
            }
            
        }

        // GET: results/Details/5
        public async Task<IActionResult> Preview(string? id)
        {
            try
            {
                var studentInfo = (from f in _context.Students where f.SchoolEmailAddress == id select f).Include(x => x.Programs).
                Include(x => x.Departments).Include(x => x.Faculties).Include(x => x.Levels).FirstOrDefault();
                ViewBag.parentName = studentInfo.ParentName;
                ViewBag.name = studentInfo.Fullname;
                ViewBag.matno = studentInfo.MatNumber;
               // ViewBag.program = studentInfo.Programs.NameOfProgram;
                ViewBag.department = studentInfo.Departments.Name;
                ViewBag.faculty = studentInfo.Faculties.Name;
                ViewBag.level = studentInfo.Levels.Name;

                // Getting Result

                var result = _context.Results.Where(r => r.StudentId == studentInfo.MatNumber).ToList();
                foreach (var item in result)
                {
                    var total = item.CA + item.Exam;
                    //var grade = "";
                    if (total >= 70)
                    {
                        ViewBag.grade = "A";
                    }
                    else if (total <= 69 || total >= 59)
                    {
                        ViewBag.grade = "B";
                    }
                    var course = _context.Courses.FirstOrDefault(x => x.Code == item.CourseId);
                    ViewBag.courseId = course.Code;
                    ViewBag.coursetitle = course.Title;
                    ViewBag.courseUnit = course.CreditUnit;
                    ViewBag.status = course.Status;
                }
                return View(result);
            }
            catch (Exception)
            {
                TempData["err"] = "A problem Occured, Kindly contact Admin";
                return RedirectToAction(nameof(Index));
                throw;
            }

        }

        // GET: results/Create
        public IActionResult Upload()
        {
            return View();
        }
       // [Authorize(Roles = "student")]
        public async Task<IActionResult> Assessment()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            var student = (from c in _context.Students where c.Id == userId select c).Include(i => i.Applicants).FirstOrDefault();
            var approvedCourses = (from c in _context.CourseRegistrations
                                   where c.StudentId == userId &&
                                   c.Status == MainStatus.Approved &&
                                   c.SessionId == student.CurrentSession
                                   select c).Include(i => i.Courses).ToList();
            //After getting the courses from coursereg and Scores from Results table, we sorted them using the course code before serializing them
            //so that the courses can align with the courses since they are coming from the same table.
            var grades = (from g in _context.Results where g.StudentId == student.MatNumber select g).ToList();
            var sortedCourses = approvedCourses.OrderBy(s => s.Courses.Code);
            var sortedGrades = grades.OrderBy(c => c.CourseId);

            var CourseCode = (from c in sortedCourses select c.Courses.Code).ToList();
            var TestScores = (from v in sortedGrades select v.CA).ToList();

            var json = JsonSerializer.Serialize(CourseCode);
            var json2 = JsonSerializer.Serialize(TestScores);


            ViewBag.courses = json;
            ViewBag.grade = json2;
            
            Console.WriteLine(ViewBag.courses);
            return View();
        }
        public IActionResult Myresult()
        {
            return RedirectToAction("resourcenotfound", "error");
        }

        // POST: results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int? id, Result result)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", result.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", result.StudentId);
            return View(result);
        }

        // GET: results/Edit/5
        public async Task<IActionResult> students(string? id)
        {
            if (id == null || _context.CourseAllocations == null)
            {
                return NotFound();
            }
            var course = (from i in _context.CourseAllocations where i.Courses.Code == id select i).Include(i => i.Courses).FirstOrDefault();
            var result = (from f in _context.Results where f.CourseId == course.Courses.Code select f);

            
            if (result == null)
            {
                return NotFound();
            }
            return View(await result.ToListAsync());
        }

        // POST: results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Result result)
        {
            try
            {
                var recordToUpdate = await _context.Results
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Result>(recordToUpdate, "", c => c.Upgrade))
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
                    return RedirectToAction("Step2", "admissions", new { id });
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            
            return View(result);
        }

        // GET: results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Results == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Results'  is null.");
            }
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
          return _context.Results.Any(e => e.Id == id);
        }
    }
}
