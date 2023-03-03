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
    public class EvaluationQuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: evaluationQuestions
        public async Task<IActionResult> Index()
        {
              return View(await _context.EvaluationQuestions.ToListAsync());
        }

        // GET: evaluationQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EvaluationQuestions == null)
            {
                return NotFound();
            }

            var evaluationQuestion = await _context.EvaluationQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationQuestion == null)
            {
                return NotFound();
            }

            return View(evaluationQuestion);
        }

        // GET: evaluationQuestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: evaluationQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question")] EvaluationQuestion evaluationQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluationQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluationQuestion);
        }

        // GET: evaluationQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EvaluationQuestions == null)
            {
                return NotFound();
            }

            var evaluationQuestion = await _context.EvaluationQuestions.FindAsync(id);
            if (evaluationQuestion == null)
            {
                return NotFound();
            }
            return View(evaluationQuestion);
        }

        // POST: evaluationQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question")] EvaluationQuestion evaluationQuestion)
        {
            if (id != evaluationQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluationQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationQuestionExists(evaluationQuestion.Id))
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
            return View(evaluationQuestion);
        }

        // GET: evaluationQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EvaluationQuestions == null)
            {
                return NotFound();
            }

            var evaluationQuestion = await _context.EvaluationQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationQuestion == null)
            {
                return NotFound();
            }

            return View(evaluationQuestion);
        }

        // POST: evaluationQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EvaluationQuestions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EvaluationQuestions'  is null.");
            }
            var evaluationQuestion = await _context.EvaluationQuestions.FindAsync(id);
            if (evaluationQuestion != null)
            {
                _context.EvaluationQuestions.Remove(evaluationQuestion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationQuestionExists(int? id)
        {
          return _context.EvaluationQuestions.Any(e => e.Id == id);
        }
    }
}
