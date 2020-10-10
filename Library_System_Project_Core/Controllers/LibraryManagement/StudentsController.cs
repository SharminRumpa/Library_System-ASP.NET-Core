using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_System_Project_Core.Data;
using Library_System_Project_Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Library_System_Project_Core.Controllers.LibraryManagement
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public StudentsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Student.Include(s => s.Departments);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Departments)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,Date_Of_Birth,Gender,Phone_Number,Email,Address,ImageUrl,DepartmentID")] Student student, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string imagePath = _env.WebRootPath + "\\Images\\Students\\" + formFile.FileName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    student.ImageUrl = "~/Images/Students/" + formFile.FileName;
                    _context.Add(student);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Students");
                }
                catch (Exception ex)
                {
                    return View();
                }

            }
            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,Date_Of_Birth,Gender,Phone_Number,Email,Address,ImageUrl,DepartmentID")] Student student, IFormFile formFile)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile != null)
                    {
                        string imagePath = _env.WebRootPath + "\\Images\\Students\\" + formFile.FileName;
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }

                        student.ImageUrl = "~/Images/Students/" + formFile.FileName;
                    }

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name", student.DepartmentID);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Departments)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}
