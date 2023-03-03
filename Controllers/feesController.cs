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
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: fees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fees.Include(f => f.Departments).Include(f => f.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: fees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .Include(f => f.Departments)
                .Include(f => f.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // GET: fees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            return View();
        }

        // POST: fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Faculty,Level1,Level2,Level3,Level4,Level5,Level6,Pgd,Msc,Phd,DepartmentId,SessionId")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", fee.DepartmentId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", fee.SessionId);
            return View(fee);
        }

        // GET: fees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", fee.DepartmentId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", fee.SessionId);
            return View(fee);
        }

        // POST: fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Faculty,Level1,Level2,Level3,Level4,Level5,Level6,Pgd,Msc,Phd,DepartmentId,SessionId")] Fee fee)
        {
            if (id != fee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeExists(fee.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", fee.DepartmentId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", fee.SessionId);
            return View(fee);
        }

        // GET: fees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .Include(f => f.Departments)
                .Include(f => f.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // POST: fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fees'  is null.");
            }
            var fee = await _context.Fees.FindAsync(id);
            if (fee != null)
            {
                _context.Fees.Remove(fee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeExists(int? id)
        {
          return _context.Fees.Any(e => e.Id == id);
        }
    }
}
