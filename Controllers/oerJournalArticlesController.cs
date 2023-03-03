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
    public class OerJournalArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public OerJournalArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: oerJournalArticles
        public async Task<IActionResult> Index()
        {
              return View(await _context.OerJournalArticles.ToListAsync());
        }
        public async Task<IActionResult> MyUploads()
        {
              return View(await _context.OerJournalArticles.ToListAsync());
        }
        // Creating a modal form to display abstact
        public async Task<IActionResult> Abstract(int? id)
        {
            var article = await _context.OerJournalArticles.FindAsync(id);
            ViewBag.Abstract = article.Abstract;
            return PartialView("_Abstract");
        }
        // GET: oerJournalArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OerJournalArticles == null)
            {
                return NotFound();
            }

            var oerJournalArticle = await _context.OerJournalArticles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerJournalArticle == null)
            {
                return NotFound();
            }

            return View(oerJournalArticle);
        }

        // GET: oerJournalArticles/Create
        public IActionResult Create()
        {
            return PartialView("_add");
        }

        // POST: oerJournalArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile FullArticle, OerJournalArticle oerJournalArticle)
        {
           // var uploader = UserManager.GetUserId(User);
            _context.Add(oerJournalArticle);

            try
            {
                //var isAuthorized = await AuthorizationService.AuthorizeAsync(User, oerJournalArticle, ApplicationOperations.Create);//using the same application Aothorizatin handler
                //if (isAuthorized.Succeeded == false)
                //    return Forbid();
                if (FullArticle != null && FullArticle.Length > 0)
                {
                    var uploadDir = @"files/OER/journal";
                    var fileName = Path.GetFileNameWithoutExtension(FullArticle.FileName);
                    var extension = Path.GetExtension(FullArticle.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        FullArticle.CopyTo(fs);
                        oerJournalArticle.Upload = fileName;
                    }

                }
                //  Context.Add(oerJournalArticle);
                await TryUpdateModelAsync<OerJournalArticle>(oerJournalArticle);               //    c => c.Ssce1, c => c.Ssce2, c => c.BirthCertificate, c => c.DirectEntryUpload, c => c.LGAUpload))

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


        // GET: oerJournalArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OerJournalArticles == null)
            {
                return NotFound();
            }

            var oerJournalArticle = await _context.OerJournalArticles.FindAsync(id);
            if (oerJournalArticle == null)
            {
                return NotFound();
            }
            return PartialView("_edit",oerJournalArticle);
        }

        // POST: oerJournalArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile fullArticle, OerJournalArticle oerJournalArticle)
        {
            try
            {
                var Article = await _context.OerJournalArticles.FindAsync(id);

                //var uploader = UserManager.GetUserId(User);
                //Context.Add(oerConferencePapers);


                if (fullArticle != null && fullArticle.Length > 0)
                {
                    var uploadDir = @"files/oerArticles";
                    var fileName = Path.GetFileNameWithoutExtension(fullArticle.FileName);
                    var extension = Path.GetExtension(fullArticle.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        fullArticle.CopyTo(fs);
                        Article.Upload = fileName;
                    }

                }
                await TryUpdateModelAsync<OerJournalArticle>(Article);  
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


        // GET: oerJournalArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OerJournalArticles == null)
            {
                return NotFound();
            }

            var oerJournalArticle = await _context.OerJournalArticles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerJournalArticle == null)
            {
                return NotFound();
            }

            return PartialView("_delete",oerJournalArticle);
        }

        // POST: oerJournalArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OerJournalArticles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OerJournalArticles'  is null.");
            }
            var oerJournalArticle = await _context.OerJournalArticles.FindAsync(id);
            if (oerJournalArticle != null)
            {
                _context.OerJournalArticles.Remove(oerJournalArticle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public FileResult DownloadFile(string FileName)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "files/oerArticles/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        private bool OerJournalArticleExists(int id)
        {
          return _context.OerJournalArticles.Any(e => e.Id == id);
        }
    }
}
