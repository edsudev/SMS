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

namespace EDSU_SYSTEM.Controllers
{
    public class IctComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public IctComplaintsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: ictComplaint
        public async Task<IActionResult> Index()
        {
            return View(await _context.IctComplaints.ToListAsync());
        }
        public async Task<IActionResult> All()
        {
            return View(await _context.IctComplaints.ToListAsync());
        }
        public async Task<IActionResult> Pending()
        {
            var pending = (from d in _context.IctComplaints where d.Status == Models.Enum.WorksStatus.Pending select d);
            return View(await pending.ToListAsync());
        }

        // GET: IctComplaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IctComplaints == null)
            {
                return NotFound();
            }

            var work = await _context.IctComplaints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, IctComplaint works)
        {
            var work = await _context.IctComplaints.FindAsync(id);

            if (work == null)
                return NotFound();

            if (await TryUpdateModelAsync<IctComplaint>(work, "", c => c.Acknowledgment))
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
        // GET: ictComplaint/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ictComplaint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IctComplaint work)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;

            work.Status = Models.Enum.WorksStatus.Pending;
            _context.IctComplaints.Add(work);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: ictComplaint/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IctComplaints == null)
            {
                return NotFound();
            }

            var work = await _context.IctComplaints.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return View("_edit", work);
        }
        // GET: IctComplaints/Edit/5
        public async Task<IActionResult> Act(int? id)
        {
            if (id == null || _context.IctComplaints == null)
            {
                return NotFound();
            }

            var work = await _context.IctComplaints.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }
            return PartialView("_act", work);
        }

        // POST: IctComplaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var work = await _context.IctComplaints.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {

                var WorkToUpdate = await _context.IctComplaints
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<IctComplaint>(WorkToUpdate, "", c => c.Response, c => c.Status))
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

        // GET: IctComplaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IctComplaints == null)
            {
                return NotFound();
            }

            var work = await _context.IctComplaints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (work == null)
            {
                return NotFound();
            }

            return PartialView("_delete", work);
        }

        // POST: IctComplaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IctComplaints == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IctComplaints'  is null.");
            }
            var work = await _context.IctComplaints.FindAsync(id);
            if (work != null)
            {
                _context.IctComplaints.Remove(work);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(int id)
        {
            return _context.IctComplaints.Any(e => e.Id == id);
        }
    }
}
