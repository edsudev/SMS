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
    public class EntreprenureshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntreprenureshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: entreprenureships
        public async Task<IActionResult> Index()
        {
              return View(await _context.ENTVacancies.ToListAsync());
        }

        // GET: entreprenureships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ENTVacancies == null)
            {
                return NotFound();
            }

            var eNTVacancy = await _context.ENTVacancies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eNTVacancy == null)
            {
                return NotFound();
            }

            return View(eNTVacancy);
        }

        // GET: entreprenureships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: entreprenureships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,TellerNo,FullName,BusinessName,Address,Phone,Email")] ENTVacancy eNTVacancy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eNTVacancy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eNTVacancy);
        }

        // GET: entreprenureships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ENTVacancies == null)
            {
                return NotFound();
            }

            var eNTVacancy = await _context.ENTVacancies.FindAsync(id);
            if (eNTVacancy == null)
            {
                return NotFound();
            }
            return View(eNTVacancy);
        }

        // POST: entreprenureships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,TellerNo,FullName,BusinessName,Address,Phone,Email")] ENTVacancy eNTVacancy)
        {
            if (id != eNTVacancy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eNTVacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ENTVacancyExists(eNTVacancy.Id))
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
            return View(eNTVacancy);
        }

        // GET: entreprenureships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ENTVacancies == null)
            {
                return NotFound();
            }

            var eNTVacancy = await _context.ENTVacancies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eNTVacancy == null)
            {
                return NotFound();
            }

            return View(eNTVacancy);
        }

        // POST: entreprenureships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ENTVacancies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ENTVacancies'  is null.");
            }
            var eNTVacancy = await _context.ENTVacancies.FindAsync(id);
            if (eNTVacancy != null)
            {
                _context.ENTVacancies.Remove(eNTVacancy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ENTVacancyExists(int? id)
        {
          return _context.ENTVacancies.Any(e => e.Id == id);
        }
    }
}
