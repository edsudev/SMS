using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;

namespace EDSU_SYSTEM.Controllers
{
    public class AllFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AllFees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AllFees.Include(a => a.Departments).Include(a => a.Levels).Include(a => a.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AllFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllFees == null)
            {
                return NotFound();
            }

            var allFees = await _context.AllFees
                .Include(a => a.Departments)
                .Include(a => a.Levels)
                .Include(a => a.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allFees == null)
            {
                return NotFound();
            }

            return View(allFees);
        }

        // GET: AllFees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            return View();
        }

        // POST: AllFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentId,LevelId,SessionId,Tuition,Tuition2,LMS,EDHIS,SRC,Acceptance,Medical,Causion,Sports,ICT,Library,IdentityCard,Registration")] AllFees allFees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allFees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", allFees.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id", allFees.LevelId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", allFees.SessionId);
            return View(allFees);
        }

        // GET: AllFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllFees == null)
            {
                return NotFound();
            }

            var allFees = await _context.AllFees.FindAsync(id);
            if (allFees == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            return View(allFees);
        }

        // POST: AllFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,DepartmentId,LevelId,SessionId,Tuition,Tuition2,LMS,EDHIS,SRC,Acceptance,Medical,Causion,Sports,ICT,Library,IdentityCard,Registration")] AllFees allFees)
        {
            if (id != allFees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allFees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllFeesExists(allFees.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", allFees.DepartmentId);
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Id", allFees.LevelId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", allFees.SessionId);
            return View(allFees);
        }

        // GET: AllFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllFees == null)
            {
                return NotFound();
            }

            var allFees = await _context.AllFees
                .Include(a => a.Departments)
                .Include(a => a.Levels)
                .Include(a => a.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allFees == null)
            {
                return NotFound();
            }

            return View(allFees);
        }

        // POST: AllFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.AllFees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AllFees'  is null.");
            }
            var allFees = await _context.AllFees.FindAsync(id);
            if (allFees != null)
            {
                _context.AllFees.Remove(allFees);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllFeesExists(int? id)
        {
          return (_context.AllFees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
