using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITnews.Data;
using ITnews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ITnews.Areas.Identity.Pages.Account.Manage
{
    public class GridModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GridModel
         (UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return Content(JsonConvert.SerializeObject(usersNews));
        }

        public async Task<IActionResult> OnPost(object data)
        {
            var user = await _userManager.GetUserAsync(User);
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return Content(JsonConvert.SerializeObject(usersNews));
        }

        public async Task<IActionResult> OnPostAdd()
        {
            var user = await _userManager.GetUserAsync(User);
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return Content(JsonConvert.SerializeObject(usersNews));
        }

        public async Task<IActionResult> OnPostEdit()
        {
            var user = await _userManager.GetUserAsync(User);
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return Content(JsonConvert.SerializeObject(usersNews));
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var user = await _userManager.GetUserAsync(User);
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return Content(JsonConvert.SerializeObject(usersNews));
        }

    }

}