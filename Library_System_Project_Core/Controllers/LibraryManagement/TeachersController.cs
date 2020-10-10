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
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public TeachersController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teacher.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,FirstName,LastName,Date_Of_Birth,Gender,Phone_Number,Email,Address,ImageUrl,Joning_Date")] Teacher teacher, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imagePath = _env.WebRootPath + "\\Images\\Teachers\\" + formFile.FileName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    teacher.ImageUrl = "~/Images/Teachers/" + formFile.FileName;
                    _context.Add(teacher);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Teachers");
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherID,FirstName,LastName,Date_Of_Birth,Gender,Phone_Number,Email,Address,ImageUrl,Joning_Date")] Teacher teacher, IFormFile formFile)
        {
            if (id != teacher.TeacherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile != null)
                    {
                        string imagePath = _env.WebRootPath + "\\Images\\Teachers\\" + formFile.FileName;
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }

                        teacher.ImageUrl = "~/Images/Teachers/" + formFile.FileName;
                    }

                    _context.Update(teacher);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Teachers");
                }
             
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.TeacherID == id);
        }
    }
}
