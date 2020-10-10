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
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        IHostingEnvironment _env;
        public AuthorsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env =  env;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var author = from a in _context.Author select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                author = author.Where(a => a.FullName.StartsWith(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    author = author.OrderBy(s => s.FullName);
                    break;
                case "date_desc":
                    author = author.OrderByDescending(s => s.Description);
                    break;
                case "Date":
                    author = author.OrderBy(s => s.Description);
                    break;
                default:
                    author = author.OrderBy(s => s.ImageUrl);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Author>.CreateAsync(author.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,FirstName,LastName,Date_Of_Birth, ImageUrl, Description")] Author author, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imagePath = _env.WebRootPath + "\\Images\\" + formFile.FileName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    author.ImageUrl = "~/Images/" + formFile.FileName;
                    _context.Add(author);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Authors");
                }
                catch (Exception ex)
                {
                    return View();
                }
                
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorID,FirstName,LastName,Date_Of_Birth,ImageUrl,Description")] Author author, IFormFile formFile)
        {
            if (id != author.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   if (formFile != null)
                    {
                        string imagePath = _env.WebRootPath + "\\Images\\" + formFile.FileName;
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }

                        author.ImageUrl = "~/Images/" + formFile.FileName;
                    }

                    _context.Update(author);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Authors");
                }
                catch (Exception ex)
                {
                    return View();
                }

            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorID == id);
        }
    }
}
