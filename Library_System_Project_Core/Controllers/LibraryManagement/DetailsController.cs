using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_System_Project_Core.Data;
using Library_System_Project_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_System_Project_Core.Controllers.LibraryManagement
{
    public class DetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //IHostingEnvironment _env;
        public DetailsController(ApplicationDbContext context) //IHostingEnvironment env
        {
            _context = context;
            //_env = env;
        }

        public ActionResult Index(string searchString, int? FacultyID, int? DepartmentID)
        {
            var singleSelectList = new SelectedDetailsModel();

            singleSelectList.Faculty = _context.Faculty.ToList();


            if (FacultyID == null)
            {
                byte[] kkk = HttpContext.Session.Get("FacultyID");

                if (kkk != null)
                {
                    string someString = Encoding.ASCII.GetString(kkk);
                    FacultyID = Convert.ToInt32(someString);
                }
            }

            if (FacultyID != null)
            {

                HttpContext.Session.SetString("FacultyID", FacultyID.ToString());
                singleSelectList.Department = _context.Department.Where(w => w.FacultyID == FacultyID.Value).ToList();


            }

            if (DepartmentID != null)
            {

                singleSelectList.Book = _context.Book.Include(i => i.BookTypes).Include(a => a.Author).Include(p => p.Publisher).Where(w => w.DepartmentID == DepartmentID.Value).ToList();

            }

            return View(singleSelectList);
            
        }
    }
}
