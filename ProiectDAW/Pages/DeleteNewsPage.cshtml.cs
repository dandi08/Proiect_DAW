using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class DeleteNewsPage : PageModel
    {
        public static News News { get; set; }
        private readonly ILogger<DeleteNewsPage> logger;
        private readonly NewsContext newsContext;
        public List<Comments> Comments { get; set; }   

        public DeleteNewsPage(ILogger<DeleteNewsPage> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet(int NewsId)
        {
            Comments = newsContext.Comments.Where(x => x.NewsId == NewsId).ToList();
            News=newsContext.News.Include(news=>news.SubCategory).FirstOrDefault(x=>x.Id==NewsId);
        }

        public IActionResult OnPost()
        {
            if(News!=null)
            {
                newsContext.Remove(News);
                newsContext.SaveChanges();
            }
            if(Comments!=null)
            {
                newsContext.Remove(Comments);
                newsContext.SaveChanges();
            }
            return RedirectToPage("Index");
        }
    }
}