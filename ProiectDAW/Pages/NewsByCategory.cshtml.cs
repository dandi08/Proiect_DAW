using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class NewsByCategoryModel : PageModel
    {
        private readonly ILogger<NewsByCategoryModel> logger;
        private readonly NewsContext newsContext;

        public List<News> News { get; set; }
        static public List<Category> Categories { get; set; }
        public int CategoryId { get; set; } 

        public NewsByCategoryModel(ILogger<NewsByCategoryModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;

        }

        public void OnGet(int CategoryId)
        {
            News = this.newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = News.OrderByDescending(news => news.Date).ToList();
            Categories = this.newsContext.Categories.ToList();
            Categories = Categories.OrderByDescending(Categories => Categories.CategoryName).ToList();
            this.CategoryId = CategoryId;
        }
        public IActionResult OnPostAZ(int CategoryId)
        {
            var AllNews = newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = AllNews.OrderBy(news => news.NewsTitle).ToList();
            this.CategoryId = CategoryId;
            return Page();
        }
        public IActionResult OnPostZA(int CategoryId)
        {
            var AllNews = newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = AllNews.OrderByDescending(news => news.NewsTitle).ToList();
            this.CategoryId = CategoryId;
            return Page();
        }
        public IActionResult OnPostWriter(int CategoryId)
        {
            var AllNews = newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = AllNews.OrderBy(news => news.NewsTitle).ToList();
            this.CategoryId = CategoryId;
            return Page();
        }

        public IActionResult OnPost()
        {
            String title = Request.Form["searchText"];
            var AllNews = newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = AllNews.Where(news => news.NewsTitle.ToLower().Contains(title.ToLower()) || title==null).ToList();
            News = News.OrderByDescending(news => news.Date).ToList();

            return Page();
        }
    }
}