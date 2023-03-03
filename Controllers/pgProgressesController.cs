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
    public class PgProgressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public PgProgressesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: pgProgresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PgProgresses.Include(i => i.Programs).
                Include(i => i.Students).ThenInclude(i => i.Students);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: pgProgresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PgProgresses == null)
            {
                return NotFound();
            }

            var pgProgress = await _context.PgProgresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgProgress == null)
            {
                return NotFound();
            }

            return View(pgProgress);
        }

        // GET: pgProgresses/Create
        public async Task<IActionResult> Create()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = loggedInUser.StudentsId;
            var s = (from d in _context.PgStudentsSupervisors where d.Student == userId select d.Lecturers).ToList();
            ViewData["Supervisor"] = new SelectList(s, "Id", "Name");
            return View();
        }

        // POST: pgProgresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, PgProgress pgProgress)
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
           
                pgProgress.StudentId = loggedInUser.StudentsId;
                if (file != null && file.Length > 0)
                {
                    var uploadDir = @"files/pgProgress";
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                        pgProgress.FileName = fileName;
                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                        }
                    }

                }
                _context.Add(pgProgress);

                await _context.SaveChangesAsync();
            ViewData["Supervisor"] = new SelectList(_context.PgStudentsSupervisors, "Id", "Name", pgProgress.SupervisorId);
            return RedirectToAction(nameof(Index));
        }


        // GET: pgProgresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PgProgresses == null)
            {
                return NotFound();
            }

            var pgProgress = await _context.PgProgresses.FindAsync(id);
            if (pgProgress == null)
            {
                return NotFound();
            }
            return View(pgProgress);
        }

        // POST: pgProgresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var pgprogress = await _context.PgProgresses.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var PgProgressToUpdate = await _context.PgProgresses
                .FirstOrDefaultAsync(c => c.Id == id);

                if (await TryUpdateModelAsync<PgProgress>(PgProgressToUpdate, "", c => c.Ranking, c => c.Comment))
                {
                    try
                    {
                        pgprogress.UpdatedAt = DateTime.Now;
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
        // GET: pgProgresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PgProgresses == null)
            {
                return NotFound();
            }

            var pgProgress = await _context.PgProgresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgProgress == null)
            {
                return NotFound();
            }

            return View(pgProgress);
        }

        // POST: pgProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PgProgresses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PgProgresses'  is null.");
            }
            var pgProgress = await _context.PgProgresses.FindAsync(id);
            if (pgProgress != null)
            {
                _context.PgProgresses.Remove(pgProgress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public FileResult DownloadFile(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "files/pgProgress/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        private bool PgProgressExists(int id)
        {
          return _context.PgProgresses.Any(e => e.Id == id);
        }
    }
}
