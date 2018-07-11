using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ITnews.Models;
using ITnews.Models.ManageViewModels;
using Microsoft.Extensions.Localization;

namespace ITnews.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IStringLocalizer<ChangePasswordModel> _stringLocalizer;

        public ChangePasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            IStringLocalizer<ChangePasswordModel> stringLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        [BindProperty]
        public ChangePasswordViewModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return await ChangePassword();
        }

        public void ChangePasswordValidate(IdentityResult changePasswordResult)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public async Task<ActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                ChangePasswordValidate(changePasswordResult);
                return Page();
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _stringLocalizer["Your password has been changed."];
            return RedirectToPage();
        }
    }
}
