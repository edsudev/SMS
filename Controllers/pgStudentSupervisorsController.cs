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
    public class PgStudentSupervisorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PgStudentSupervisorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: pgStudentSupervisors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PgStudentsSupervisors.Include(p => p.Lecturers).Include(p => p.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: pgStudentSupervisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PgStudentsSupervisors == null)
            {
                return NotFound();
            }

            var pgStudentSupervisor = await _context.PgStudentsSupervisors
                .Include(p => p.Lecturers)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgStudentSupervisor == null)
            {
                return NotFound();
            }

            return View(pgStudentSupervisor);
        }

        // GET: pgStudentSupervisors/Create
        public IActionResult Create()
        {
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "SchoolEmail");
            ViewData["Student"] = new SelectList(_context.PostGraduateStudents, "Id", "Fullname");
            return View();
        }

        // POST: pgStudentSupervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Student,Supervisor,SupervisorRole,CreatedAt,UpdatedAt")] PgStudentSupervisor pgStudentSupervisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pgStudentSupervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", pgStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.PostGraduateStudents, "Id", "Id", pgStudentSupervisor.Student);
            return View(pgStudentSupervisor);
        }

        // GET: pgStudentSupervisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PgStudentsSupervisors == null)
            {
                return NotFound();
            }

            var pgStudentSupervisor = await _context.PgStudentsSupervisors.FindAsync(id);
            if (pgStudentSupervisor == null)
            {
                return NotFound();
            }
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", pgStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.PostGraduateStudents, "Id", "Id", pgStudentSupervisor.Student);
            return View(pgStudentSupervisor);
        }

        // POST: pgStudentSupervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Student,Supervisor,SupervisorRole,CreatedAt,UpdatedAt")] PgStudentSupervisor pgStudentSupervisor)
        {
            if (id != pgStudentSupervisor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pgStudentSupervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PgStudentSupervisorExists(pgStudentSupervisor.Id))
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
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", pgStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.PostGraduateStudents, "Id", "Id", pgStudentSupervisor.Student);
            return View(pgStudentSupervisor);
        }

        // GET: pgStudentSupervisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PgStudentsSupervisors == null)
            {
                return NotFound();
            }

            var pgStudentSupervisor = await _context.PgStudentsSupervisors
                .Include(p => p.Lecturers)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgStudentSupervisor == null)
            {
                return NotFound();
            }

            return View(pgStudentSupervisor);
        }

        // POST: pgStudentSupervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PgStudentsSupervisors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PgStudentsSupervisors'  is null.");
            }
            var pgStudentSupervisor = await _context.PgStudentsSupervisors.FindAsync(id);
            if (pgStudentSupervisor != null)
            {
                _context.PgStudentsSupervisors.Remove(pgStudentSupervisor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PgStudentSupervisorExists(int? id)
        {
          return _context.PgStudentsSupervisors.Any(e => e.Id == id);
        }
    }
}
