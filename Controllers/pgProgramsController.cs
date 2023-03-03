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
    public class PgProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PgProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: pgPrograms
        public async Task<IActionResult> Index()
        {
              return View(await _context.PgPrograms.ToListAsync());
        }

        // GET: pgPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PgPrograms == null)
            {
                return NotFound();
            }

            var pgProgram = await _context.PgPrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgProgram == null)
            {
                return NotFound();
            }

            return View(pgProgram);
        }

        // GET: pgPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pgPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameOfProgram")] PgProgram pgProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pgProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pgProgram);
        }

        // GET: pgPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PgPrograms == null)
            {
                return NotFound();
            }

            var pgProgram = await _context.PgPrograms.FindAsync(id);
            if (pgProgram == null)
            {
                return NotFound();
            }
            return View(pgProgram);
        }

        // POST: pgPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameOfProgram")] PgProgram pgProgram)
        {
            if (id != pgProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pgProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PgProgramExists(pgProgram.Id))
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
            return View(pgProgram);
        }

        // GET: pgPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PgPrograms == null)
            {
                return NotFound();
            }

            var pgProgram = await _context.PgPrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgProgram == null)
            {
                return NotFound();
            }

            return View(pgProgram);
        }

        // POST: pgPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PgPrograms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PgPrograms'  is null.");
            }
            var pgProgram = await _context.PgPrograms.FindAsync(id);
            if (pgProgram != null)
            {
                _context.PgPrograms.Remove(pgProgram);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PgProgramExists(int? id)
        {
          return _context.PgPrograms.Any(e => e.Id == id);
        }
    }
}
