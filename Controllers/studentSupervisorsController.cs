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
    public class StudentSupervisorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentSupervisorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: studentSupervisors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UgStudentSupervisors.Include(u => u.Lecturers).Include(u => u.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: studentSupervisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UgStudentSupervisors == null)
            {
                return NotFound();
            }

            var ugStudentSupervisor = await _context.UgStudentSupervisors
                .Include(u => u.Lecturers)
                .Include(u => u.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ugStudentSupervisor == null)
            {
                return NotFound();
            }

            return View(ugStudentSupervisor);
        }

        // GET: studentSupervisors/Create
        public IActionResult Create()
        {
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "SchoolEmail");
            ViewData["Student"] = new SelectList(_context.Students, "Id", "Fullname");
            return View();
        }

        // POST: studentSupervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Student,Supervisor,SupervisorRole,CreatedAt,UpdatedAt")] UgStudentSupervisor ugStudentSupervisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ugStudentSupervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", ugStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.Students, "Id", "Id", ugStudentSupervisor.Student);
            return View(ugStudentSupervisor);
        }

        // GET: studentSupervisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UgStudentSupervisors == null)
            {
                return NotFound();
            }

            var ugStudentSupervisor = await _context.UgStudentSupervisors.FindAsync(id);
            if (ugStudentSupervisor == null)
            {
                return NotFound();
            }
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", ugStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.Students, "Id", "Id", ugStudentSupervisor.Student);
            return View(ugStudentSupervisor);
        }

        // POST: studentSupervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Student,Supervisor,SupervisorRole,CreatedAt,UpdatedAt")] UgStudentSupervisor ugStudentSupervisor)
        {
            if (id != ugStudentSupervisor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ugStudentSupervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UgStudentSupervisorExists(ugStudentSupervisor.Id))
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
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", ugStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.Students, "Id", "Id", ugStudentSupervisor.Student);
            return View(ugStudentSupervisor);
        }

        // GET: studentSupervisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UgStudentSupervisors == null)
            {
                return NotFound();
            }

            var ugStudentSupervisor = await _context.UgStudentSupervisors
                .Include(u => u.Lecturers)
                .Include(u => u.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ugStudentSupervisor == null)
            {
                return NotFound();
            }

            return View(ugStudentSupervisor);
        }

        // POST: studentSupervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UgStudentSupervisors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UgStudentSupervisors'  is null.");
            }
            var ugStudentSupervisor = await _context.UgStudentSupervisors.FindAsync(id);
            if (ugStudentSupervisor != null)
            {
                _context.UgStudentSupervisors.Remove(ugStudentSupervisor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UgStudentSupervisorExists(int? id)
        {
          return _context.UgStudentSupervisors.Any(e => e.Id == id);
        }
    }
}
