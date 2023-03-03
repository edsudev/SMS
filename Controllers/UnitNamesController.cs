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
    public class UnitNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnitNames
        public async Task<IActionResult> Index()
        {
              return View(await _context.UnitNames.ToListAsync());
        }

        // GET: UnitNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UnitNames == null)
            {
                return NotFound();
            }

            var unitName = await _context.UnitNames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitName == null)
            {
                return NotFound();
            }

            return View(unitName);
        }

        // GET: UnitNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,UpdateddAt")] UnitName unitName)
        {
            if (ModelState.IsValid)
            {
                unitName.CreatedAt = DateTime.Now;
                _context.Add(unitName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitName);
        }

        // GET: UnitNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UnitNames == null)
            {
                return NotFound();
            }

            var unitName = await _context.UnitNames.FindAsync(id);
            if (unitName == null)
            {
                return NotFound();
            }
            return View(unitName);
        }

        // POST: UnitNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedAt,UpdateddAt")] UnitName unitName)
        {
            if (id != unitName.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitName.UpdateddAt = DateTime.Now;
                    _context.Update(unitName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitNameExists(unitName.Id))
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
            return View(unitName);
        }

        // GET: UnitNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UnitNames == null)
            {
                return NotFound();
            }

            var unitName = await _context.UnitNames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitName == null)
            {
                return NotFound();
            }

            return View(unitName);
        }

        // POST: UnitNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UnitNames == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UnitNames'  is null.");
            }
            var unitName = await _context.UnitNames.FindAsync(id);
            if (unitName != null)
            {
                _context.UnitNames.Remove(unitName);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitNameExists(int? id)
        {
          return _context.UnitNames.Any(e => e.Id == id);
        }
    }
}
