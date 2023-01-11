using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class AddExternalNewsPage : PageModel
    {
        [BindProperty]

        public News news { get; set; }
        public List<SelectListItem> subcategories { get; set; }
        private readonly ILogger<AddExternalNewsPage> logger;
        private readonly NewsContext newsContext;

        public AddExternalNewsPage(ILogger<AddExternalNewsPage> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet()
        {
            news = new News();
            subcategories = newsContext.SubCategories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.SubCategoryName
            }).ToList();
        }

        public IActionResult OnPost()
        {
            newsContext.Add(news);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}