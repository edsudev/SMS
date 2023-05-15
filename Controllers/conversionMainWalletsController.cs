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
    public class conversionMainWalletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public conversionMainWalletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: conversionMainWallets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConversionMainWallets.Include(c => c.Applicants);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: conversionMainWallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConversionMainWallets == null)
            {
                return NotFound();
            }

            var conversionMainWallet = await _context.ConversionMainWallets
                .Include(c => c.Applicants)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionMainWallet == null)
            {
                return NotFound();
            }

            return View(conversionMainWallet);
        }

        // GET: conversionMainWallets/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id");
            return View();
        }

        // POST: conversionMainWallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicantId,WalletId,Name,CreditBalance,BulkDebitBalanace,Status,DateCreated,DateUpdated")] ConversionMainWallet conversionMainWallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversionMainWallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionMainWallet.ApplicantId);
            return View(conversionMainWallet);
        }

        // GET: conversionMainWallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConversionMainWallets == null)
            {
                return NotFound();
            }

            var conversionMainWallet = await _context.ConversionMainWallets.FindAsync(id);
            if (conversionMainWallet == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionMainWallet.ApplicantId);
            return View(conversionMainWallet);
        }

        // POST: conversionMainWallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ApplicantId,WalletId,Name,CreditBalance,BulkDebitBalanace,Status,DateCreated,DateUpdated")] ConversionMainWallet conversionMainWallet)
        {
            if (id != conversionMainWallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversionMainWallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversionMainWalletExists(conversionMainWallet.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionMainWallet.ApplicantId);
            return View(conversionMainWallet);
        }

        // GET: conversionMainWallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConversionMainWallets == null)
            {
                return NotFound();
            }

            var conversionMainWallet = await _context.ConversionMainWallets
                .Include(c => c.Applicants)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionMainWallet == null)
            {
                return NotFound();
            }

            return View(conversionMainWallet);
        }

        // POST: conversionMainWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.ConversionMainWallets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConversionMainWallets'  is null.");
            }
            var conversionMainWallet = await _context.ConversionMainWallets.FindAsync(id);
            if (conversionMainWallet != null)
            {
                _context.ConversionMainWallets.Remove(conversionMainWallet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversionMainWalletExists(int? id)
        {
          return (_context.ConversionMainWallets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
