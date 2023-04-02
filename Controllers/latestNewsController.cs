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
    public class LatestNewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public LatestNewsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: latestNews
        public async Task<IActionResult> Index()
        {
              return View(await _context.LatestNews.ToListAsync());
        }

        // GET: latestNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LatestNews == null)
            {
                return NotFound();
            }

            var latestNews = await _context.LatestNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latestNews == null)
            {
                return NotFound();
            }

            return View(latestNews);
        }

        // GET: latestNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: latestNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img, LatestNews latestNews)
        {
            try
            {
                //if (img != null && img.Length > 0)
                //{
                //    var uploadDir = @"files/magazines/file";
                //    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                //    var extension = Path.GetExtension(img.FileName);
                //    var webRootPath = _hostingEnvironment.WebRootPath;
                //    //fileName = applicants.UTMENumber + extension;

                //    fileName = fileName + extension;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);
                //    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                //    {
                //        img.CopyTo(fs);
                //        // latestNews.pic = fileName;

                //    }
                //}
                latestNews.CreatedAt = DateTime.Now;
                _context.Add(latestNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        // GET: latestNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LatestNews == null)
            {
                return NotFound();
            }

            var latestNews = await _context.LatestNews.FindAsync(id);
            if (latestNews == null)
            {
                return NotFound();
            }
            return View(latestNews);
        }

        // POST: latestNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Body,isActive,CreatedAt")] LatestNews latestNews)
        {
            if (id != latestNews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(latestNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LatestNewsExists(latestNews.Id))
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
            return View(latestNews);
        }

        // GET: latestNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LatestNews == null)
            {
                return NotFound();
            }

            var latestNews = await _context.LatestNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latestNews == null)
            {
                return NotFound();
            }

            return View(latestNews);
        }

        // POST: latestNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.LatestNews == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LatestNews'  is null.");
            }
            var latestNews = await _context.LatestNews.FindAsync(id);
            if (latestNews != null)
            {
                _context.LatestNews.Remove(latestNews);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LatestNewsExists(int? id)
        {
          return _context.LatestNews.Any(e => e.Id == id);
        }
    }
}
