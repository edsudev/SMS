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
using Microsoft.AspNetCore.Authorization;
using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Controllers
{
   // [Authorize]
    public class BursaryClearancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public BursaryClearancesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: busaryClearances
        public async Task<IActionResult> Index()
        {
            var studentIds = _context.BursaryClearances.Select(b => b.StudentId).ToList();
            var students = _context.Students.Where(s => studentIds.Contains(s.Id)).Include(i => i.Departments).Include(i => i.Levels).ToList();

            return View(students);
        }
        public async Task<IActionResult> Students()
        {
            var applicationDbContext = _context.BursaryClearances.Include(b => b.Payments).ThenInclude(i => i.Sessions).Include(b => b.Students);
            return View(await applicationDbContext.ToListAsync());
        }
      //  [Authorize(Roles = "student")]
        public async Task<IActionResult> Preview(string? id)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = loggedInUser.StudentsId;
                var student = (from s in _context.Students where s.Id == userId select s)
                    .Include(c => c.Departments).Include(c => c.Levels).Include(c => c.Sessions).Include(c => c.Programs).FirstOrDefault();
                ViewBag.name = student.Fullname;
                ViewBag.email = student.SchoolEmailAddress;
                ViewBag.mat = student.MatNumber;
                ViewBag.dept = student.Departments.Name;
                //ViewBag.programme = student.Programs.NameOfProgram;
                ViewBag.session = student.Sessions.Name;
                ViewBag.level = student.Levels.Name;
                var clearance = (from s in _context.BursaryClearances where s.StudentId == userId && s.Sessions.Name == id select s).Include(i => i.Hostels).Include(i => i.Payments).ThenInclude(i => i.OtherFees).ThenInclude(i => i.Sessions).ToList();
                //var hostel = (from s in _context.HostelPayments where s.)
               
                if (clearance.Count() == 0)
                {
                    return RedirectToAction("resourcenotfound", "error");
                }
                return View(clearance);
            }
            catch (Exception)
            {
                return RedirectToAction("badreq", "error");
                throw;
            }
            
        }
      //  [Authorize(Roles = "student")]
        public async Task<IActionResult> History()
        {

            var sessions = (from c in _context.Sessions select c);
            return View(await sessions.ToListAsync());
        }
        // GET: busaryClearances/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                var student = (from s in _context.Students where s.SchoolEmailAddress == id select s).Include(i => i.Departments).FirstOrDefault();
                ViewBag.name = student.Fullname;
                ViewBag.mat = student.MatNumber;
                ViewBag.department = student.Departments.Name;
                ViewBag.email = student.SchoolEmailAddress;
                var clearances = (from c in _context.BursaryClearances where c.Students.SchoolEmailAddress == id select c).Include(i => i.Payments).ThenInclude(i => i.OtherFees).ToList();

                if (clearances == null)
                {
                    return RedirectToAction("PageNotFound", "error");
                }

                return View(clearances);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Clearance(ClearanceRemark status, string email)
        {
            var student = (from v in _context.BursaryClearances where v.Students.SchoolEmailAddress == email select v).FirstOrDefault();

           // student.rem = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // GET: busaryClearances/Create
      //  [Authorize(Roles = "student")]
        public IActionResult Offline()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            return View();
        }

        // POST: busaryClearances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      //  [Authorize(Roles = "student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Offline(OfflinePaymentClearance offlinePayment)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StudentsId;
            offlinePayment.StudentId = user;
            offlinePayment.CreatedAt = DateTime.Now;
           _context.Add(offlinePayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        // GET: busaryClearances/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: busaryClearances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClearanceId,StudentId,PaymentId,CreatedAt")] BursaryClearance busaryClearance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busaryClearance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", busaryClearance.PaymentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", busaryClearance.StudentId);
            return View(busaryClearance);
        }

        // GET: busaryClearances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BursaryClearances == null)
            {
                return NotFound();
            }

            var busaryClearance = await _context.BursaryClearances.FindAsync(id);
            if (busaryClearance == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", busaryClearance.PaymentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", busaryClearance.StudentId);
            return View(busaryClearance);
        }

        // POST: busaryClearances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClearanceId,StudentId,PaymentId,CreatedAt")] BursaryClearance busaryClearance)
        {
            if (id != busaryClearance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busaryClearance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusaryClearanceExists(busaryClearance.Id))
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
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", busaryClearance.PaymentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", busaryClearance.StudentId);
            return View(busaryClearance);
        }

        // GET: busaryClearances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BursaryClearances == null)
            {
                return NotFound();
            }

            var busaryClearance = await _context.BursaryClearances
                .Include(b => b.Payments)
                .Include(b => b.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busaryClearance == null)
            {
                return NotFound();
            }

            return View(busaryClearance);
        }

        // POST: busaryClearances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BursaryClearances == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BusaryClearances'  is null.");
            }
            var busaryClearance = await _context.BursaryClearances.FindAsync(id);
            if (busaryClearance != null)
            {
                _context.BursaryClearances.Remove(busaryClearance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusaryClearanceExists(int? id)
        {
          return _context.BursaryClearances.Any(e => e.Id == id);
        }
    }
}
