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
    public class creditUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public creditUnitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: creditUnits
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;
            var staff = _context.Staffs.Where(x => x.Id == userId).Select(x => x.DepartmentId).FirstOrDefault();
            var applicationDbContext = _context.CreditUnits.Where(x => x.DepartmentId == staff).Include(c => c.Departments).Include(c => c.Levels).Include(c => c.Semesters).Include(c => c.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: creditUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CreditUnits == null)
            {
                return NotFound();
            }

            var creditUnit = await _context.CreditUnits
                .Include(c => c.Departments)
                .Include(c => c.Levels)
                .Include(c => c.Semesters)
                .Include(c => c.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditUnit == null)
            {
                return NotFound();
            }

            return View(creditUnit);
        }

        // GET: creditUnits/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            return View();
        }

        // POST: creditUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreditUnit creditUnit)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;
            var staffdpt = _context.Staffs.Where(x => x.Id == userId).Select(x => x.DepartmentId).FirstOrDefault();
            var Lastcredit = (from s in _context.CreditUnits
                                 where s.LevelId == creditUnit.LevelId &&
                                 s.DepartmentId == staffdpt && s.SessionId == creditUnit.SessionId
                                 select s.Max).FirstOrDefault();
            if (creditUnit.Max > 50 || ( creditUnit.Max + Lastcredit > 50))
            {
                ViewBag.err = "There was an attempt to exceed the maximum credit unit for the session. For more info, contact the ICT.";
                return View();
            }
            else
            {
                creditUnit.DepartmentId = staffdpt;
                _context.Add(creditUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", creditUnit.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id", creditUnit.LevelId);
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Id", creditUnit.SemesterId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", creditUnit.SessionId);
            return View(creditUnit);
        }

        // GET: creditUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CreditUnits == null)
            {
                return NotFound();
            }

            var creditUnit = await _context.CreditUnits.FindAsync(id);
            if (creditUnit == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", creditUnit.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", creditUnit.LevelId);
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Name", creditUnit.SemesterId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name", creditUnit.SessionId);
            return View(creditUnit);
        }

        // POST: creditUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Max,Min,LevelId,SemesterId,DepartmentId,SessionId")] CreditUnit creditUnit)
        {
            if (id != creditUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditUnitExists(creditUnit.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", creditUnit.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id", creditUnit.LevelId);
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Id", creditUnit.SemesterId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", creditUnit.SessionId);
            return View(creditUnit);
        }

        // GET: creditUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CreditUnits == null)
            {
                return NotFound();
            }

            var creditUnit = await _context.CreditUnits
                .Include(c => c.Departments)
                .Include(c => c.Levels)
                .Include(c => c.Semesters)
                .Include(c => c.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditUnit == null)
            {
                return NotFound();
            }

            return PartialView("_delete",creditUnit);
        }

        // POST: creditUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CreditUnits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CreditUnits'  is null.");
            }
            var creditUnit = await _context.CreditUnits.FindAsync(id);
            if (creditUnit != null)
            {
                _context.CreditUnits.Remove(creditUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditUnitExists(int? id)
        {
          return _context.CreditUnits.Any(e => e.Id == id);
        }
    }
}
