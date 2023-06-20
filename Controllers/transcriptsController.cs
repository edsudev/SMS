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
    public class TranscriptsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TranscriptsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: transcripts
        public async Task<IActionResult> Index()
        {
              return View(await _context.Transcripts.ToListAsync());
        }
        [Authorize(Roles = "student, superAdmin")]
        public async Task<IActionResult> History()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            var userTranscripts = (from c in _context.Transcripts where c.UserId == userId.ToString() select c);
            return View(await userTranscripts.ToListAsync());
        }

        // GET: transcripts/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Transcripts == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts
                .FirstOrDefaultAsync(m => m.TranscriptId == id);
            if (transcript == null)
            {
                return NotFound();
            }

            return View(transcript);
        }
        [Authorize(Roles = "student, superAdmin")]
        public IActionResult Requirements()
        {
            return View();
        }
        [Authorize(Roles = "student, superAdmin")]
        public IActionResult Apply()
        {

            ViewData["programme"] = new SelectList(_context.Programs, "Id", "NameOfProgram");
            return View();
        }
        [Authorize(Roles = "student, superAdmin")]
        // POST: transcripts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(Transcript transcript)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
           
            transcript.TranscriptId = Guid.NewGuid().ToString();
            transcript.UserId = userId.ToString();
            _context.Add(transcript);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(History));
            
        }
        [Authorize(Roles = "student, superAdmin")]
        // GET: transcripts/Edit/5
        public IActionResult Edit(string? id)
        {
            if (id == null || _context.Transcripts == null)
            {
                return NotFound();
            }

            var transcript = _context.Transcripts.FirstOrDefault(d => d.TranscriptId == id);
            
            ViewData["programme"] = new SelectList(_context.Programs, "Id", "NameOfProgram");
            if (transcript == null)
            {
                return NotFound();
            }
            return View(transcript);
        }

        // POST: transcripts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Transcript transcript)
        {
            if (id != transcript.TranscriptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transcript);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranscriptExists(transcript.TranscriptId))
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
            return View(transcript);
        }

        // GET: transcripts/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Transcripts == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts
                .FirstOrDefaultAsync(m => m.TranscriptId == id);
            if (transcript == null)
            {
                return NotFound();
            }

            return View(transcript);
        }

        // POST: transcripts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (_context.Transcripts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transcripts'  is null.");
            }
            var transcript = await _context.Transcripts.FirstOrDefaultAsync(i => i.TranscriptId == id);
            if (transcript != null)
            {
                _context.Transcripts.Remove(transcript);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranscriptExists(string? id)
        {
          return _context.Transcripts.Any(e => e.TranscriptId == id);
        }
    }
}
