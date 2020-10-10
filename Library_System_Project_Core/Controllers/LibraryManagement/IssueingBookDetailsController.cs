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
    public class IssueingBookDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueingBookDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IssueingBookDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IssueingBookDetails.Include(i => i.Books).Include(i => i.Staffs).Include(i => i.Students).Include(i => i.Teachers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IssueingBookDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueingBookDetails = await _context.IssueingBookDetails
                .Include(i => i.Books)
                .Include(i => i.Staffs)
                .Include(i => i.Students)
                .Include(i => i.Teachers)
                .FirstOrDefaultAsync(m => m.SerialID == id);
            if (issueingBookDetails == null)
            {
                return NotFound();
            }

            return View(issueingBookDetails);
        }

        // GET: IssueingBookDetails/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "Title");
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID");
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID");
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID");
            return View();
        }

        // POST: IssueingBookDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SerialID,BookID,StudentID,TeacherID,StaffID,Borrow_Date,Return_Date,Actual_Return_Date")] IssueingBookDetails issueingBookDetails)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(issueingBookDetails);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}


            _context.Add(issueingBookDetails);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "ISBN", issueingBookDetails.BookID);
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "Address", issueingBookDetails.StaffID);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Address", issueingBookDetails.StudentID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "Address", issueingBookDetails.TeacherID);
            return RedirectToAction(nameof(Index));
        }

        // GET: IssueingBookDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueingBookDetails = await _context.IssueingBookDetails.FindAsync(id);
            if (issueingBookDetails == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "BookID", issueingBookDetails.BookID);
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID", issueingBookDetails.StaffID);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StudentID", issueingBookDetails.StudentID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID", issueingBookDetails.TeacherID);
            return View(issueingBookDetails);
        }

        // POST: IssueingBookDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SerialID,BookID,StudentID,TeacherID,StaffID,Borrow_Date,Return_Date,Actual_Return_Date")] IssueingBookDetails issueingBookDetails)
        {
            if (id != issueingBookDetails.SerialID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(issueingBookDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueingBookDetailsExists(issueingBookDetails.SerialID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "Title", issueingBookDetails.BookID);
            ViewData["StaffID"] = new SelectList(_context.Staff, "StaffID", "StaffID", issueingBookDetails.StaffID);
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "StaffID", issueingBookDetails.StudentID);
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "StaffID", issueingBookDetails.TeacherID);
            return View(issueingBookDetails);
        }

        // GET: IssueingBookDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueingBookDetails = await _context.IssueingBookDetails
                .Include(i => i.Books)
                .Include(i => i.Staffs)
                .Include(i => i.Students)
                .Include(i => i.Teachers)
                .FirstOrDefaultAsync(m => m.SerialID == id);
            if (issueingBookDetails == null)
            {
                return NotFound();
            }

            return View(issueingBookDetails);
        }

        // POST: IssueingBookDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueingBookDetails = await _context.IssueingBookDetails.FindAsync(id);
            _context.IssueingBookDetails.Remove(issueingBookDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueingBookDetailsExists(int id)
        {
            return _context.IssueingBookDetails.Any(e => e.SerialID == id);
        }
    }
}
