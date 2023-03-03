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
    public class OerCoursewaresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OerCoursewaresController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: OerCoursewares
        public async Task<IActionResult> Index()
        {
            return View(await _context.OerCoursewares.Include(i => i.Faculty).Include(i => i.Department).ToListAsync());
        }
        public async Task<IActionResult> MyUploads()
        {
            return View(await _context.OerCoursewares.ToListAsync());
        }

        // GET: OerCoursewares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OerCoursewares == null)
            {
                return NotFound();
            }

            var oerCourseware = await _context.OerCoursewares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerCourseware == null)
            {
                return NotFound();
            }

            return View(oerCourseware);
        }

        // GET: OerCoursewares/Create
        public IActionResult Create()
        {
            return PartialView("_add");
        }

        // POST: OerCoursewares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, IFormFile synopsis, OerCourseware oerCourseware)
        {
            if (ModelState.IsValid)
            {

                if (file != null && file.Length > 0)
                {
                    var uploadDir = @"files/OER/courseware/main";
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                        oerCourseware.Upload = fileName;

                    }
                }
                if (synopsis != null && synopsis.Length > 0)
                {
                    var uploadDir = @"files/OER/courseware/synopsis";
                    var fileName = Path.GetFileNameWithoutExtension(synopsis.FileName);
                    var extension = Path.GetExtension(synopsis.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        synopsis.CopyTo(fs);
                        oerCourseware.Synopsis = fileName;

                    }


                }
                oerCourseware.CreatedAt = DateTime.Now;
                _context.Add(oerCourseware);
                await _context.SaveChangesAsync();
                //return PartialView("_coursewarePartial");
                return RedirectToAction(nameof(Index));
            }
            return View(oerCourseware);
        }

        // GET: OerCoursewares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OerCoursewares == null)
            {
                return NotFound();
            }

            var oerCourseware = await _context.OerCoursewares.FindAsync(id);
            if (oerCourseware == null)
            {
                return NotFound();
            }
            return PartialView("_edit",oerCourseware);
        }

        // POST: OerCoursewares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LectureName,CourseCode,CourseTitle,Level,Upload,Synopsis,CreatedAt,UpdateAt")] OerCourseware oerCourseware)
        {
            if (id != oerCourseware.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oerCourseware);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OerCoursewareExists(oerCourseware.Id))
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
            return View(oerCourseware);
        }

        // GET: OerCoursewares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OerCoursewares == null)
            {
                return NotFound();
            }

            var oerCourseware = await _context.OerCoursewares
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerCourseware == null)
            {
                return NotFound();
            }

            return PartialView("_delete",oerCourseware);
        }

        // POST: OerCoursewares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OerCoursewares == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OerCoursewares'  is null.");
            }
            var oerCourseware = await _context.OerCoursewares.FindAsync(id);
            if (oerCourseware != null)
            {
                _context.OerCoursewares.Remove(oerCourseware);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Method to download File
        public FileResult DownloadFile(string FileName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "files/OER/courseware/main/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        public FileResult DownloadSynopsis(string FileName)
        {
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "files/OER/courseware/synopsis/") + FileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", FileName);
        }
        private bool OerCoursewareExists(int? id)
        {
            return _context.OerCoursewares.Any(e => e.Id == id);
        }
    }
}
