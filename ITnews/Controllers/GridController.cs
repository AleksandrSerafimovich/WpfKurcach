using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITnews.Data;
using ITnews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ITnews.Controllers
{
    public class GridController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GridController
         (UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public PartialViewResult GetTable(string email)
        {
            Response.Cookies.Append("user", email);
            return PartialView("Views/Shared/_GridPartial.cshtml");
        }

        public async Task<string> GetDataTable()
        {
            var News = GetNews(await GetCurrentUser());
            return JsonConvert.SerializeObject(News);
        }

        public async Task<string> EditData(New requestNew)
        {
            var oldNew = _context.News.Where(n => n.Id == requestNew.Id).FirstOrDefault();
            oldNew.Title = requestNew.Title;
            oldNew.Text = requestNew.Text;
            await _context.SaveChangesAsync();
            var News = GetNews(await GetCurrentUser());
            return JsonConvert.SerializeObject(News);
        }

        public async Task<string> AddData(New requestNew)
        {
            var admin = await AdminAction();
            await CreateNews(requestNew);
            var News = GetNews(await GetCurrentUser());
            return JsonConvert.SerializeObject(News);
        }

        public async Task<string> DeleteData(Guid Id)
        {
            var deleteNew = _context.News.Where(n => n.Id == Id).Include(i=>i.Comments).FirstOrDefault();
            RemoveNew(deleteNew);
            var News = GetNews(await GetCurrentUser());
            return JsonConvert.SerializeObject(News);
        }

        private List<New> GetNews(ApplicationUser user)
        {       
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return usersNews;
        }

        private async Task<ApplicationUser> AdminAction()
        {
            string email = Request.Cookies["user"];
            if (email == null)
            {
                return null;
            }
            return (await _userManager.FindByEmailAsync(email));
        }

        private async Task CreateNews(New requestNew)
        {
            New item = new New
            {
                Id = new Guid(),
                Text = requestNew.Text,
                Title = requestNew.Title,
                AuthorId = (await GetCurrentUser()).Id,
                Date = DateTime.Now
            };
            _context.News.Add(item);
            await _context.SaveChangesAsync();
        }

        private void RemoveNew(New deleteNew)
        {
            _context.News.Remove(deleteNew);
            _context.Comments.RemoveRange(deleteNew.Comments);
            _context.SaveChanges();
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return (await AdminAction()) ?? (await _userManager.GetUserAsync(User));
        }
    }
}