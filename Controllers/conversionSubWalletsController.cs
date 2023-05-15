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
    public class conversionSubWalletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public conversionSubWalletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: conversionSubWallets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConversionSubWallets.Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Levels).Include(c => c.Sessions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: conversionSubWallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConversionSubWallets == null)
            {
                return NotFound();
            }

            var conversionSubWallet = await _context.ConversionSubWallets
                .Include(c => c.Applicants)
                .Include(c => c.Departments)
                .Include(c => c.Levels)
                .Include(c => c.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionSubWallet == null)
            {
                return NotFound();
            }

            return View(conversionSubWallet);
        }

        // GET: conversionSubWallets/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id");
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");
            return View();
        }

        // POST: conversionSubWallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicantId,WalletId,Name,Pic,RegNo,CreditBalance,Debit,Status,DateCreated,Tuition,FortyPercent,SixtyPercent,LMS,AcceptanceFee,SRC,EDHIS,SessionId,Level,Department")] ConversionSubWallet conversionSubWallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversionSubWallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionSubWallet.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", conversionSubWallet.Department);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", conversionSubWallet.Level);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", conversionSubWallet.SessionId);
            return View(conversionSubWallet);
        }

        // GET: conversionSubWallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConversionSubWallets == null)
            {
                return NotFound();
            }

            var conversionSubWallet = await _context.ConversionSubWallets.FindAsync(id);
            if (conversionSubWallet == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionSubWallet.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", conversionSubWallet.Department);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", conversionSubWallet.Level);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", conversionSubWallet.SessionId);
            return View(conversionSubWallet);
        }

        // POST: conversionSubWallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ApplicantId,WalletId,Name,Pic,RegNo,CreditBalance,Debit,Status,DateCreated,Tuition,FortyPercent,SixtyPercent,LMS,AcceptanceFee,SRC,EDHIS,SessionId,Level,Department")] ConversionSubWallet conversionSubWallet)
        {
            if (id != conversionSubWallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversionSubWallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversionSubWalletExists(conversionSubWallet.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ConversionApplicants, "Id", "Id", conversionSubWallet.ApplicantId);
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", conversionSubWallet.Department);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", conversionSubWallet.Level);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", conversionSubWallet.SessionId);
            return View(conversionSubWallet);
        }

        // GET: conversionSubWallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConversionSubWallets == null)
            {
                return NotFound();
            }

            var conversionSubWallet = await _context.ConversionSubWallets
                .Include(c => c.Applicants)
                .Include(c => c.Departments)
                .Include(c => c.Levels)
                .Include(c => c.Sessions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversionSubWallet == null)
            {
                return NotFound();
            }

            return View(conversionSubWallet);
        }

        // POST: conversionSubWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.ConversionSubWallets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConversionSubWallets'  is null.");
            }
            var conversionSubWallet = await _context.ConversionSubWallets.FindAsync(id);
            if (conversionSubWallet != null)
            {
                _context.ConversionSubWallets.Remove(conversionSubWallet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversionSubWalletExists(int? id)
        {
          return (_context.ConversionSubWallets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
