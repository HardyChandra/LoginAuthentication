using InternProject.Models;
using InternProject.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccount _userAccount;

        public AccountController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                _userAccount.CreateUser(user.Fullname, user.Email, user.Password);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

       

        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var userData = _userAccount.CheckUser(user.Email, user.Password);

                if (user != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("Username", userData.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userData.Email));
                    claims.Add(new Claim(ClaimTypes.Name, userData.Fullname));
                    claims.Add(new Claim(ClaimTypes.Role, userData.Role));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    return Redirect(returnUrl);
                }
                TempData["Error"] = "Username or Password is invalid!";
                return View();
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
