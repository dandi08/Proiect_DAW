using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class NewsPageModel : PageModel
    {
        
        public News News { get; set; }
        [BindProperty]
        public Comments comm { get; set; }
        private readonly ILogger<NewsPageModel> logger;
        private readonly NewsContext newsContext;
        public List<Comments> Comments { get; set; }   
        public string page { get; set; }

        public NewsPageModel(ILogger<NewsPageModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet(int NewsId)
        {
            comm = new Comments();
            comm.NewsId=NewsId;
            Comments = newsContext.Comments.Where(x => x.NewsId == NewsId).ToList();
            News=newsContext.News.Include(news=>news.SubCategory).FirstOrDefault(x=>x.Id==NewsId);
            page = "NewsPage" + "/" + News.Id.ToString();

        }
        public IActionResult OnPost()
        {
            newsContext.Add(comm);
            newsContext.SaveChanges();
            return RedirectToPage(page);
        }
    }
}