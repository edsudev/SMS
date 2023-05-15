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
    public class SessionalClearanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionalClearanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionalClearance
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BursaryClearedStudents.Include(b => b.Sessions).Include(b => b.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SessionalClearance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BursaryClearedStudents == null)
            {
                return NotFound();
            }

            var bursaryClearedStudents = await _context.BursaryClearedStudents
                .Include(b => b.Sessions)
                .Include(b => b.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bursaryClearedStudents == null)
            {
                return NotFound();
            }

            return View(bursaryClearedStudents);
        }

        // GET: SessionalClearance/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Fullname");
            return View();
        }

        // POST: SessionalClearance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,SessionId,Remark,CreatedAt")] BursaryClearedStudents bursaryClearedStudents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bursaryClearedStudents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", bursaryClearedStudents.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", bursaryClearedStudents.StudentId);
            return View(bursaryClearedStudents);
        }

        // GET: SessionalClearance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BursaryClearedStudents == null)
            {
                return NotFound();
            }

            var bursaryClearedStudents = await _context.BursaryClearedStudents.FindAsync(id);
            if (bursaryClearedStudents == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", bursaryClearedStudents.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", bursaryClearedStudents.StudentId);
            return View(bursaryClearedStudents);
        }

        // POST: SessionalClearance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,StudentId,SessionId,Remark,CreatedAt")] BursaryClearedStudents bursaryClearedStudents)
        {
            if (id != bursaryClearedStudents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bursaryClearedStudents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BursaryClearedStudentsExists(bursaryClearedStudents.Id))
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
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", bursaryClearedStudents.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", bursaryClearedStudents.StudentId);
            return View(bursaryClearedStudents);
        }

        // GET: SessionalClearance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BursaryClearedStudents == null)
            {
                return NotFound();
            }

            var bursaryClearedStudents = await _context.BursaryClearedStudents
                .Include(b => b.Sessions)
                .Include(b => b.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bursaryClearedStudents == null)
            {
                return NotFound();
            }

            return View(bursaryClearedStudents);
        }

        // POST: SessionalClearance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.BursaryClearedStudents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BursaryClearedStudents'  is null.");
            }
            var bursaryClearedStudents = await _context.BursaryClearedStudents.FindAsync(id);
            if (bursaryClearedStudents != null)
            {
                _context.BursaryClearedStudents.Remove(bursaryClearedStudents);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BursaryClearedStudentsExists(int? id)
        {
          return (_context.BursaryClearedStudents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
