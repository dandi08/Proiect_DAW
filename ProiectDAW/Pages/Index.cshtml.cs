using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly NewsContext newsContext;

        public List<News> News { get; set; }
        static public List<Category> Categories { get; set; }

        public IndexModel(ILogger<IndexModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet()
        {
            News = this.newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = News.OrderByDescending(news => news.Date).ToList();
            Categories = this.newsContext.Categories.ToList();
            Categories = Categories.OrderByDescending(Categories => Categories.CategoryName).ToList();
        }
        [HttpPost]
        public IActionResult OnPost()
        {
            String title = Request.Form["searchText"];
            var AllNews = newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News = AllNews.Where(news => news.NewsTitle.Contains(title) || title==null).ToList();
            News = News.OrderByDescending(news => news.Date).ToList();

            return Page();
        }
    }
}