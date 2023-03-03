using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using EDSU_SYSTEM.Data;

namespace EDSU_SYSTEM.Controllers
{
    public class OtherFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OtherFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: otherFees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OtherFees.Include(o => o.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: otherFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OtherFees == null)
            {
                return NotFound();
            }

            var otherFees = await _context.OtherFees
                .Include(o => o.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherFees == null)
            {
                return NotFound();
            }

            return View(otherFees);
        }

        // GET: otherFees/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            return View();
        }

        // POST: otherFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,SessionId")] OtherFees otherFees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(otherFees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name", otherFees.SessionId);
            return View(otherFees);
        }

        // GET: otherFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OtherFees == null)
            {
                return NotFound();
            }

            var otherFees = await _context.OtherFees.FindAsync(id);
            if (otherFees == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", otherFees.SessionId);
            return View(otherFees);
        }

        // POST: otherFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,SessionId")] OtherFees otherFees)
        {
            if (id != otherFees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otherFees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtherFeesExists(otherFees.Id))
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
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name", otherFees.SessionId);
            return View(otherFees);
        }

        // GET: otherFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OtherFees == null)
            {
                return NotFound();
            }

            var otherFees = await _context.OtherFees
                .Include(o => o.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherFees == null)
            {
                return NotFound();
            }

            return View(otherFees);
        }

        // POST: otherFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OtherFees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OtherFees'  is null.");
            }
            var otherFees = await _context.OtherFees.FindAsync(id);
            if (otherFees != null)
            {
                _context.OtherFees.Remove(otherFees);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtherFeesExists(int? id)
        {
          return _context.OtherFees.Any(e => e.Id == id);
        }
    }
}
