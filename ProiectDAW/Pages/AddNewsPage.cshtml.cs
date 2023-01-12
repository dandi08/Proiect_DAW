using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class AddNewsPage : PageModel
    {
        [BindProperty]
        public News news { get; set; }
        [BindProperty]
        public Category category { get; set; }
        [BindProperty]
        public SubCategory subCategory { get; set; }
        public List<SelectListItem> categories { get; set; }
        public List<SelectListItem> subcategories { get; set; }
        private readonly ILogger<AddNewsPage> logger;
        private readonly NewsContext newsContext;
        public int errorCode { get; set; }
        public AddNewsPage(ILogger<AddNewsPage> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet(int errorCode)
        {
            news = new News();
            subcategories = newsContext.SubCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.SubCategoryName
            }).ToList();

            categories = newsContext.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.CategoryName
            }).ToList();
            this.errorCode = errorCode;
        }

        public IActionResult OnPostAddNews()
        {
            if (news.Writer == null || news.NewsTitle == null || news.NewsTitle == null || news.Lead == null || news.Body == null || news.NewsTitle == null)
                return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 1 });
            newsContext.Add(news);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostAddCategory()
        {
            if (category.CategoryName == null)
                return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 1 });
            newsContext.Add(category);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }

        public IActionResult OnPostAddSubCategory()
        {
            if (subCategory.SubCategoryName == null)
                return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 1 });
            newsContext.Add(subCategory);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}