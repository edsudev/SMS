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
using System.Collections;
using static EDSU_SYSTEM.Models.Enum;
using System.Text.Json;

namespace EDSU_SYSTEM.Controllers
{
    public class ParentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ParentsController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: parents
        //public IActionResult GetStarted()
        //{
        //      return View();
        //}
       
        //// POST: parents/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> GetStarted(string id)
        //{
        //    var transactionID = (from v in _context.Payments where v.Ref == id && v.Status == "approved" select v).FirstOrDefault();
        //    if (transactionID == null)
        //    {
        //        ViewBag.Nullerror = "There was an error in the Transaction ID";
        //        ViewBag.Nullerror2 = "Check and Try again or contact the ICT unit.";
        //        return View();
        //    }
        //    else
        //    {
        //        var HasBeenUsed = (from s in _context.Parent where s.PaymentId == id select s).FirstOrDefault();
        //        if (HasBeenUsed != null)
        //        {
        //            return BadRequest();
        //        }
        //        else
        //        {
        //            return RedirectToAction("create", "parents");
        //        }
                
        //    }
            
        //}
        public async Task<IActionResult> Profile()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.Parent;
            var parent = (from s in _context.Parent where s.Id == user select s).FirstOrDefault();
            return View(parent);
        }
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.Parent;
            var parent = (from s in _context.Parent where s.Id == user select s).FirstOrDefault();

            ViewBag.Name = parent.Title + " " + parent.Name;
            ViewBag.Email = parent.Email;
            ViewBag.Phone = parent.Phone;
            ViewBag.Address = parent.Address;
            var wardList = (from s in _context.ParentWards where s.ParentId == user select s).Include(s => s.Students).ThenInclude(s => s.Departments).
                Include(s => s.Students).ThenInclude(s => s.Departments).ToList();

            return View(wardList);
        }
        public async Task<IActionResult> Mywards()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.Parent;
            var mywards = (from s in _context.ParentWards where s.ParentId == user select s).Include(x => x.Students)
                .ThenInclude(x=> x.Departments).Include(x => x.Students).ThenInclude(x => x.Levels).ToList();
            return View(await _context.Parent.ToListAsync());
        }
        // GET: parents/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            var student = (from c in _context.Students where c.SchoolEmailAddress == id select c).Include(i => i.Applicants).FirstOrDefault();
            var wallet = (from c in _context.UgMainWallets where c.Id == student.ApplicantId select c).Include(i => i.Applicants).ThenInclude(i => i.Departments).FirstOrDefault();
            var approvedCourses = (from c in _context.CourseRegistrations
                                   where c.StudentId == student.Id &&
                            c.Status == MainStatus.Approved &&
                            c.SessionId == student.CurrentSession
                                   select c).Include(c => c.Courses).ToList();
            var timetable = (from c in _context.TimeTables
                             where c.Courses.Courses.DepartmentId == student.Department && c.Courses.Courses.Level ==
                             student.Level
                             select c).Include(c => c.Courses).ThenInclude(c => c.Courses).Include(c => c.Staffs).ThenInclude(c => c.Staff).ToList();

            //After getting the courses from coursereg and Scores from Results table, we sorted them using the course code before serializing them
            //so that the courses can align with the courses since they are coming from the same table.
            var grades = (from g in _context.Results where g.StudentId == student.MatNumber select g).ToList();
            var sortedCourses = approvedCourses.OrderBy(s => s.Courses.Code);
            var sortedGrades = grades.OrderBy(c => c.CourseId);

            var CourseCode = (from c in sortedCourses select c.Courses.Code).ToList();
            var TestScores = (from v in sortedGrades select v.CA).ToList();

            var json = JsonSerializer.Serialize(CourseCode);
            var json2 = JsonSerializer.Serialize(TestScores);

            ViewBag.StudentName = student.Fullname;
            ViewBag.courses = json;
            ViewBag.grade = json2;
            return View();

        }

        // GET: parents/Create
        public IActionResult Addward()
        {
            ViewData["parent"] = new SelectList(_context.Parent, "Id", "Email");
            ViewData["student"] = new SelectList(_context.Students, "Id", "UTMENumber");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Addward(ParentWard pw)
        {
            pw.CreatedAt = DateTime.Now;
            _context.ParentWards.Add(pw);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,Title,Name,Phone,Address,Email,CreatedAt")] Parent parent)
        {
            
                parent.ParentId = Guid.NewGuid().ToString();
                parent.CreatedAt = DateTime.Now;
                _context.Add(parent);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    Email = parent.Email,
                    UserName = parent.Email,
                    Parent = parent.Id,
                    Type = 5,
                    EmailConfirmed = true
                };
                var r = await _userManager.CreateAsync(user, "Password@1");
               
                return RedirectToAction(nameof(Index));
        
        }

        // GET: parents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,Title,Name,Phone,Address,Email,CreatedAt")] Parent parent)
        {
            if (id != parent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(parent.Id))
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
            return View(parent);
        }

        // GET: parents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parent'  is null.");
            }
            var parent = await _context.Parent.FindAsync(id);
            if (parent != null)
            {
                _context.Parent.Remove(parent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentExists(int? id)
        {
          return _context.Parent.Any(e => e.Id == id);
        }
    }
}
