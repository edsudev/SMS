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
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ActivitiesController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: activities
        public async Task<IActionResult> Index()
        {
              return View(await _context.Activities.ToListAsync());
        }

        // GET: activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // GET: activities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile image, Activities activities)
        {
            try
            {
                if (image != null && image.Length > 0)
                {
                    var uploadDir = @"files/activities";
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = applicants.UTMENumber + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        image.CopyTo(fs);
                        activities.Image = fileName;

                    }
                }

                _context.Add(activities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities.FindAsync(id);
            if (activities == null)
            {
                return NotFound();
            }
            return View(activities);
        }

        // POST: activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, IFormFile image, Activities activities)
        {
            if (id != activities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        var uploadDir = @"files/activities";
                        var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        var extension = Path.GetExtension(image.FileName);
                        var webRootPath = _hostingEnvironment.WebRootPath;
                        //fileName = applicants.UTMENumber + extension;
                        fileName = fileName + extension;
                        var path = Path.Combine(webRootPath, uploadDir, fileName);
                        using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                        {
                            image.CopyTo(fs);
                            activities.Image = fileName;

                        }
                    }
                    _context.Update(activities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivitiesExists(activities.Id))
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
            return View(activities);
        }

        // GET: activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activities = await _context.Activities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activities == null)
            {
                return NotFound();
            }

            return View(activities);
        }

        // POST: activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Activities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Activities'  is null.");
            }
            var activities = await _context.Activities.FindAsync(id);
            if (activities != null)
            {
                _context.Activities.Remove(activities);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivitiesExists(int? id)
        {
          return _context.Activities.Any(e => e.Id == id);
        }
    }
}
