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
    public class OerTextbooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public OerTextbooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: oerTextbooks
        public async Task<IActionResult> Index()
        {
              return View(await _context.OerTextbooks.ToListAsync());
        }
        public async Task<IActionResult> MyUploads()
        {
              return View(await _context.OerTextbooks.ToListAsync());
        }
        // Creating a modal form to display abstact
        public async Task<IActionResult> Abstract(int? id)
        {
            var article = await _context.OerTextbooks.FindAsync(id);
            ViewBag.Sumary = article.Summary;
            return PartialView("_Abstract");
        }
        // GET: oerTextbooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OerTextbooks == null)
            {
                return NotFound();
            }

            var oerTextbook = await _context.OerTextbooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerTextbook == null)
            {
                return NotFound();
            }

            return View(oerTextbook);
        }

        // GET: oerTextbooks/Create
        public IActionResult Create()
        {
            return PartialView("_add");
        }

        // POST: oerTextbooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile FullTextbook, OerTextbook oerTextbook)
        {


            try
            {
                _context.Add(oerTextbook);

                if (FullTextbook != null && FullTextbook.Length > 0)
                {
                    var uploadDir = @"files/OER/textbook";
                    var fileName = Path.GetFileNameWithoutExtension(FullTextbook.FileName);
                    var extension = Path.GetExtension(FullTextbook.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        FullTextbook.CopyTo(fs);
                        oerTextbook.Upload = fileName;
                    }

                }
                // Context.Add(oerJournalArticle);
                await TryUpdateModelAsync<OerTextbook>(oerTextbook);               //    c => c.Ssce1, c => c.Ssce2, c => c.BirthCertificate, c => c.DirectEntryUpload, c => c.LGAUpload))

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

        // GET: oerTextbooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OerTextbooks == null)
            {
                return NotFound();
            }

            var oerTextbook = await _context.OerTextbooks.FindAsync(id);
            if (oerTextbook == null)
            {
                return NotFound();
            }
            return PartialView("_edit",oerTextbook);
        }

        // POST: oerTextbooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile FullTextbook, OerTextbook oerTextbook)
        {
            try
            {
                var oerTextbooks = await _context.OerTextbooks.FindAsync(id);

                //var uploader = UserManager.GetUserId(User);
                // Context.Add(oerTextbooks);


                if (FullTextbook != null && FullTextbook.Length > 0)
                {
                    var uploadDir = @"files/oerTextbooks";
                    var fileName = Path.GetFileNameWithoutExtension(FullTextbook.FileName);
                    var extension = Path.GetExtension(FullTextbook.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        FullTextbook.CopyTo(fs);
                        oerTextbooks.Upload = fileName;
                    }

                }
                // Context.Add(oerJournalArticle);
                await TryUpdateModelAsync<OerTextbook>(oerTextbooks);               //    c => c.Ssce1, c => c.Ssce2, c => c.BirthCertificate, c => c.DirectEntryUpload, c => c.LGAUpload))

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


        // GET: oerTextbooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OerTextbooks == null)
            {
                return NotFound();
            }

            var oerTextbook = await _context.OerTextbooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerTextbook == null)
            {
                return NotFound();
            }

            return PartialView("_delete",oerTextbook);
        }

        // POST: oerTextbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OerTextbooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OerTextbooks'  is null.");
            }
            var oerTextbook = await _context.OerTextbooks.FindAsync(id);
            if (oerTextbook != null)
            {
                _context.OerTextbooks.Remove(oerTextbook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public FileResult DownloadFile(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "files/oerTextbooks/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        private bool OerTextbookExists(int id)
        {
          return _context.OerTextbooks.Any(e => e.Id == id);
        }
    }
}
