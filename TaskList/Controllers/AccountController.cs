using System;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Serilog;
using TaskList.Models;
using TaskList.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TaskList.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersContext usersDB;
        
        public AccountController(UsersContext usersContext)
        {
            usersDB = usersContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Tasks.Task<IActionResult> Login(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                User user = await usersDB.Users.
                    FirstOrDefaultAsync(user => user.Login == loginModel.Login && user.Password == loginModel.Password);
                if(user != null)
                {
                    await Authenticate(loginModel.Login);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некоректные имя или пароль");
            }
            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Tasks.Task<IActionResult> Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                User user = await usersDB.Users.FirstOrDefaultAsync(user => user.Login == registerModel.Login);
                if (user == null)
                {
                    usersDB.Add(new User() { Login = registerModel.Login, Password = registerModel.Password });
                    await usersDB.SaveChangesAsync();
                    await Authenticate(registerModel.Login);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Пользователь с данным логином уже существует");
            }
            return View(registerModel);
        }

        private async Tasks.Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Tasks.Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}