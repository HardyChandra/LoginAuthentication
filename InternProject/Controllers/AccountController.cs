﻿using InternProject.Models;
using InternProject.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                var email = _userAccount.CheckEmail(user.Email);
                if (email != null)
                {
                    var password = _userAccount.CheckPassword(user.Password);

                    if(password != null)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim("Username", user.Email));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Email));
                        claims.Add(new Claim(ClaimTypes.Name, password.Fullname));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);

                        return Redirect(returnUrl);
                    }
                    return View();
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