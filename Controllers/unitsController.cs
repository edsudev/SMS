using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EDSU_SYSTEM.Controllers
{
    public class UnitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UnitsController(ApplicationDbContext context,IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: units
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Units.Include(u => u.Positions).Include(u => u.Units);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Home(string? id)
        {
            ViewBag.unitName = id.ToUpper();
            var applicationDbContext = (from c in _context.Units where c.Units.Name == id select c).Include(u => u.Positions).Include(u => u.Units);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: units/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Name");
            return View();
        }

        // POST: units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile img, Unit unit)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.Length > 0)
                {
                    var uploadDir = @"files/units";
                    var fileName = Path.GetFileNameWithoutExtension(img.FileName);
                    var extension = Path.GetExtension(img.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = unit.Email + extension;
                    //fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extension;

                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        img.CopyTo(fs);
                        unit.Pic = fileName;
                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                        }
                    }

                }
                unit.CreatedAt = DateTime.Now;
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", unit.PositionId);
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Name", unit.UnitId);
            return View(unit);
        }

        // GET: units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", unit.PositionId);
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Name", unit.UnitId);
            return View(unit);
        }

        // POST: units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PositionId,UnitId,Pic,Phone,Message,IsActive,IsDirector,CreatedAt,UpdatedAt")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.Id))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", unit.PositionId);
            ViewData["UnitId"] = new SelectList(_context.UnitNames, "Id", "Name", unit.UnitId);
            return View(unit);
        }

        // GET: units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Units == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Units'  is null.");
            }
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(int? id)
        {
          return _context.Units.Any(e => e.Id == id);
        }
    }
}
