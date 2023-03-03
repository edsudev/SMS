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
    public class OerStudentProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public OerStudentProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: oerStudentProjects
        public async Task<IActionResult> Index()
        {
              return View(await _context.OerStudentProjects.ToListAsync());
        }
        public async Task<IActionResult> MyUploads()
        {
              return View(await _context.OerStudentProjects.ToListAsync());
        }

        // GET: oerStudentProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OerStudentProjects == null)
            {
                return NotFound();
            }

            var oerStudentProject = await _context.OerStudentProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerStudentProject == null)
            {
                return NotFound();
            }

            return View(oerStudentProject);
        }

        // GET: OerStudentProjects/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            return PartialView("_add");
        }

        // POST: OerStudentProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, IFormFile synopsis, OerStudentProject oerStudentProject)
        {
           

                if (file != null && file.Length > 0)
                {
                    var uploadDir = @"files/OER/student project/main";
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                        oerStudentProject.Upload = fileName;

                    }
                }
                if (synopsis != null && synopsis.Length > 0)
                {
                    var uploadDir = @"files/OER/student project/synopsis";
                    var fileName = Path.GetFileNameWithoutExtension(synopsis.FileName);
                    var extension = Path.GetExtension(synopsis.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    fileName = fileName + extension;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        synopsis.CopyTo(fs);
                        oerStudentProject.Synopsis = fileName;

                    }


                }
                oerStudentProject.CreatedAt = DateTime.Now;
                _context.Add(oerStudentProject);
                await _context.SaveChangesAsync();
                //return PartialView("_coursewarePartial");
                return RedirectToAction(nameof(Index));
           
        }

        // GET: oerStudentProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OerStudentProjects == null)
            {
                return NotFound();
            }

            var oerStudentProject = await _context.OerStudentProjects.FindAsync(id);
            if (oerStudentProject == null)
            {
                return NotFound();
            }
            return PartialView("_edit",oerStudentProject);
        }

        // POST: oerStudentProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,NameOfStudent,MatNo,SessionOfAdmission,ModeOfStudy,Abstract,PublicationDate,Upload,Synopsis,IsPublished,CreatedAt,UpdatedAt")] OerStudentProject oerStudentProject)
        {
            if (id != oerStudentProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oerStudentProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OerStudentProjectExists(oerStudentProject.Id))
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
            return View(oerStudentProject);
        }

        // GET: oerStudentProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OerStudentProjects == null)
            {
                return NotFound();
            }

            var oerStudentProject = await _context.OerStudentProjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oerStudentProject == null)
            {
                return NotFound();
            }

            return PartialView("_delete",oerStudentProject);
        }

        // POST: oerStudentProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OerStudentProjects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OerStudentProjects'  is null.");
            }
            var oerStudentProject = await _context.OerStudentProjects.FindAsync(id);
            if (oerStudentProject != null)
            {
                _context.OerStudentProjects.Remove(oerStudentProject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OerStudentProjectExists(int? id)
        {
          return _context.OerStudentProjects.Any(e => e.Id == id);
        }
    }
}
