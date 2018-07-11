using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using ITnews.Models;
using ITnews.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace ITnews.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<IndexModel> _stringLocalizer;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
             IStringLocalizer<IndexModel> stringLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public IndexViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            GetRequiedData(user);
            GetNonRequiedData(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await PostPersonalData(Input);
            StatusMessage = _stringLocalizer["Your profile has been updated"];
            return RedirectToPage();
        }

        public void GetNonRequiedData(ApplicationUser user)
        {
            Input = _mapper.Map<ApplicationUser, IndexViewModel>(user);
        }

        public async void GetRequiedData(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            Username = userName;
            Email = email;
        }

        public async Task PostPersonalData(IndexViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            ApplicationUser newUser = _mapper.Map(model, user);
            await _userManager.UpdateAsync(newUser);
            await _signInManager.RefreshSignInAsync(user);
        }
    }  
}
