using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using EDSU_SYSTEM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EDSU_SYSTEM.Controllers
{
    [Authorize]
    public class worksController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public worksController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: works
        public async Task<IActionResult> Index()
        {
              return View(await _context.Works.ToListAsync());
        } 
        public async Task<IActionResult> All()
        {
              return View(await _context.Works.ToListAsync());
        }
        public async Task<IActionResult> Pending()
        {
            var pending = (from d in _context.Works where d.Status == Models.Enum.WorksStatus.Pending select d);
              return View(await pending.ToListAsync());
        }

        // GET: works/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, Work works)
        {
            var work = await _context.Works.FindAsync(id);

            if (work == null)
                return NotFound();

            if (await TryUpdateModelAsync<Work>(work, "", c => c.Acknowledgment))
            {
                try
                {
                    //work.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        // GET: works/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Work work)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;

            work.Status = Models.Enum.WorksStatus.Pending;
            _context.Works.Add(work);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
          
        }
        
        // GET: works/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return PartialView("_edit", work);
        }
        // GET: works/Edit/5
        public async Task<IActionResult> Act(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return PartialView("_act",work);
        }
        public async Task<IActionResult> Acknowledge(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return PartialView("_acknowledge",work);
        }

        // POST: works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acknowledge(int id)
        {
            var work = await _context.Works.FindAsync(id);
            try
            {
                var WorkToUpdate = await _context.Works
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Work>(WorkToUpdate, "", c => c.Acknowledgment))
                {
                    try
                    {
                        work.UpdatedAt = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var work = await _context.Works.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {

                var WorkToUpdate = await _context.Works
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<Work>(WorkToUpdate, "", c=>c.Location, c=> c.Complain, c => c.Response, c => c.Status))
                {
                    try
                    {
                        work.UpdatedAt = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction(nameof(Index));
                }



                //Context.Update(applicant);
                //await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return View();

        }

        // GET: works/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Works == null)
            {
                return NotFound();
            }

            var work = await _context.Works
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return PartialView("_delete",work);
        }

        // POST: works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Works == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Works'  is null.");
            }
            var work = await _context.Works.FindAsync(id);
            if (work != null)
            {
                _context.Works.Remove(work);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(int id)
        {
          return _context.Works.Any(e => e.Id == id);
        }
    }
}
