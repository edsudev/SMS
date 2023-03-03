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
    public class ParentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: parents
        public IActionResult GetStarted()
        {
              return View();
        }
        // POST: parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetStarted(string id)
        {
            var transactionID = (from v in _context.Payments where v.Ref == id && v.Status == "approved" select v).FirstOrDefault();
            if (transactionID == null)
            {
                return NotFound();
            }
            else
            {
                var HasBeenUsed = (from s in _context.Parent where s.PaymentId == id select s).FirstOrDefault();
                if (HasBeenUsed != null)
                {
                    return BadRequest();
                }
                else
                {
                    return RedirectToAction("create", "parents");
                }
                
            }
            
        }
        public async Task<IActionResult> Index()
        {
              return View(await _context.Parent.ToListAsync());
        }

        // GET: parents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // GET: parents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: parents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,Title,Name,Phone,Address,Email,CreatedAt")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                parent.ParentId = Guid.NewGuid().ToString();
                parent.CreatedAt = DateTime.Now;
                _context.Add(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parent);
        }

        // GET: parents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: parents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,Title,Name,Phone,Address,Email,CreatedAt")] Parent parent)
        {
            if (id != parent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(parent.Id))
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
            return View(parent);
        }

        // GET: parents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parent'  is null.");
            }
            var parent = await _context.Parent.FindAsync(id);
            if (parent != null)
            {
                _context.Parent.Remove(parent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentExists(int? id)
        {
          return _context.Parent.Any(e => e.Id == id);
        }
    }
}
