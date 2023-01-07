using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Pages;

namespace ProiectDAW.Pages
{
    public class EditNewsPage : PageModel
    {
        public News news { get; set; }
        public List<SelectListItem> subcategories { get; set; }
        private readonly ILogger<EditNewsPage> logger;
        private readonly NewsContext newsContext;

        public EditNewsPage(ILogger<EditNewsPage> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet(int NewsId)
        {
            news=newsContext.News.Include(news=>news.SubCategory.Category).FirstOrDefault(x=>x.Id==NewsId);
            subcategories = newsContext.SubCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.SubCategoryName
            }).ToList();
        }

        public IActionResult OnPost()
        {
            newsContext.Update(news);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}