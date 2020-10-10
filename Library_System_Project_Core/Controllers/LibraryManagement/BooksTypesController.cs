using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_System_Project_Core.Data;
using Library_System_Project_Core.Models;

namespace Library_System_Project_Core.Controllers.LibraryManagement
{
    public class BooksTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BooksTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BooksType.ToListAsync());
        }

        // GET: BooksTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksType = await _context.BooksType
                .FirstOrDefaultAsync(m => m.BooksTypeID == id);
            if (booksType == null)
            {
                return NotFound();
            }

            return View(booksType);
        }

        // GET: BooksTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooksTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BooksTypeID,Name")] BooksType booksType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booksType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booksType);
        }

        // GET: BooksTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksType = await _context.BooksType.FindAsync(id);
            if (booksType == null)
            {
                return NotFound();
            }
            return View(booksType);
        }

        // POST: BooksTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BooksTypeID,Name")] BooksType booksType)
        {
            if (id != booksType.BooksTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booksType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksTypeExists(booksType.BooksTypeID))
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
            return View(booksType);
        }

        // GET: BooksTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksType = await _context.BooksType
                .FirstOrDefaultAsync(m => m.BooksTypeID == id);
            if (booksType == null)
            {
                return NotFound();
            }

            return View(booksType);
        }

        // POST: BooksTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booksType = await _context.BooksType.FindAsync(id);
            _context.BooksType.Remove(booksType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksTypeExists(int id)
        {
            return _context.BooksType.Any(e => e.BooksTypeID == id);
        }
    }
}
