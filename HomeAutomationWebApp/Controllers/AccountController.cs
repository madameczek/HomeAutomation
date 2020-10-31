using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomationWebApp.Models.DbModels;
using HomeAutomationWebApp.Models.ViewModels;
using HomeAutomationWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomationWebApp.Controllers
{
    public class AccountController : Controller
    {
        #region Dependency Injection
        private readonly SignInManager<IotUser> _signInManager;
        private readonly UserManager<IotUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserManagerService _userManagerService;
        public AccountController(SignInManager<IotUser> signInManager, UserManager<IotUser> userManager, RoleManager<IdentityRole> roleManager, IUserManagerService userManagerService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userManagerService = userManagerService;
        }
        #endregion

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_userManagerService.IsEmailUnique(model.Email))
                {
                    TempData["warning_email"] = "This email is taken";
                    return View(model);
                }

                if (!model.Name.All(character => char.IsLetterOrDigit(character)))
                {
                    TempData["warning_name"] = "Only letters and digits";
                    return View(model);
                }

                if (!model.Surname.All(character => char.IsLetterOrDigit(character)))
                {
                    TempData["warning_surname"] = "Only letters and digits";
                    return View(model);
                }

                // Ensure unique UserName 
                string userName = $"{model.Name.Trim()}_{model.Surname.Trim()}";
                IotUser user = new IotUser { UserName = userName, Email = model.Email, PhoneNumber = model.PhoneNumber };

                try
                {
                    IdentityResult createUserResult = await _userManager.CreateAsync(user, model.Password);
                    IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(user, "User");

                    // Add first user created with Name="Administrator" to Administrator role
                    if (_userManagerService.IsOnlyAdministratorExisting() && model.Name == "Administrator")
                    {
                        _ = await _userManager.AddToRoleAsync(user, "Administrator");
                    }

                    if (createUserResult.Succeeded && addToRoleResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach(var item in createUserResult.Errors.ToList())
                        {
                            var _errors = new List<string>();
                            _errors.Add(string.Concat(item.Code, ": ", item.Description));
                            ViewBag.Warning_regerror = _errors;
                        }
                        return View(model);
                    }

                }
                catch
                {
                    throw new Exception("Database exception");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userManagerService.GetUserByEmail(model.Email);
            if (user == null)
            {
                TempData["warning_email"] = "Nie odnaleziono adresu e-mail";
                return View(model);
            }

            await _signInManager.SignOutAsync();
            var login = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
            if (login.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["warning_password"] = "Pokombinuj jeszcze z hasłem";
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Manage()
        {
            var _user = _userManager.GetUserAsync(User);
            ViewBag.Phone = _userManager.GetPhoneNumberAsync(_user.Result).Result;
            ViewBag.Email = _userManager.GetEmailAsync(_user.Result).Result;
            return View();
        }
    }
}
