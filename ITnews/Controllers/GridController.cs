using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITnews.Data;
using ITnews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public async Task<string> GetDataTable()
        {
            var News = await GetNews();
            return JsonConvert.SerializeObject(News);
        }

        public async Task<string> EditData(New requestNew)
        {
            var oldNew = _context.News.Where(n => n.Id == requestNew.Id).FirstOrDefault();
            oldNew.Title = requestNew.Title;
            oldNew.Text = requestNew.Text;
            await _context.SaveChangesAsync();
            var News = await GetNews();
            return JsonConvert.SerializeObject(News);
        }

        public async Task<string> AddData(New requestNew)
        {
            New item = new New
            {
                Id = new Guid(),
                Text = requestNew.Text,
                Title = requestNew.Title,
                AuthorId = (await _userManager.GetUserAsync(User)).Id
            };
            _context.News.Add(item);
            await _context.SaveChangesAsync();
            var News = await GetNews();
            return JsonConvert.SerializeObject(News);
        }

        public async Task<string> DeleteData(Guid Id)
        {
            _context.News.Remove(_context.News.Where(n => n.Id == Id).FirstOrDefault());
            _context.SaveChanges();
            var News = await GetNews();
            return JsonConvert.SerializeObject(News);
        }

        private async Task<List<New>> GetNews()
        {

            var user =await _userManager.GetUserAsync(User);
            var usersNews = _context.News.Where(n => n.AuthorId == user.Id).ToList();
            return usersNews;
        }
    }
}