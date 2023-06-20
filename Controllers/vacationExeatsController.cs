using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using static EDSU_SYSTEM.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace EDSU_SYSTEM.Controllers
{
    public class vacationExeatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public vacationExeatsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: vacationExeats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VacationExeats.Include(v => v.Hostels);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: vacationExeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VacationExeats == null)
            {
                return NotFound();
            }

            var vacationExeat = await _context.VacationExeats
                .Include(v => v.Hostels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacationExeat == null)
            {
                return NotFound();
            }

            return View(vacationExeat);
        }
        [Authorize(Roles = "student, superAdmin")]
        // GET: vacationExeats/Create
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(_context.Hostels, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "student, superAdmin")]
        // POST: vacationExeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VacationExeat exeat)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var studentId = loggedInUser.StudentsId;
            var student = (from s in _context.Students where s.Id == studentId select s).Include(i => i.Departments).FirstOrDefault();

            exeat.Name = student.Fullname;
            exeat.Department = student.Departments.Name;
            exeat.Email = student.SchoolEmailAddress;
            exeat.MatNo = student.MatNumber;
            exeat.AltParentPhone = student.ParentPhone;
            exeat.ExeatId = Guid.NewGuid().ToString();
            exeat.ChiefPortalStatus = ChiefPortalStatus.Pending;
            exeat.HallMasterStatus = MainStatus.Pending;
            exeat.Dean = MainStatus.Pending;
            exeat.GatePass = MainStatus.Pending;
            
            _context.Add(exeat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(History));

        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> History()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var studentId = loggedInUser.StudentsId;
            var email = _context.Students.Where(x => x.Id == studentId).Select(x => x.SchoolEmailAddress).First();
            var StudentExeats = (from f in _context.VacationExeats where f.Email == email select f).ToList();
            return View(StudentExeats);
        }
        // GET: vacationExeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VacationExeats == null)
            {
                return NotFound();
            }

            var vacationExeat = await _context.VacationExeats.FindAsync(id);
            if (vacationExeat == null)
            {
                return NotFound();
            }
            ViewData["Hall"] = new SelectList(_context.Hostels, "Id", "Id", vacationExeat.Hall);
            return View(vacationExeat);
        }

        // POST: vacationExeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ExeatId,Name,Email,MatNo,Hall,Department,Destination,DepartureDate,Reason,Phone,ParentPhone,AltParentPhone,ChiefPortalStatus,HallMasterStatus,Dean,GatePass,CreatedAt")] VacationExeat vacationExeat)
        {
            if (id != vacationExeat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationExeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExeatExists(vacationExeat.Id))
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
            ViewData["Hall"] = new SelectList(_context.Hostels, "Id", "Id", vacationExeat.Hall);
            return View(vacationExeat);
        }

        // GET: vacationExeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VacationExeats == null)
            {
                return NotFound();
            }

            var vacationExeat = await _context.VacationExeats
                .Include(v => v.Hostels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacationExeat == null)
            {
                return NotFound();
            }

            return View(vacationExeat);
        }

        // POST: vacationExeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.VacationExeats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VacationExeats'  is null.");
            }
            var vacationExeat = await _context.VacationExeats.FindAsync(id);
            if (vacationExeat != null)
            {
                _context.VacationExeats.Remove(vacationExeat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationExeatExists(int? id)
        {
          return (_context.VacationExeats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
