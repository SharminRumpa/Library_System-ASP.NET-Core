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
    public class StaffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _env;

        public StaffsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;

        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staff.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }


        // GET: Staffs/Create
        [ServiceFilter(typeof(CommonActionFilter))]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CommonActionFilter))]
        public async Task<IActionResult> Create([Bind("StaffID,FirstName,LastName,Date_Of_Birth,Gender,Phone_Number,Email,Address,ImageUrl,Joning_Date")] Staff staff, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string imagePath = _env.WebRootPath + "\\Images\\Staff\\" + formFile.FileName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    staff.ImageUrl = "~/Images/Staff/" + formFile.FileName;
                    _context.Add(staff);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Staffs");
                }
                catch (Exception ex)
                {
                    return View();
                }
    
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        [ServiceFilter(typeof(CommonActionFilter))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(CommonActionFilter))]
        public async Task<IActionResult> Edit(int id, [Bind("StaffID,FirstName,LastName,Date_Of_Birth,Gender,Phone_Number,Email,Address,ImageUrl,Joning_Date")] Staff staff, IFormFile formFile)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile != null)
                    {
                        string imagePath = _env.WebRootPath + "\\Images\\Staff\\" + formFile.FileName;
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }

                        staff.ImageUrl = "~/Images/Staff\\" + formFile.FileName;
                    }

                    _context.Update(staff);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Staffs");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        [ServiceFilter(typeof(CommonActionFilter))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffID == id);
        }
    }
}
