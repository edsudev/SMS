using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using EDSU_SYSTEM.Data;

namespace EDSU_SYSTEM.Controllers
{
    public class AdmissionLettersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdmissionLettersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: admissionLetters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdmissionLetters.Include(a => a.Departments).Include(a => a.Faculties);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: admissionLetters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdmissionLetters == null)
            {
                return NotFound();
            }

            var admissionLetter = await _context.AdmissionLetters
                .Include(a => a.Departments)
                .Include(a => a.Faculties)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admissionLetter == null)
            {
                return NotFound();
            }

            return View(admissionLetter);
        }

        // GET: admissionLetters/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id");
            return View();
        }

        // POST: admissionLetters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Header,Body,Var1,Var2,Var3,Note,Registrar,Ref,CreatedAt,DepartmentId,FacultyId")] AdmissionLetter admissionLetter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admissionLetter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", admissionLetter.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", admissionLetter.FacultyId);
            return View(admissionLetter);
        }

        // GET: admissionLetters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdmissionLetters == null)
            {
                return NotFound();
            }

            var admissionLetter = await _context.AdmissionLetters.FindAsync(id);
            if (admissionLetter == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", admissionLetter.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", admissionLetter.FacultyId);
            return View(admissionLetter);
        }

        // POST: admissionLetters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Header,Body,Var1,Var2,Var3,Note,Registrar,Ref,CreatedAt,DepartmentId,FacultyId")] AdmissionLetter admissionLetter)
        {
            if (id != admissionLetter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admissionLetter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionLetterExists(admissionLetter.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", admissionLetter.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", admissionLetter.FacultyId);
            return View(admissionLetter);
        }

        // GET: admissionLetters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdmissionLetters == null)
            {
                return NotFound();
            }

            var admissionLetter = await _context.AdmissionLetters
                .Include(a => a.Departments)
                .Include(a => a.Faculties)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admissionLetter == null)
            {
                return NotFound();
            }

            return View(admissionLetter);
        }

        // POST: admissionLetters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdmissionLetters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AdmissionLetters'  is null.");
            }
            var admissionLetter = await _context.AdmissionLetters.FindAsync(id);
            if (admissionLetter != null)
            {
                _context.AdmissionLetters.Remove(admissionLetter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionLetterExists(int? id)
        {
          return _context.AdmissionLetters.Any(e => e.Id == id);
        }
    }
}
