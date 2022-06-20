using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Shoppers.Membership.Entities;
using Shoppers.Utility;
using Shoppers.Web.Areas.SuperAdmin.Models;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace Shoppers.Web.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ILifetimeScope _scope;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(ILogger<AccountController> logger,
            ILifetimeScope scope,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _scope = scope;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var model = _scope.Resolve<SuperAdminLoginModel>();
            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(SuperAdminLoginModel model)
        {
            //model.ReturnUrl ??= Url.Content("~/");
            model.ReturnUrl ??= Url.Content("/SuperAdmin/Dashboard");

            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(model.ReturnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        /* [HttpPost, ValidateAntiForgeryToken]
         public async Task<IActionResult> Logout(string returnUrl = null)
         {
             await _signInManager.SignOutAsync();
             _logger.LogInformation("User logged out.");

             //returnUrl = Url.Content("/SuperAdmin/Login");
             if (returnUrl != null)
             {
                 return LocalRedirect(returnUrl);
             }
             else
             {
                 return RedirectToAction("Index", "Login");
             }
         }*/
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    values: new { area = "SuperAdmin", code },
                    protocol: Request.Scheme);

                _emailSender.Send(
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                    model.Email, model.Email);

                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]       
        public IActionResult ResetPassword(string code = null)
        {
            var model = _scope.Resolve<ResetPasswordModel>();
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                //var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                model = new ResetPasswordModel
                {
                    Code = code
                    
                };
                return View(model);
            }
        }

        [AllowAnonymous]
        [HttpPost, AutoValidateAntiforgeryToken] 
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            model.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var user = HttpContext.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
            }

            return View("Login");
        }
    }
}

