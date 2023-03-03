﻿using System;
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
    public class ProjectProgressController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public ProjectProgressController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: projectProgress
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UgProgresses.Include(u => u.Programs).Include(u => u.Students).Include(u => u.Supervisors);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> History()
        {
            var applicationDbContext = _context.UgProgresses.Include(u => u.Programs).Include(u => u.Students).Include(u => u.Supervisors);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: projectProgress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UgProgresses == null)
            {
                return NotFound();
            }

            var ugProgress = await _context.UgProgresses
                .Include(u => u.Programs)
                .Include(u => u.Students)
                .Include(u => u.Supervisors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ugProgress == null)
            {
                return NotFound();
            }

            return View(ugProgress);
        }

        // GET: projectProgress/Create
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            var supervior = (from s in _context.UgStudentSupervisors where s.Student == userId select s.Lecturers.Name).ToList();
            ViewData["SupervisorId"] = new SelectList(supervior);
            return View();
        }

        // POST: projectProgress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,StudentId,Title,Program,FileName,SupervisorId,Ranking,Comment,CreatedAt,UpdatedAt")] UgProgress ugProgress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ugProgress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Program"] = new SelectList(_context.Programs, "Id", "Id", ugProgress.Program);
            ViewData["StudentId"] = new SelectList(_context.UgStudentSupervisors, "Id", "Id", ugProgress.StudentId);
            ViewData["SupervisorId"] = new SelectList(_context.UgStudentSupervisors, "Id", "Id", ugProgress.SupervisorId);
            return View(ugProgress);
        }

        // GET: projectProgress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UgProgresses == null)
            {
                return NotFound();
            }

            var ugProgress = await _context.UgProgresses.FindAsync(id);
            if (ugProgress == null)
            {
                return NotFound();
            }
            ViewData["Program"] = new SelectList(_context.Programs, "Id", "Id", ugProgress.Program);
            ViewData["StudentId"] = new SelectList(_context.UgStudentSupervisors, "Id", "Id", ugProgress.StudentId);
            ViewData["SupervisorId"] = new SelectList(_context.UgStudentSupervisors, "Id", "Id", ugProgress.SupervisorId);
            return View(ugProgress);
        }

        // POST: projectProgress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,StudentId,Title,Program,FileName,SupervisorId,Ranking,Comment,CreatedAt,UpdatedAt")] UgProgress ugProgress)
        {
            if (id != ugProgress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ugProgress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UgProgressExists(ugProgress.Id))
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
            ViewData["Program"] = new SelectList(_context.Programs, "Id", "Id", ugProgress.Program);
            ViewData["StudentId"] = new SelectList(_context.UgStudentSupervisors, "Id", "Id", ugProgress.StudentId);
            ViewData["SupervisorId"] = new SelectList(_context.UgStudentSupervisors, "Id", "Id", ugProgress.SupervisorId);
            return View(ugProgress);
        }

        // GET: projectProgress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UgProgresses == null)
            {
                return NotFound();
            }

            var ugProgress = await _context.UgProgresses
                .Include(u => u.Programs)
                .Include(u => u.Students)
                .Include(u => u.Supervisors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ugProgress == null)
            {
                return NotFound();
            }

            return View(ugProgress);
        }

        // POST: projectProgress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UgProgresses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UgProgresses'  is null.");
            }
            var ugProgress = await _context.UgProgresses.FindAsync(id);
            if (ugProgress != null)
            {
                _context.UgProgresses.Remove(ugProgress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UgProgressExists(int? id)
        {
          return _context.UgProgresses.Any(e => e.Id == id);
        }
    }
}
