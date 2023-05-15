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
    public class pgCourseRegsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public pgCourseRegsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: pgCourseRegs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PgCourseRegs.Include(p => p.Courses).Include(p => p.Sessions).Include(p => p.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: pgCourseRegs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PgCourseRegs == null)
            {
                return NotFound();
            }

            var pgCourseReg = await _context.PgCourseRegs
                .Include(p => p.Courses)
                .Include(p => p.Sessions)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgCourseReg == null)
            {
                return NotFound();
            }

            return View(pgCourseReg);
        }

        // GET: pgCourseRegs/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.PgCourses, "Id", "Id");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.PostGraduateStudents, "Id", "Id");
            return View();
        }

        // POST: pgCourseRegs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseRegId,CourseId,StudentId,Comment,SessionId,Status,CreatedAt,DateApproved")] PgCourseReg pgCourseReg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pgCourseReg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.PgCourses, "Id", "Id", pgCourseReg.CourseId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", pgCourseReg.SessionId);
            ViewData["StudentId"] = new SelectList(_context.PostGraduateStudents, "Id", "Id", pgCourseReg.StudentId);
            return View(pgCourseReg);
        }

        // GET: pgCourseRegs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PgCourseRegs == null)
            {
                return NotFound();
            }

            var pgCourseReg = await _context.PgCourseRegs.FindAsync(id);
            if (pgCourseReg == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.PgCourses, "Id", "Id", pgCourseReg.CourseId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", pgCourseReg.SessionId);
            ViewData["StudentId"] = new SelectList(_context.PostGraduateStudents, "Id", "Id", pgCourseReg.StudentId);
            return View(pgCourseReg);
        }

        // POST: pgCourseRegs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,CourseRegId,CourseId,StudentId,Comment,SessionId,Status,CreatedAt,DateApproved")] PgCourseReg pgCourseReg)
        {
            if (id != pgCourseReg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pgCourseReg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PgCourseRegExists(pgCourseReg.Id))
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
            ViewData["CourseId"] = new SelectList(_context.PgCourses, "Id", "Id", pgCourseReg.CourseId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", pgCourseReg.SessionId);
            ViewData["StudentId"] = new SelectList(_context.PostGraduateStudents, "Id", "Id", pgCourseReg.StudentId);
            return View(pgCourseReg);
        }

        // GET: pgCourseRegs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PgCourseRegs == null)
            {
                return NotFound();
            }

            var pgCourseReg = await _context.PgCourseRegs
                .Include(p => p.Courses)
                .Include(p => p.Sessions)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgCourseReg == null)
            {
                return NotFound();
            }

            return View(pgCourseReg);
        }

        // POST: pgCourseRegs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.PgCourseRegs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PgCourseRegs'  is null.");
            }
            var pgCourseReg = await _context.PgCourseRegs.FindAsync(id);
            if (pgCourseReg != null)
            {
                _context.PgCourseRegs.Remove(pgCourseReg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PgCourseRegExists(int? id)
        {
          return (_context.PgCourseRegs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
