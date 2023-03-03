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
    public class oerConferencePapersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public oerConferencePapersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: oerConferencePapers
        public async Task<IActionResult> Index()
        {
              return View(await _context.OerConferencePapers.ToListAsync());
        }
        public async Task<IActionResult> MyUploads()
        {
              return View(await _context.OerConferencePapers.ToListAsync());
        }
        // Creating a modal form to display abstact
        public async Task<IActionResult> Abstract(int? id)
        {
            var article = await _context.OerConferencePapers.FindAsync(id);
            ViewBag.Abstract = article.Abstract;
            return PartialView("_Abstract");
        }
        // GET: oerConferencePapers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OerConferencePapers == null)
            {
                return NotFound();
            }

            var oerConferencePaper = await _context.OerConferencePapers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerConferencePaper == null)
            {
                return NotFound();
            }

            return View(oerConferencePaper);
        }

        // GET: oerConferencePapers/Create
        public IActionResult Create()
        {
            return PartialView("_add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile fullArticle, OerConferencePaper oerConferencePaper)
        {

            try
            {
               // var uploader = UserManager.GetUserId(User);
                _context.Add(oerConferencePaper);


                if (fullArticle != null && fullArticle.Length > 0)
                {
                    var uploadDir = @"files/OER/conference";
                    var fileName = Path.GetFileNameWithoutExtension(fullArticle.FileName);
                    var extension = Path.GetExtension(fullArticle.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        fullArticle.CopyTo(fs);
                        oerConferencePaper.Upload = fileName;
                    }

                }
                await TryUpdateModelAsync<OerConferencePaper>(oerConferencePaper);               //    c => c.Ssce1, c => c.Ssce2, c => c.BirthCertificate, c => c.DirectEntryUpload, c => c.LGAUpload))

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {

                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction(nameof(Index));
        }
        // GET: OerConferencePapers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OerConferencePapers == null)
            {
                return NotFound();
            }

            var oerConferencePaper = await _context.OerConferencePapers.FindAsync(id);
            if (oerConferencePaper == null)
            {
                return NotFound();
            }
            return PartialView("_edit",oerConferencePaper);
        }

        // POST: OerConferencePapers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile fullPaper, OerConferencePaper oerConferencePaper)
        {
            try
            {
                var oerConferencePapers = await _context.OerConferencePapers.FindAsync(id);

                //var uploader = UserManager.GetUserId(User);
                //Context.Add(oerConferencePapers);


                if (fullPaper != null && fullPaper.Length > 0)
                {
                    var uploadDir = @"files/oerConferencePapers";
                    var fileName = Path.GetFileNameWithoutExtension(fullPaper.FileName);
                    var extension = Path.GetExtension(fullPaper.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        fullPaper.CopyTo(fs);
                        oerConferencePapers.Upload = fileName;
                    }

                }
                await TryUpdateModelAsync<OerConferencePaper>(oerConferencePapers);               //    c => c.Ssce1, c => c.Ssce2, c => c.BirthCertificate, c => c.DirectEntryUpload, c => c.LGAUpload))

                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {

                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return RedirectToAction(nameof(Index));
        }


        // GET: oerConferencePapers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OerConferencePapers == null)
            {
                return NotFound();
            }

            var oerConferencePaper = await _context.OerConferencePapers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerConferencePaper == null)
            {
                return NotFound();
            }

            return PartialView("_delete",oerConferencePaper);
        }

        // POST: oerConferencePapers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OerConferencePapers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OerConferencePapers'  is null.");
            }
            var oerConferencePaper = await _context.OerConferencePapers.FindAsync(id);
            if (oerConferencePaper != null)
            {
                _context.OerConferencePapers.Remove(oerConferencePaper);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public FileResult DownloadFile(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "files/oerConferencePapers/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        private bool OerConferencePaperExists(int id)
        {
          return _context.OerConferencePapers.Any(e => e.Id == id);
        }
    }
}
