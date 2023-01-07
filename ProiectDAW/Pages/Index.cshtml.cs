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

        public IndexModel(ILogger<IndexModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet()
        {
            News = this.newsContext.News.Include(news => news.SubCategory.Category).ToList();
            News=News.OrderByDescending(news => news.Date).ToList();
        }

        public IActionResult OnPost(String searchText)
        {
            String title = searchText;
            var AllNews = newsContext.News.Include(news => news.SubCategory).ToList();
            News = AllNews.Where(news => news.NewsTitle.Contains(title) || title==null).ToList();

            return Page();
        }
    }
}