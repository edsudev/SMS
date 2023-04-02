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
    public class VacancyListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacancyListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: vacancyLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VacancyLists.Include(v => v.Departments).Include(v => v.Positions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: vacancyLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VacancyLists == null)
            {
                return NotFound();
            }

            var vacancyList = await _context.VacancyLists
                .Include(v => v.Departments)
                .Include(v => v.Positions)
                .Include(v => v.Units)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancyList == null)
            {
                return NotFound();
            }

            return View(vacancyList);
        }

        // GET: vacancyLists/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Name");
            return View();
        }

        // POST: vacancyLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VacancyList vacancyList)
        {
           
            _context.Add(vacancyList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }

        // GET: vacancyLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VacancyLists == null)
            {
                return NotFound();
            }

            var vacancyList = await _context.VacancyLists.FindAsync(id);
            if (vacancyList == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", vacancyList.DepartmentId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", vacancyList.PositionId);
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Id", vacancyList.UnitId);
            return View(vacancyList);
        }

        // POST: vacancyLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Type,PositionId,UnitId,DepartmentId,IsActive")] VacancyList vacancyList)
        {
            if (id != vacancyList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacancyList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyListExists(vacancyList.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", vacancyList.DepartmentId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", vacancyList.PositionId);
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Id", vacancyList.UnitId);
            return View(vacancyList);
        }

        // GET: vacancyLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VacancyLists == null)
            {
                return NotFound();
            }

            var vacancyList = await _context.VacancyLists
                .Include(v => v.Departments)
                .Include(v => v.Positions)
                .Include(v => v.Units)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancyList == null)
            {
                return NotFound();
            }

            return View(vacancyList);
        }

        // POST: vacancyLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.VacancyLists == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VacancyLists'  is null.");
            }
            var vacancyList = await _context.VacancyLists.FindAsync(id);
            if (vacancyList != null)
            {
                _context.VacancyLists.Remove(vacancyList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacancyListExists(int? id)
        {
          return _context.VacancyLists.Any(e => e.Id == id);
        }
    }
}
