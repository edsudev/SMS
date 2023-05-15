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
    public class pgStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public pgStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: pgStudents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostGraduateStudents;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Allstudents()
        {
            var applicationDbContext = _context.PostGraduateStudents.Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Faculties).Include(c => c.LGAs).Include(c => c.Levels).Include(c => c.Nationalities).Include(c => c.Sessions).Include(c => c.Staffs).Include(c => c.States).Include(c => c.YearOfAdmissions);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Graduated()
        {
            var applicationDbContext = _context.PostGraduateStudents.Where(x => x.StudentStatus == 2).Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Faculties).Include(c => c.LGAs).Include(c => c.Levels).Include(c => c.Nationalities).Include(c => c.Sessions).Include(c => c.Staffs).Include(c => c.States).Include(c => c.YearOfAdmissions);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Expelled()
        {
            var applicationDbContext = _context.PostGraduateStudents.Where(x => x.StudentStatus == 3).Include(c => c.Applicants).Include(c => c.Departments).Include(c => c.Faculties).Include(c => c.LGAs).Include(c => c.Levels).Include(c => c.Nationalities).Include(c => c.Sessions).Include(c => c.Staffs).Include(c => c.States).Include(c => c.YearOfAdmissions);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: pgStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostGraduateStudents == null)
            {
                return NotFound();
            }

            var pgStudent = await _context.PostGraduateStudents
                .Include(p => p.Departments)
                .Include(p => p.Faculties)
                .Include(p => p.LGAs)
                .Include(p => p.Levels)
                .Include(p => p.Nationalities)
                .Include(p => p.Sessions)
                .Include(p => p.Staffs)
                .Include(p => p.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgStudent == null)
            {
                return NotFound();
            }

            return View(pgStudent);
        }

        // GET: pgStudents/Create
        public IActionResult Create()
        {
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id");
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id");
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id");
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id");
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id");
            return View();
        }

        // POST: pgStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Picture,Fullname,Sex,DOB,Religion,Phone,AltPhoneNumber,Email,NationalityId,StateOfOriginId,LGAId,PlaceOfBirth,PermanentHomeAddress,ContactAddress,MaritalStatus,NextOfkinName,NextOfKinRelationship,NextOfKinPhone,NextOfKinAddress,SchoolEmailAddress,MatNumber,Faculty,Level,ModeOfAdmission,YearOfAdmission,Department,CurrentSession,CreatedAt,Cleared,ClearedBy")] PgStudent pgStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pgStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", pgStudent.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", pgStudent.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", pgStudent.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", pgStudent.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", pgStudent.NationalityId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", pgStudent.CurrentSession);
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id", pgStudent.ClearedBy);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", pgStudent.StateOfOriginId);
            return View(pgStudent);
        }

        // GET: pgStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostGraduateStudents == null)
            {
                return NotFound();
            }

            var pgStudent = await _context.PostGraduateStudents.FindAsync(id);
            if (pgStudent == null)
            {
                return NotFound();
            }
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", pgStudent.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", pgStudent.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", pgStudent.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", pgStudent.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", pgStudent.NationalityId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", pgStudent.CurrentSession);
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id", pgStudent.ClearedBy);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", pgStudent.StateOfOriginId);
            return View(pgStudent);
        }

        // POST: pgStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Picture,Fullname,Sex,DOB,Religion,Phone,AltPhoneNumber,Email,NationalityId,StateOfOriginId,LGAId,PlaceOfBirth,PermanentHomeAddress,ContactAddress,MaritalStatus,NextOfkinName,NextOfKinRelationship,NextOfKinPhone,NextOfKinAddress,SchoolEmailAddress,MatNumber,Faculty,Level,ModeOfAdmission,YearOfAdmission,Department,CurrentSession,CreatedAt,Cleared,ClearedBy")] PgStudent pgStudent)
        {
            if (id != pgStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pgStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PgStudentExists(pgStudent.Id))
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
            ViewData["Department"] = new SelectList(_context.Departments, "Id", "Id", pgStudent.Department);
            ViewData["Faculty"] = new SelectList(_context.Faculties, "Id", "Id", pgStudent.Faculty);
            ViewData["LGAId"] = new SelectList(_context.Lgas, "Id", "Id", pgStudent.LGAId);
            ViewData["Level"] = new SelectList(_context.Levels, "Id", "Id", pgStudent.Level);
            ViewData["NationalityId"] = new SelectList(_context.Countries, "Id", "Id", pgStudent.NationalityId);
            ViewData["CurrentSession"] = new SelectList(_context.Sessions, "Id", "Id", pgStudent.CurrentSession);
            ViewData["ClearedBy"] = new SelectList(_context.Staffs, "Id", "Id", pgStudent.ClearedBy);
            ViewData["StateOfOriginId"] = new SelectList(_context.States, "Id", "Id", pgStudent.StateOfOriginId);
            return View(pgStudent);
        }

        // GET: pgStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostGraduateStudents == null)
            {
                return NotFound();
            }

            var pgStudent = await _context.PostGraduateStudents
                .Include(p => p.Departments)
                .Include(p => p.Faculties)
                .Include(p => p.LGAs)
                .Include(p => p.Levels)
                .Include(p => p.Nationalities)
                .Include(p => p.Sessions)
                .Include(p => p.Staffs)
                .Include(p => p.States)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pgStudent == null)
            {
                return NotFound();
            }

            return View(pgStudent);
        }

        // POST: pgStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostGraduateStudents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PostGraduateStudents'  is null.");
            }
            var pgStudent = await _context.PostGraduateStudents.FindAsync(id);
            if (pgStudent != null)
            {
                _context.PostGraduateStudents.Remove(pgStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PgStudentExists(int? id)
        {
          return _context.PostGraduateStudents.Any(e => e.Id == id);
        }
    }
}
