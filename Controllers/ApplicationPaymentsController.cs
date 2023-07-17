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
    public class ApplicationPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationPayments.Include(a => a.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApplicationPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicationPayments == null)
            {
                return NotFound();
            }

            var applicationPayment = await _context.ApplicationPayments
                .Include(a => a.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationPayment == null)
            {
                return NotFound();
            }

            return View(applicationPayment);
        }

        // GET: ApplicationPayments/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");
            return View();
        }

        // POST: ApplicationPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ref,Amount,Email,Type,ModeOfAdmission,Phone,Firstname,Lastname,Status,SessionId,PaymentDate")] ApplicationPayment applicationPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", applicationPayment.SessionId);
            return View(applicationPayment);
        }

        // GET: ApplicationPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplicationPayments == null)
            {
                return NotFound();
            }

            var applicationPayment = await _context.ApplicationPayments.FindAsync(id);
            if (applicationPayment == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", applicationPayment.SessionId);
            return View(applicationPayment);
        }

        // POST: ApplicationPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ref,Amount,Email,Type,ModeOfAdmission,Phone,Firstname,Lastname,Status,SessionId,PaymentDate")] ApplicationPayment applicationPayment)
        {
            if (id != applicationPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationPaymentExists(applicationPayment.Id))
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
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", applicationPayment.SessionId);
            return View(applicationPayment);
        }

        // GET: ApplicationPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicationPayments == null)
            {
                return NotFound();
            }

            var applicationPayment = await _context.ApplicationPayments
                .Include(a => a.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationPayment == null)
            {
                return NotFound();
            }

            return View(applicationPayment);
        }

        // POST: ApplicationPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicationPayments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicationPayments'  is null.");
            }
            var applicationPayment = await _context.ApplicationPayments.FindAsync(id);
            if (applicationPayment != null)
            {
                _context.ApplicationPayments.Remove(applicationPayment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationPaymentExists(int id)
        {
          return (_context.ApplicationPayments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
