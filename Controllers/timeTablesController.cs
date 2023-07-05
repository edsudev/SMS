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
    public class TimeTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: timeTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TimeTables
            .Include(t => t.Courses).ThenInclude(t=>t.Courses)
            .Include(t => t.Staffs).ToList();
            
            return View(applicationDbContext);
        }

        // GET: timeTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeTables == null)
            {
                return NotFound();
            }

            var timeTable = await _context.TimeTables
                .Include(t => t.Courses)
                .Include(t => t.Staffs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeTable == null)
            {
                return NotFound();
            }

            return View(timeTable);
        }

        // GET: timeTables/Create
        public IActionResult Create()
        {
            var courses = (from d in _context.CourseAllocations select d.Courses).ToList();
            var lecturer = (from d in _context.CourseAllocations select d.Staff).ToList();
           
            ViewData["CourseId"] = new SelectList(courses, "Id", "Title"); 
            ViewData["LecturerId"] = new SelectList(lecturer, "Id", "SchoolEmail");
            return View();
        }

        // POST: timeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
                timeTable.TimeTableId = Guid.NewGuid().ToString();
                timeTable.CreatedAt = DateTime.Now;
                _context.Add(timeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.CourseAllocations, "Id", "Id", timeTable.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Staffs.Where(x => x.Type == 5), "Id", "Name", timeTable.LecturerId);
            return View(timeTable);
        }

        // GET: timeTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeTables == null)
            {
                return NotFound();
            }

            var timeTable = await _context.TimeTables.FindAsync(id);
            if (timeTable == null)
            {
                return NotFound();
            }
            var s = (from d in _context.CourseAllocations select d.Courses).ToList();
            var lecturer = (from d in _context.CourseAllocations select d.Staff).ToList();

            ViewData["CourseId"] = new SelectList(s, "Id", "Title");
            ViewData["LecturerId"] = new SelectList(lecturer, "Id", "SchoolEmail");
            return View(timeTable);
        }

        // POST: timeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimeTable timeTable)
        {
            if (id != timeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeTableExists(timeTable.Id))
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
            ViewData["CourseId"] = new SelectList(_context.CourseAllocations, "Id", "Id", timeTable.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.CourseAllocations, "Id", "Id", timeTable.LecturerId);
            return View(timeTable);
        }

        // GET: timeTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeTables == null)
            {
                return NotFound();
            }

            var timeTable = await _context.TimeTables
                .Include(t => t.Courses)
                .Include(t => t.Staffs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeTable == null)
            {
                return NotFound();
            }

            return View(timeTable);
        }

        // POST: timeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TimeTables'  is null.");
            }
            var timeTable = await _context.TimeTables.FindAsync(id);
            if (timeTable != null)
            {
                _context.TimeTables.Remove(timeTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeTableExists(int? id)
        {
          return _context.TimeTables.Any(e => e.Id == id);
        }
    }
}
