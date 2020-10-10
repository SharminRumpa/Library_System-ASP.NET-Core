using Library_System_Project_Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library_System_Project_Core.Models
{
    public class CommonActionFilter : IActionFilter
    {
        SignInManager<ApplicationUser> _signInManager;

        
        public CommonActionFilter(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var dbconOptions = new DbContextOptionsBuilder<ApplicationDbContext>();

            dbconOptions.UseSqlServer("Server =.\\sqlexpress; Database = Library_Management_Core; Trusted_Connection = True; MultipleActiveResultSets = true");
            ApplicationDbContext db = new ApplicationDbContext(dbconOptions.Options);

            var MenuPermiBasedOnRoll = from mm in db.MenuModel
                                       join mhm in db.MenuHelperModel on mm.MenuHelperModelId equals mhm.Id
                                       join ro in db.Roles.ToList() on mm.RollId equals ro.Id
                                       join mmm in db.MenuModelManage on mm.Id equals mmm.MenuModelId
                                       select new
                                       {
                                           ConName = mhm.ControllerName,
                                           ActName = mhm.ActionName,
                                           RollName = ro.Name,
                                           InsName = mmm.Insert,
                                           DelName = mmm.Delete,
                                           UpdName = mmm.Update,
                                           RetName = mmm.Retrive
                                       };
            string contName = context.RouteData.Values["controller"].ToString();
            string actName = context.RouteData.Values["action"].ToString();
            string metName = context.HttpContext.Request.Method;


            bool Retrive = metName.ToString() == "GET" ? true : false;
            bool Insert = metName.ToString() == "POST" ? true : false;
            bool Delete = metName.ToString() == "DELETE" ? true : false;
            bool Update = metName.ToString() == "PUT" ? true : false;

            var allRollInClaims = context.HttpContext.User.Claims.Where(w => w.Type == ClaimTypes.Role).ToList();
            bool permitted = false;
            foreach (var loopClaimRoll in allRollInClaims)
            {
                if (Retrive)
                {

                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.RetName == Retrive && w.ConName.ToString() == contName && w.ActName == actName).Any();


                    if (permitted)
                    {
                        break;
                    }
                }
                else if (Insert)
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.InsName == Insert && w.ConName.ToString() == contName && w.ActName == actName).Any();
                    if (permitted)
                    {
                        break;
                    }

                }
                else if (Delete)
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.DelName == Delete && w.ConName.ToString() == contName && w.ActName == actName).Any();
                    if (permitted)
                    {
                        break;
                    }


                }
                else
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.UpdName == Update && w.ConName.ToString() == contName && w.ActName == actName).Any();
                    if (permitted)
                    {
                        break;
                    }
                }
            }


            if (!permitted)
            {
                Controller controller = context.Controller as Controller;

                context.Result = controller.RedirectToAction("Login", "Account");

            }




        }
    }
}
