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
        public List<SelectListItem> subcategories { get; set; }
        private readonly ILogger<AddNewsPage> logger;
        private readonly NewsContext newsContext;

        public AddNewsPage(ILogger<AddNewsPage> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("AddInternalNewsPage");
        }
    }
}