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
    public class conversionStudentSupervisorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public conversionStudentSupervisorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: conversionStudentSupervisors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConversionStudentSupervisors.Include(c => c.Lecturers).Include(c => c.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: conversionStudentSupervisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConversionStudentSupervisors == null)
            {
                return NotFound();
            }

            var conversionStudentSupervisor = await _context.ConversionStudentSupervisors
                .Include(c => c.Lecturers)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionStudentSupervisor == null)
            {
                return NotFound();
            }

            return View(conversionStudentSupervisor);
        }

        // GET: conversionStudentSupervisors/Create
        public IActionResult Create()
        {
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "SchoolEmail");
            ViewData["Student"] = new SelectList(_context.ConversionStudents, "Id", "Fullname");
            return View();
        }

        // POST: conversionStudentSupervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Student,Supervisor,SupervisorRole,CreatedAt,UpdatedAt")] ConversionStudentSupervisor conversionStudentSupervisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversionStudentSupervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", conversionStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.ConversionStudents, "Id", "Id", conversionStudentSupervisor.Student);
            return View(conversionStudentSupervisor);
        }

        // GET: conversionStudentSupervisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConversionStudentSupervisors == null)
            {
                return NotFound();
            }

            var conversionStudentSupervisor = await _context.ConversionStudentSupervisors.FindAsync(id);
            if (conversionStudentSupervisor == null)
            {
                return NotFound();
            }
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", conversionStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.ConversionStudents, "Id", "Id", conversionStudentSupervisor.Student);
            return View(conversionStudentSupervisor);
        }

        // POST: conversionStudentSupervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Student,Supervisor,SupervisorRole,CreatedAt,UpdatedAt")] ConversionStudentSupervisor conversionStudentSupervisor)
        {
            if (id != conversionStudentSupervisor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversionStudentSupervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversionStudentSupervisorExists(conversionStudentSupervisor.Id))
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
            ViewData["Supervisor"] = new SelectList(_context.Staffs, "Id", "Id", conversionStudentSupervisor.Supervisor);
            ViewData["Student"] = new SelectList(_context.ConversionStudents, "Id", "Id", conversionStudentSupervisor.Student);
            return View(conversionStudentSupervisor);
        }

        // GET: conversionStudentSupervisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConversionStudentSupervisors == null)
            {
                return NotFound();
            }

            var conversionStudentSupervisor = await _context.ConversionStudentSupervisors
                .Include(c => c.Lecturers)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionStudentSupervisor == null)
            {
                return NotFound();
            }

            return View(conversionStudentSupervisor);
        }

        // POST: conversionStudentSupervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.ConversionStudentSupervisors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConversionStudentSupervisors'  is null.");
            }
            var conversionStudentSupervisor = await _context.ConversionStudentSupervisors.FindAsync(id);
            if (conversionStudentSupervisor != null)
            {
                _context.ConversionStudentSupervisors.Remove(conversionStudentSupervisor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversionStudentSupervisorExists(int? id)
        {
          return (_context.ConversionStudentSupervisors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
