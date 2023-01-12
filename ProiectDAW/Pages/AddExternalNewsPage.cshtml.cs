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
        [BindProperty]
        public Category category { get; set; }
        public List<SelectListItem> categories { get; set; }
        private readonly ILogger<AddExternalNewsPage> logger;
        private readonly NewsContext newsContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IFormFile Image { get; set; }
        public int errorCode { get; set; }

        public AddExternalNewsPage(ILogger<AddExternalNewsPage> logger, NewsContext newsContext, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.newsContext = newsContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet(int errorCode)
        {
            news = new News();
            this.errorCode=errorCode;
        }
        [HttpPost]
        public IActionResult OnPostAddExternalNews()
        {
            news.SubCategoryId = 2007;
            if (news.NewsTitle == null || news.Body == null)
                return RedirectToPage("AddExternalNewsPage", new { NewsId = news.Id, errorCode = 1 });
            string fileName = null;
            if (Image != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
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