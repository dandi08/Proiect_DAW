using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Pages;

namespace ProiectDAW.Pages
{
    public class EditNewsPageModel : PageModel
    {
        [BindProperty]
        public News news { get; set; }
        public List<SelectListItem> subcategories { get; set; }
        private readonly ILogger<EditNewsPageModel> logger;
        private readonly NewsContext newsContext;
        public int errorCode { get; set; }

        public EditNewsPageModel(ILogger<EditNewsPageModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet(int NewsId, int errorCode)
        {
            news = newsContext.News.Include(news => news.SubCategory.Category).FirstOrDefault( x=> x.Id == NewsId);
            subcategories = newsContext.SubCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.SubCategoryName
            }).ToList();
            this.errorCode = errorCode;
        }

        public IActionResult OnPost()
        {
            if (news.NewsTitle == null || news.Lead == null || news.Body == null)
                return RedirectToPage("EditNewsPage", new { errorCode = 1 });
            newsContext.Update(news);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}