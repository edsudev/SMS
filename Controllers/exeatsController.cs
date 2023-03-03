﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Models;
using EDSU_SYSTEM.Data;
using static EDSU_SYSTEM.Models.Enum;
using System.Drawing.Imaging;
using System.Drawing;
using QRCoder;
using Microsoft.AspNetCore.Identity;

namespace EDSU_SYSTEM.Controllers
{
    public class exeatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public exeatsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: exeats
        public async Task<IActionResult> Index()
        {
            //var parentNo = (from s in _context.Students
            return View(await _context.Exeats.ToListAsync());
        }
        // GET: exeats
        public async Task<IActionResult> History()
        {
            //var StudenExeats = (from f in _context.Exeats where f.)
              return View(await _context.Exeats.ToListAsync());
        }

        // GET: exeats/Details/5
        public async Task<IActionResult> Act(string? id)
        {
            if (id == null || _context.Exeats == null)
            {
                return NotFound();
            }
            var exeat = (from e in _context.Exeats where e.ExeatId == id select e).Include(h => h.Hostels).FirstOrDefault();
            if (exeat == null)
            {
                return NotFound();
            }

            return View(exeat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Act(int? id, MainStatus status, ChiefPortalStatus cheifPortalStatus)
        {
            var exeat = await _context.Exeats.FindAsync(id);

            if (exeat == null)
                return NotFound();
            exeat.Dean = status;
            exeat.HallMasterStatus = status;
            exeat.ChiefPortalStatus = cheifPortalStatus;
            _context.Exeats.Update(exeat);

            await _context.SaveChangesAsync();

            return View(exeat);
        }
        // GET: exeats/Create
        public IActionResult Apply()
        {
            ViewData["HallId"] = new SelectList(_context.Hostels, "Id", "Name");
            return View();
        }

        // POST: exeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply([Bind("Id,ExeatId,Name,Email,MatNo,Hall,Department,Destination,NoOfDays,DepartureDate,ArrivalDate,Reason,Phone,ParentPhone,AltParentPhone,ChiefPortalStatus,HallMasterStatus,Dean,GatePass,CreatedAt")] Exeat exeat)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var studentId = loggedInUser.StudentsId;
                var students = (from s in _context.Students where s.Id == studentId select s).ToList();
                foreach (var item in students)
                {
                    exeat.Name = item.Fullname;
                    exeat.Department = item.Departments.Name;
                    exeat.Email = item.Email;
                    exeat.MatNo = item.MatNumber;
                    exeat.AltParentPhone = item.ParentPhone;
                    exeat.ExeatId = Guid.NewGuid().ToString();
                }
                exeat.NoOfDays = (exeat.ArrivalDate.Date - exeat.DepartureDate.Date).Days;
                _context.Add(exeat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                ViewData["HallId"] = new SelectList(_context.Hostels, "Id", "Name", exeat.Hall);

            return View(exeat);
        }
        public async Task<IActionResult> Pass(string? id)
        {
            var exeat = await _context.Exeats.FirstOrDefaultAsync(e => e.ExeatId == id);
            string exeatId;
            if (exeat.ExeatId != null)
            {
                exeatId = "EDSU-PASS " + exeat.ExeatId;
                //Only the 1.4.1 Version of the QRCode package works fine.
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(exeatId.ToString(), QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qRCodeData);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Bitmap bitmap = qrCode.GetGraphic(20))
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                        ViewBag.BookingId = exeatId;
                    }
                }
            }


            return View();
        }
        // GET: exeats/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Exeats == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Hostels, "Id", "Name");
            var exeat = await _context.Exeats.FirstOrDefaultAsync(e => e.ExeatId == id);
            if (exeat == null)
            {
                return NotFound();
            }
            return View(exeat);
        }

        // POST: exeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ExeatId,Name,Email,MatNo,Hall,Department,Destination,NoOfDays,DepartureDate,ArrivalDate,Reason,Phone,ParentPhone,AltParentPhone,ChiefPortalStatus,HallMasterStatus,Dean,GatePass,CreatedAt")] Exeat exeat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExeatExists(exeat.Id))
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
            return View(exeat);
        }

        // GET: exeats/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            var exeat = await _context.Exeats
                .FirstOrDefaultAsync(m => m.ExeatId == id);
            if (exeat == null)
            {
                return NotFound();
            }

            return View(exeat);
        }

        // POST: exeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (_context.Exeats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Exeats'  is null.");
            }
            var exeat = await _context.Exeats.FirstOrDefaultAsync(e => e.ExeatId == id);
            if (exeat != null)
            {
                _context.Exeats.Remove(exeat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExeatExists(int? id)
        {
          return _context.Exeats.Any(e => e.Id == id);
        }
    }
}
