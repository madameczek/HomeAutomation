using HomeAutomationWebApp.Models.DbModels;
using HomeAutomationWebApp.Models.ViewModels;
using HomeAutomationWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Controllers
{
    [RequireHttps]
    public class Account : Controller
    {
        #region Ctor & Dependency Injection
        private readonly SignInManager<IotUser> _signInManager;
        private readonly UserManager<IotUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IUserManagerService _userManagerService;
        private readonly IWebAppEmailService _emailService;
        public Account(
            SignInManager<IotUser> signInManager, 
            UserManager<IotUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory,
            IUserManagerService userManagerService,
            IWebAppEmailService webAppEmailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger("Account Controller");
            _userManagerService = userManagerService;
            _emailService = webAppEmailService;
        }
        #endregion

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationViewModel model)
        {
            IdentityResult addToRoleResult;

            if (ModelState.IsValid)
            {
                if (!_userManagerService.IsEmailUnique(model.Email))
                {
                    ModelState.AddModelError("", "This email is taken");
                    TempData["warning_email"] = "This email is taken";
                    return View(model);
                }

                if (!IsPhoneNumberValid(model.PhoneNumber))
                {
                    ModelState.AddModelError("", "Check phone number. Allowed characters: 0-9, '+- .'. Ex: 0048.501 502 503. The only country code accepted is 48.");
                    return View(model);
                }

                IotUser user = new IotUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                user.UserName = $"{user.FirstName}{user.LastName}".Replace(" ", "_");
                user.UserName = RemoveDiacritics(user.UserName);
                user.NormalizedUserName = user.UserName.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();
                
                try
                {
                    // Create user
                    IdentityResult createUserResult = await _userManager.CreateAsync(user, model.Password);

                    if (createUserResult.Succeeded)
                    {
                        _logger.LogInformation("User {User} with email {Email} created.", user.UserName, user.Email);

                        // Add to Roles. Add first user created with FirstName="Administrator" to Administrator role :o
                        if (_userManagerService.IsOnlyAdministratorExisting() && model.FirstName == "Administrator")
                        {
                            addToRoleResult = await _userManager.AddToRoleAsync(user, "Administrator");
                            if (addToRoleResult.Succeeded)
                            {
                                _logger.LogInformation("User with email {Email} added to role {Role}", user.Email, "Administrator");
                            }
                            else
                            {
                                _logger.LogWarning("Error adding user with email {Email} added {Role}", user.Email, "Administrator");
                            }
                        }
                        addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                        if (addToRoleResult.Succeeded)
                        {
                            _logger.LogInformation("User with email {Email} added to role {Role}", user.Email, "User");
                        }
                        else
                        {
                            _logger.LogWarning("Error adding user with email {Email} added {Role}", user.Email, "User");
                        }

                        _ = GenerateAndSendTokeByEmail(user);
                        return RedirectToAction(nameof(SuccessfulRegistration));
                    }
                    else
                    {
                        createUserResult.Errors.Select(e => e.Description).ToList().ForEach(e=>ModelState.AddModelError("",e));
                        return View(model);
                    }

                }
                catch(Exception e)
                {
                    _logger.LogError(e, "Error creating user.");
                    throw;
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult SuccessfulRegistration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmed(string user, string token)
        {
            // Do not change name of parameter 'user'. It must match email
            var identityUser = _userManager.FindByIdAsync(user).Result;
            if (identityUser != null)
            {
                var result = await _userManager.ConfirmEmailAsync(identityUser, token);
                if (result.Succeeded || await _userManager.IsEmailConfirmedAsync(identityUser))
                {
                    return View(nameof(EmailConfirmed));
                }
                else
                {
                    var enumerator = result.Errors.GetEnumerator();
                    var errorList = new List<string>();
                    while (enumerator.MoveNext())
                    {
                        errorList.Add(enumerator.Current.Description);
                    }
                    if (errorList.Contains("Invalid token."))
                    {
                        _logger.LogError("Confirming Email {Email} error. Error list: {Error}", identityUser.Email, errorList);
                        // Send confirmation email again
                        _ = GenerateAndSendTokeByEmail(identityUser);
                        ViewBag.Message = "Twój link aktywacyjny wygasł.";
                        return View(nameof(SuccessfulRegistration));
                    }
                    ViewBag.Errors = errorList;
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
            return RedirectToAction(nameof(Error));
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
                return RedirectToAction(nameof(Home.Index), nameof(Home));
            }

            TempData["warning_password"] = "Pokombinuj jeszcze z hasłem";
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Home.Index), nameof(Home));
        }

        [Authorize]
        public IActionResult Manage()
        {
            var user = _userManager.GetUserAsync(User);
            ViewBag.Phone = _userManager.GetPhoneNumberAsync(user.Result).Result;
            ViewBag.Email = _userManager.GetEmailAsync(user.Result).Result;
            return View();
        }

        public IActionResult Error(List<string> errors)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        private async Task GenerateAndSendTokeByEmail(IotUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(
                nameof(EmailConfirmed), 
                nameof(Account), 
                new { token, user = user.Id }, 
                Request.Scheme, 
                Request.Host.ToString());
            _ = _emailService.SendEmailConfirmation(confirmationLink, user);
            return;
        }

        [NonAction]
        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            normalizedString = normalizedString.Replace('ł', 'l');
            
            var stringBuilder = new StringBuilder();
            foreach (char c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        [NonAction]
        private bool IsPhoneNumberValid(string numberToCheck)
        {
            Regex regex = new Regex(pattern: @"^((00|\+)[1-9]\d)?[\- \.]?[1-9]\d{2}[\- \.]?\d{3}[\- \.]?\d{3}$");
            return regex.IsMatch(numberToCheck);
        }
    }
}
