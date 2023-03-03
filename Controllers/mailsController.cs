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

namespace EDSU_SYSTEM.Controllers
{
    public class MailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MailsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: mails
        public async Task<IActionResult> Index()
        {
              return View(await _context.Mails.ToListAsync());
        } 
        public async Task<IActionResult> Important()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StaffId;
            var staffEmail = (from s in _context.Staffs where s.Id == user select s.SchoolEmail).FirstOrDefault();
            var importantMails = (from m in _context.Mails where m.From == staffEmail select m);
              return View(await importantMails.ToListAsync());
        } 
        public async Task<IActionResult> Sent()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StaffId;
            var staffEmail = (from s in _context.Staffs where s.Id == user select s.SchoolEmail).FirstOrDefault();
            var sentMails = (from m in _context.Mails where m.From == staffEmail select m).Include(c => c.StaffTo);
              return View(await sentMails.ToListAsync());
        } 
        public async Task<IActionResult> Spam()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StaffId;
            var staffEmail = (from s in _context.Staffs where s.Id == user select s.SchoolEmail).FirstOrDefault();
            var sentMails = (from m in _context.Mails where m.From == staffEmail select m).Include(c => c.StaffTo);
              return View(await sentMails.ToListAsync());
        } 
        public async Task<IActionResult> Inbox()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StaffId;
            var staffEmail = (from s in _context.Staffs where s.Id == user select s.Id).FirstOrDefault();
            var incomingMails = (from m in _context.Mails where m.To == staffEmail select m);
            return View(await incomingMails.ToListAsync());
        }public async Task<IActionResult> Draft()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StaffId;
            var staffEmail = (from s in _context.Staffs where s.Id == user select s.Id).FirstOrDefault();
            var incomingMails = (from m in _context.Mails where m.To == staffEmail select m);
            return View(await incomingMails.ToListAsync());
        }
        public async Task<IActionResult> Trash()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = loggedInUser.StaffId;
            var staffEmail = (from s in _context.Staffs where s.Id == user select s.Id).FirstOrDefault();
            var incomingMails = (from m in _context.Mails where m.To == staffEmail select m);
            return View(await incomingMails.ToListAsync());
        }

        // GET: mails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mails == null)
            {
                return NotFound();
            }
            var mail = (from m in _context.Mails where m.Id == id select m)
                .Include(c => c.StaffThrough).Include(c => c.StaffThrough2)
                .Include(c => c.StaffThrough3).FirstOrDefault();
            //var mail = await _context.Mails
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (mail == null)
            {
                return NotFound();
            }

            return View(mail);
        }

        // GET: mails/Create
        public async Task<IActionResult> CreateAsync()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StaffId;
            var userEmail = (from c in _context.Staffs where c.Id == userId select c).ToList();
            foreach (var item in userEmail)
            {
                ViewBag.email = item.SchoolEmail;
                TempData["email"] = ViewBag.email;
            }
            ViewData["LecturerId"] = new SelectList(_context.Staffs, "Id", "Name");
            return View();
        }

        // POST: mails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mail mail)
        {
            if (ModelState.IsValid)
            {
                mail.From = (string?)TempData["email"]; 
                mail.CreatedAt = DateTime.Now;
                _context.Add(mail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mail);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Draft(Mail mail)
        {
            if (ModelState.IsValid)
            {
                mail.From = (string?)TempData["email"]; 
                mail.CreatedAt = DateTime.Now;
                mail.Draft = true;
                _context.Add(mail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mail);
        }

        // GET: mails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mails == null)
            {
                return NotFound();
            }

            var mail = await _context.Mails.FindAsync(id);
            if (mail == null)
            {
                return NotFound();
            }
            return View(mail);
        }

        // POST: mails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,From,To,CC,Subject,File,Status,Draft,IsImportant,Message,CreatedAt,UpdateDate")] Mail mail)
        {
            if (id != mail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MailExists(mail.Id))
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
            return View(mail);
        }

        // GET: mails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mails == null)
            {
                return NotFound();
            }

            var mail = await _context.Mails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mail == null)
            {
                return NotFound();
            }

            return View(mail);
        }

        // POST: mails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mail'  is null.");
            }
            var mail = await _context.Mails.FindAsync(id);
            if (mail != null)
            {
                _context.Mails.Remove(mail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }
        private bool MailExists(int? id)
        {
          return _context.Mails.Any(e => e.Id == id);
        }
    }
}
