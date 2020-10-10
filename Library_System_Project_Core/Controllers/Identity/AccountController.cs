using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library_System_Project_Core.Data;
using Library_System_Project_Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Library_System_Project_Core.Controllers.Identity
{
    public class AccountController : Controller
    {
        SignInManager<ApplicationUser> _signInManager;
        UserManager<ApplicationUser> _userManager;
        RoleManager<ApplicationRole> _rollManager;
        ApplicationDbContext _applicationDbContext;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> rollManager, ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _rollManager = rollManager;
            _applicationDbContext = applicationDbContext;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName, string password, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var userAll = _userManager.Users.ToList();

                if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("Login");

                }
                try
                {
                    var UserInstance = _userManager.Users.Where(w => w.Email == userName).FirstOrDefault();

                    var userRole = _applicationDbContext.UserRoles.Where(w => w.UserId == UserInstance.Id).ToList();
                    var roleList = _applicationDbContext.Roles.Join(userRole, a => a.Id, b => b.RoleId, (a, b) => new { roleName = a.Name }).ToList();


                    IList<string> rolesList = new List<string>();
                    foreach (var ro in roleList)
                    {
                        rolesList.Add(ro.roleName.ToString());
                    }

                    bool yesFound = await _userManager.CheckPasswordAsync(UserInstance, password);

                    if (!yesFound)
                    {
                        return RedirectToAction("Index", "Home");
                    }


                    var customClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserInstance.UserName)
                    };

                    foreach (string s in rolesList)
                    {
                        customClaims.Add(new Claim(ClaimTypes.Role, s));
                    }

                    var claimsIdentity = new ClaimsIdentity(customClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);



                    await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
                        claimsPrincipal, new AuthenticationProperties { IsPersistent = false, RedirectUri = "/Home/Index" });



                }
                catch (Exception e)
                {
                    return RedirectToAction("Login");
                }

                return View();

            }

            return View();

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var studentModel = _applicationDbContext.Student.Where(w => w.Email == model.Email).FirstOrDefault();

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult RollCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RollCreate(string RollName)
        {

            ApplicationRole role = new ApplicationRole()
            {
                Name = RollName,
                NormalizedName = RollName
            };

            IdentityResult result = await _rollManager.CreateAsync(role);

            if (result.Succeeded)
            {

                return RedirectToAction("RollList", "Account");
            }
            else
            {
                return View();
            }


        }



        public IActionResult RollList()
        {
            var roleList = _applicationDbContext.Roles.ToList();
            return View(roleList);
        }


        public IActionResult UserList()
        {
            var roleList = _applicationDbContext.Users.ToList();
            return View(roleList);
        }

        [HttpGet]
        public IActionResult RollMappingToUser(string UserName)
        {
            ViewData["UserName"] = UserName;
            List<UserRollMapping> RollList = new List<UserRollMapping>();
            foreach (var r in _rollManager.Roles.ToList())
            {
                RollList.Add(new UserRollMapping() { Role = r, IsChecked = true });
            }


            return View(RollList);
        }


        [HttpPost]
        public async Task<IActionResult> RollMappingToUser(IList<UserRollMapping> applicationRoles, string UserName)
        {

            try
            {
                var user = _userManager.Users.Where(w => w.Email == UserName).FirstOrDefault();

                foreach (var k in applicationRoles)
                {
                    if (k.IsChecked == true)
                    {
                        try
                        {
                            await _userManager.AddToRoleAsync(user, k.Role.Name);
                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
                return RedirectToAction("UserList", "Account");

            }
            catch (Exception ex)
            {
                return View();
            }

        }





        [HttpGet]
        public IActionResult RollMappingToUserList()
        {


            List<DropDownListValue> ddlv = _userManager.Users.ToList().Select(s => new DropDownListValue { Id = s.Email, Name = s.Email }).ToList();

            var ViewDropDown = new ViewDropDown()
            {
                DropDownListValues = ddlv,
                DropdownName = "MyDropDownl"
            };


            return View(ViewDropDown);
        }

        [HttpPost]
        public IActionResult RollMappingToUserList(string UserName)
        {
            string url = "RollMappingToUser?UserName=" + UserName;

            return Redirect(url);

        }





        public ActionResult RoleAssignWithUser()
        {
            List<ApplicationUserRole> aur = new List<ApplicationUserRole>();
            foreach (var v in _applicationDbContext.ApplicationUserRole.ToList())
            {
                aur.Add(new ApplicationUserRole()
                {

                    Role = _applicationDbContext.Roles.Where(w => w.Id == v.RoleId).FirstOrDefault(),
                    User = _applicationDbContext.Users.Where(u => u.Id == v.UserId).FirstOrDefault()


                });
            }
            return View(_applicationDbContext.ApplicationUserRole.ToList());
        }




        public IActionResult Denied()
        {

            return RedirectToAction("Login");
        }



        [HttpGet]

        [ActionName("Logout")]
        public async Task<IActionResult> LogoutGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("YourAppCookieName");
            return RedirectToAction("Login");
        }


        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.Context.SignOutAsync();


            return RedirectToAction("Login");
        }



    }
}