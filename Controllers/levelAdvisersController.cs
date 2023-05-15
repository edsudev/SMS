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
    public class levelAdvisersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public levelAdvisersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: levelAdvisers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LevelAdvisers.Include(l => l.Levels).Include(l => l.Staffs);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: levelAdvisers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LevelAdvisers == null)
            {
                return NotFound();
            }

            var levelAdviser = await _context.LevelAdvisers
                .Include(l => l.Levels)
                .Include(l => l.Staffs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (levelAdviser == null)
            {
                return NotFound();
            }

            return View(levelAdviser);
        }

        // GET: levelAdvisers/Create
        public IActionResult Create()
        {
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "SchoolEmail");
            return View();
        }

        // POST: levelAdvisers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StaffId,LevelId,Status,CreatedAt,UpdatedAt")] LevelAdviser levelAdviser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(levelAdviser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", levelAdviser.LevelId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "SchoolEmail", levelAdviser.StaffId);
            return View(levelAdviser);
        }

        // GET: levelAdvisers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LevelAdvisers == null)
            {
                return NotFound();
            }

            var levelAdviser = await _context.LevelAdvisers.FindAsync(id);
            if (levelAdviser == null)
            {
                return NotFound();
            }
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", levelAdviser.LevelId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "SchoolEmail", levelAdviser.StaffId);
            return View(levelAdviser);
        }

        // POST: levelAdvisers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StaffId,LevelId,Status,CreatedAt,UpdatedAt")] LevelAdviser levelAdviser)
        {
            if (id != levelAdviser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(levelAdviser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LevelAdviserExists(levelAdviser.Id))
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
            ViewData["LevelId"] = new SelectList(_context.Levels, "Id", "Name", levelAdviser.LevelId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "Id", "SchoolEmail", levelAdviser.StaffId);
            return View(levelAdviser);
        }

        // GET: levelAdvisers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LevelAdvisers == null)
            {
                return NotFound();
            }

            var levelAdviser = await _context.LevelAdvisers
                .Include(l => l.Levels)
                .Include(l => l.Staffs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (levelAdviser == null)
            {
                return NotFound();
            }

            return PartialView("_delete", levelAdviser);
        }

        // POST: levelAdvisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LevelAdvisers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LevelAdvisers'  is null.");
            }
            var levelAdviser = await _context.LevelAdvisers.FindAsync(id);
            if (levelAdviser != null)
            {
                _context.LevelAdvisers.Remove(levelAdviser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LevelAdviserExists(int? id)
        {
          return _context.LevelAdvisers.Any(e => e.Id == id);
        }
    }
}
