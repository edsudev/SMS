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
    public class EvaluationGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: evaluationGrades
        public async Task<IActionResult> Index()
        {
              return View(await _context.EvaluationGrades.ToListAsync());
        }

        // GET: evaluationGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EvaluationGrades == null)
            {
                return NotFound();
            }

            var evaluationGrade = await _context.EvaluationGrades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationGrade == null)
            {
                return NotFound();
            }

            return View(evaluationGrade);
        }

        // GET: evaluationGrades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: evaluationGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Grade,Value")] EvaluationGrade evaluationGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluationGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluationGrade);
        }

        // GET: evaluationGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EvaluationGrades == null)
            {
                return NotFound();
            }

            var evaluationGrade = await _context.EvaluationGrades.FindAsync(id);
            if (evaluationGrade == null)
            {
                return NotFound();
            }
            return View(evaluationGrade);
        }

        // POST: evaluationGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Grade,Value")] EvaluationGrade evaluationGrade)
        {
            if (id != evaluationGrade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluationGrade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationGradeExists(evaluationGrade.Id))
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
            return View(evaluationGrade);
        }

        // GET: evaluationGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EvaluationGrades == null)
            {
                return NotFound();
            }

            var evaluationGrade = await _context.EvaluationGrades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationGrade == null)
            {
                return NotFound();
            }

            return View(evaluationGrade);
        }

        // POST: evaluationGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EvaluationGrades == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EvaluationGrades'  is null.");
            }
            var evaluationGrade = await _context.EvaluationGrades.FindAsync(id);
            if (evaluationGrade != null)
            {
                _context.EvaluationGrades.Remove(evaluationGrade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationGradeExists(int? id)
        {
          return _context.EvaluationGrades.Any(e => e.Id == id);
        }
    }
}
