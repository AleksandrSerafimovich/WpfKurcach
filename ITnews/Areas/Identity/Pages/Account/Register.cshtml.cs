using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ITnews.Areas.Identity.Services;
using ITnews.Models.AccountViewModels;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using ITnews.Models;

namespace ITnews.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IStringLocalizer<RegisterModel> _stringLocalizer;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IStringLocalizer<RegisterModel> stringLocalizer,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _roleManager = roleManager;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }



        public void OnGet(string returnUrl = null)
        {         
            ReturnUrl = returnUrl;            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                await AddWriterRole(user);
                if (result.Succeeded)
                {
                    await SendEmail(user);
                    return RedirectToPage("./CheckEmail");
                }
                OutputErrors(result);
            }
            return Page();
        }

        private void OutputErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task SendEmail(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);
            EmailService _emailSender = new EmailService();
            await _emailSender.SendEmailAsync(Input.Email, "Confirm your account",
        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
        }

        public async Task AddWriterRole(ApplicationUser user)
        {
            await _userManager.AddToRoleAsync(user, "READER");
            user.RoleName = "reader";
            await _userManager.UpdateAsync(user);
        }
    }
}
