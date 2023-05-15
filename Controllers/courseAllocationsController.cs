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
    public class courseAllocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public courseAllocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: courseAllocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseAllocations.Include(c => c.Courses).Include(c => c.Staff);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: courseAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourseAllocations == null)
            {
                return NotFound();
            }

            var courseAllocation = await _context.CourseAllocations
                .Include(c => c.Courses)
                .Include(c => c.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAllocation == null)
            {
                return NotFound();
            }

            return View(courseAllocation);
        }

        // GET: courseAllocations/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Code");
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "SchoolEmail");
            return View();
        }

        // POST: courseAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseAllocation courseAllocation)
        {
            if (ModelState.IsValid)
            {
                courseAllocation.CreatedAt = DateTime.Now;
                _context.Add(courseAllocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseAllocation.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "Id", courseAllocation.LecturerId);
            return View(courseAllocation);
        }

        // GET: courseAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourseAllocations == null)
            {
                return NotFound();
            }

            var courseAllocation = await _context.CourseAllocations.FindAsync(id);
            if (courseAllocation == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Code", courseAllocation.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "SchoolEmail", courseAllocation.LecturerId);
            return View(courseAllocation);
        }

        // POST: courseAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,LecturerId,CourseLecturerRole,CreatedAt,UpdatedAt")] CourseAllocation courseAllocation)
        {
            if (id != courseAllocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseAllocationExists(courseAllocation.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseAllocation.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "Id", courseAllocation.LecturerId);
            return View(courseAllocation);
        }

        // GET: courseAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourseAllocations == null)
            {
                return NotFound();
            }

            var courseAllocation = await _context.CourseAllocations
                .Include(c => c.Courses)
                .Include(c => c.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseAllocation == null)
            {
                return NotFound();
            }

            return PartialView("_delete",courseAllocation);
        }

        // POST: courseAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourseAllocations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CourseAllocations'  is null.");
            }
            var courseAllocation = await _context.CourseAllocations.FindAsync(id);
            if (courseAllocation != null)
            {
                _context.CourseAllocations.Remove(courseAllocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseAllocationExists(int? id)
        {
          return _context.CourseAllocations.Any(e => e.Id == id);
        }
    }
}
