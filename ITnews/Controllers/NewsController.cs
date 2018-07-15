using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITnews.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ITnews.Models.NewViewModel;
using ITnews.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ITnews.Controllers
{
    public class NewsController : Controller
    {
       private readonly UserManager<ApplicationUser> _userManager;
       private readonly ApplicationDbContext _context;

        public NewsController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
               await CreateNews(model);
            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Search(string name)
        {
            if (name == null)
            {
                return View();
            }
            Tag tag = _context.Tags.FirstOrDefault(t=>t.Name == name);
            if (tag == null && name != null)
            {
                return SearchTextAndTitle(name);
            }
            var searchTags = _context.NewTags.Where(i=>i.TagId == tag.Id).Select(n=>n.New).Include(c => c.Comments).ToList();
            return View(searchTags);
        }

        [HttpGet]
        public JsonResult AutocompleteSearch(string term)
       {
            var models = _context.Tags.Where(a => a.Name.Contains(term))
                            .Select(a => new { value = a.Name })
                            .Distinct().ToList();

            return Json(models);

        }

        [HttpPost]
        public async Task<IActionResult> SetStar(double currentRating, string user, Guid id)
        {
            ApplicationUser author = await _userManager.FindByIdAsync(user);
            New currentNew = await _context.News.FirstOrDefaultAsync(z => z.Id == id);
            UserNew rating = CreateGrade(author, currentNew, currentRating);
            AddAndValidateRating(author, currentNew, rating);
            await CalculateRating(currentNew);
            return RedirectToAction("Index");
        }

        public PartialViewResult GetComments(Guid Id)
        {
            New news = _context.News.Include(c => c.Comments).FirstOrDefault(i => i.Id == Id);
           return PartialView("_CommentPartial", news);
        }

        [HttpPost]
        public ActionResult PostComments(Guid Id, string text, Guid author)
        {
            Сomment comment = new Сomment()
            {
                NewId = Id,
                Text = text,
                AuthorId = author,
                Date = DateTime.Now,
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("GetComments", new { Id });
        }


        [HttpPost]
        public ActionResult SetLike(Guid IdComment, Guid Id, Guid user, int likes)
        {
            UserComment userComment = new UserComment()
            {
                ApplicationUserId = user,
                СommentId = IdComment,
            };
            _context.Comments.FirstOrDefault(n=>n.Id == IdComment).Likes = likes;
            _context.SaveChanges();
            SaveLikes(userComment);
            return RedirectToAction("GetComments", new { Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        public async Task CreateNews(NewsViewModel model)
        {
            New post = new New()
            {
                Title = model.Title,
                Text = model.Text,
                AuthorId = (await _userManager.GetUserAsync(User)).Id,
                Date = DateTime.Now,
            };
            _context.News.Add(post);
            CreateTags(model.Tags, post.Id);
           await _context.SaveChangesAsync();
        }

        public UserNew CreateGrade(ApplicationUser user, New news, double rating)
        {
            UserNew userNew = new UserNew()
            {
                ApplicationUserId = user.Id,
                NewId = news.Id,
                Rating = rating,
            };
            return userNew;
        }

        private void AddAndValidateRating(ApplicationUser author, New currentNew, UserNew rating)
        {
            try
                {
                DeleteDuplicate(author, currentNew);
                SaveRating(rating);
            }
            catch (Exception)
            {
                SaveRating(rating);
            }
        }

        private void CreateTags(string tags, Guid idNew)
        {
            foreach (string tag in tags.Split(" "))
            {
                Tag tagNew = _context.Tags.FirstOrDefault(t=>t.Name == tag);
                SaveTags(idNew, ValidateTags(tagNew, tag).Id);
            }
        }

        private void SaveRating(UserNew rating)
        {
            _context.UserNews.Add(rating);
            _context.SaveChanges();
        }

        private void DeleteDuplicate(ApplicationUser author, New currentNew)
        {
           var duplicate = _context.UserNews.FirstOrDefault
           (un => un.ApplicationUserId == author.Id
           && un.NewId == currentNew.Id);
           _context.UserNews.Remove(duplicate);
        }

        public async Task CalculateRating(New currentNew)
        {
            double? res = 0;
                var calculateRating = _context.UserNews.Where(un => un.NewId == currentNew.Id).ToList();
                foreach (UserNew item in calculateRating)
                {
                    res += item.Rating;
                }
                    currentNew.Rating = res / calculateRating.Count();
                await _context.SaveChangesAsync();
            }

        private Tag ValidateTags(Tag tagNew, string tag)
        {
            if (tagNew == null)
            {
                tagNew = new Tag { Id = new Guid(), Name = tag };
                _context.Tags.Add(tagNew);
                return tagNew;
            }
            return tagNew;
        }

        private void SaveTags(Guid idNew, Guid tagId)
        {
            _context.NewTags.Add(new NewTag { NewId = idNew, TagId = tagId });
            _context.SaveChanges();
        }

        private void SaveLikes(UserComment userComment)
        {
            if (!_context.UserComments.Contains(userComment))
            {
                _context.UserComments.Add(userComment);
                _context.SaveChanges();
            }
        }

        private IActionResult SearchTextAndTitle(string name)
        {
            var searchText = _context.News.Where(s => s.Text.ToLower().Contains(name.ToLower()));
            var searchTitle = _context.News.Where(s => s.Title.ToLower().Contains(name.ToLower()));
            return View(searchTitle.Union(searchText).ToList());
        }

    }

    
}

