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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

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
            if (!ModelState.IsValid) return View(model);

            if (!_userManagerService.IsEmailUnique(model.Email))
            {
                ModelState.AddModelError("", "This email is taken");
                return View(model);
            }

            if (!IsPhoneNumberValid(model.PhoneNumber))
            {
                ModelState.AddModelError("", "Check phone number. Allowed characters: 0-9, '+- .'. Ex: 0048.501 502 503. The only country code accepted is 48.");
                return View(model);
            }

            var random = new Random();
            var user = new IotUser()
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email
            };
            user.UserName = $"{user.Firstname}{user.Lastname}";
            user.UserName = RemoveDiacritics(user.UserName);
            user.UserName = RemoveNotLetterNorDigit(user.UserName);
            user.UserName += random.Next(1000).ToString("D3");
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
                    IdentityResult addToRoleResult;
                    if (_userManagerService.IsOnlyAdministratorExisting() && model.FirstName == "Administrator")
                    {
                        addToRoleResult = await _userManager.AddToRoleAsync(user, "Administrator");
                        if (addToRoleResult.Succeeded)
                        {
                            _logger.LogInformation("User with email {Email} added to role {Role}", user.Email, "Administrator");
                        }
                        else
                        {
                            _logger.LogWarning("Error adding user with email {Email} to role {Role}", user.Email, "Administrator");
                        }
                    }

                    addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (addToRoleResult.Succeeded)
                    {
                        _logger.LogInformation("User with email {Email} added to role {Role}", user.Email, "User");
                    }
                    else
                    {
                        _logger.LogWarning("Error adding user with email {Email} to role {Role}", user.Email, "User");
                    }

                    _ = GenerateAndSendTokeByEmail(user);
                    return RedirectToAction(nameof(ConfirmRegistration));
                }

                createUserResult.Errors
                    .Select(e => e.Description)
                    .ToList()
                    .ForEach(e => ModelState.AddModelError("", e));
                return View(model);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error creating user.");
            }
            return View(model); // consider redirect to error page
        }

        [HttpGet]
        // Show a page with 'click emailed link' prompt
        public IActionResult ConfirmRegistration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmed(string user, string token)
        {
            // Do not change name of parameter 'user'. It must match email
            var identityUser = _userManager.FindByIdAsync(user).Result;
            
            if (identityUser == null) return RedirectToAction(nameof(Error));

            var result = await _userManager.ConfirmEmailAsync(identityUser, token);
            if (result.Succeeded || await _userManager.IsEmailConfirmedAsync(identityUser))
            {
                return View(nameof(EmailConfirmed));
            }

            using var enumerator = result.Errors.GetEnumerator();
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
                return View(nameof(ConfirmRegistration));
            }

            ViewBag.Errors = errorList;
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

            var user = await _userManager.FindByEmailAsync(model.Email);
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
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);
            var json = JsonConvert.SerializeObject(user);
            var model = JsonConvert.DeserializeObject<ManageViewModel>(json);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Show a page with 'click emailed link to reset password' prompt
        public async Task<IActionResult> ConfirmResetPassword(LoginViewModel model)
        {
            if (ModelState.GetFieldValidationState("Email") == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("", "Podaj adres email");
                return View(nameof(Login), model);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email not found"); // To show or not to show that email address id valid
                    return View(nameof(Login), model);
                }
                // Prepare token & send it via email
                _ = GenerateAndSendPasswordResetToken(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error");
            }
            return View();
        }

        [HttpGet]
        // Method invoked by email link
        // Show to a user a page with reset password form
        public async Task<IActionResult> ResetPassword(string user, string token)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(token)) return RedirectToAction(nameof(Error));
            var identityUser = await _userManager.FindByIdAsync(user);
            if (identityUser == null) return RedirectToAction(nameof(Error)); // or redirect to login page
            var model = new ResetPasswordViewModel() { Email = identityUser.Email, Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                // Confirm email, if not confirmed yet
                if (!(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    _ = _userManagerService.ConfirmEmailAsync(user);
                }
                return View("PasswordChangeConfirmed");
            }

            ModelState.AddModelError("", "Nie udało się zmienić hasła.");
            result.Errors
                .Select(identityError => identityError.Description)
                .ToList()
                .ForEach(errorMessage => ModelState.AddModelError("", errorMessage));
            return View(model);
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
        }

        [NonAction]
        private async Task GenerateAndSendPasswordResetToken(IotUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = Url.Action(
                nameof(ResetPassword),
                nameof(Account),
                new { token, user = user.Id },
                Request.Scheme,
                Request.Host.ToString());
            _ = _emailService.SendResetPasswordConfirmation(confirmationLink, user);
        }

        [NonAction]
        private static string RemoveDiacritics(string text)
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
        private static string RemoveNotLetterNorDigit(string text)
        {
            var sb = new StringBuilder();
            foreach (var character in text.Where(char.IsLetterOrDigit))
            {
                sb.Append(character);
            }
            return sb.ToString();
        }

        [NonAction]
        private static bool IsPhoneNumberValid(string numberToCheck)
        {
            var regex = new Regex(pattern: @"^((00|\+)48)?[\- \.]?[1-9]\d{2}[\- \.]?\d{3}[\- \.]?\d{3}$");
            return regex.IsMatch(numberToCheck);
        }
    }
}
