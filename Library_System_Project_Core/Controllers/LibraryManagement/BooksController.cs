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
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Library_System_Project_Core.Controllers.LibraryManagement
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public BooksController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Books
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

            var book = from b in _context.Book.Include(b => b.Author).Include(b => b.BookTypes).Include(b => b.Departments).Include(b => b.Publisher) select b;


            if (!String.IsNullOrEmpty(searchString))
            {
                book = book.Where(b => b.Title.StartsWith(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    book = book.OrderBy(s => s.Title);
                    break;
                case "date_desc":
                    book = book.OrderByDescending(s => s.NoOfPage);
                    break;
                case "Date":
                    book = book.OrderBy(s => s.Author.FullName);
                    break;
                default:
                    book = book.OrderBy(s => s.ISBN);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Book>.CreateAsync(book.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookTypes)
                .Include(b => b.Departments)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Author, "AuthorID", "FullName");
            ViewData["BooksTypeID"] = new SelectList(_context.Set<BooksType>(), "BooksTypeID", "Name");
            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name");
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "PublisherID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookID,Title,ISBN,NoOfPage,ImageUrl,Publish_Year,IsAvailableForBorrow,AuthorID,DepartmentID,BooksTypeID,PublisherID")] Book book, IFormFile formFile)
        {
            try
            {
                string imagePath = _env.WebRootPath + "\\Images\\Books\\" + formFile.FileName;
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                book.ImageUrl = "~/Images/Books/" + formFile.FileName;
                _context.Add(book);
                await _context.SaveChangesAsync();

                ViewData["AuthorID"] = new SelectList(_context.Author, "AuthorID", "FullName", book.AuthorID);
                ViewData["BooksTypeID"] = new SelectList(_context.Set<BooksType>(), "BooksTypeID", "Name", book.BooksTypeID);
                ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name", book.DepartmentID);
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "PublisherID", "Name", book.PublisherID);
                //return View(book);

                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                return View();
            
            }
            
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Author, "AuthorID", "FullName", book.AuthorID);
            ViewData["BooksTypeID"] = new SelectList(_context.Set<BooksType>(), "BooksTypeID", "Name", book.BooksTypeID);
            ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name", book.DepartmentID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "PublisherID", "Name", book.PublisherID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,ISBN,NoOfPage,ImageUrl,Publish_Year,IsAvailableForBorrow,AuthorID,DepartmentID,BooksTypeID,PublisherID")] Book book, IFormFile formFile)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            try
            {
                if (formFile != null)
                {
                    string imagePath = _env.WebRootPath + "\\Images\\Books\\" + formFile.FileName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    book.ImageUrl = "~/Images/Books/" + formFile.FileName;
                }

                _context.Update(book);
                await _context.SaveChangesAsync();

                ViewData["AuthorID"] = new SelectList(_context.Author, "AuthorID", "FullName", book.AuthorID);
                ViewData["BooksTypeID"] = new SelectList(_context.Set<BooksType>(), "BooksTypeID", "Name", book.BooksTypeID);
                ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "Name", book.DepartmentID);
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "PublisherID", "Name", book.PublisherID);
                //return View(book);

                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                return View();
            }




           
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookTypes)
                .Include(b => b.Departments)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookID == id);
        }
    }
}
