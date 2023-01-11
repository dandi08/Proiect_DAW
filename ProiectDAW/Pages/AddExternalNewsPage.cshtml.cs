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
        public List<SelectListItem> categories { get; set; }
        private readonly ILogger<AddExternalNewsPage> logger;
        private readonly NewsContext newsContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IFormFile Image { get; set; }

        public AddExternalNewsPage(ILogger<AddExternalNewsPage> logger, NewsContext newsContext, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.newsContext = newsContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            news = new News();
            categories = newsContext.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.CategoryName
            }).ToList();
        }
        [HttpPost]
        public IActionResult OnPost()
        {
            string fileName = null;
            if (Image != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            string stringFileName = fileName;
            news.Image = stringFileName;
            newsContext.Add(news);
            newsContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}