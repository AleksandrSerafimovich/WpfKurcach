using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITnews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ITnews.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AdminAction(string returnUrl = null)
        {
            List<ApplicationUser> loginViews = new List<ApplicationUser>(_userManager.Users);
            ViewData["ReturnUrl"] = returnUrl;
            return View(loginViews);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AdminAction(List<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                ApplicationUser newUser = await SynhronizeUser(user);
                string currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                await _userManager.RemoveFromRoleAsync(newUser, currentRole);
                await _userManager.AddToRoleAsync(newUser, user.RoleName);
            }
            return View(new List<ApplicationUser>(_userManager.Users));
        }

        private async Task<ApplicationUser> SynhronizeUser(ApplicationUser user)
        {
            var newUser = await _userManager.FindByEmailAsync(user.Email);
            newUser.Email = user.Email;
            newUser.RoleName = user.RoleName;
            newUser.SecurityStamp = Guid.NewGuid().ToString();
            await _userManager.UpdateAsync(newUser);
            return newUser;
        }
    }
}