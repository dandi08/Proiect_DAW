using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class AddInternalNewsPage : PageModel
    {
        [BindProperty]

        public News news { get; set; }
        [BindProperty]
        public Category category { get; set; }
        [BindProperty]
        public SubCategory subCategory { get; set; }
        public List<SelectListItem> subcategories { get; set; }
        public List<SelectListItem> categories { get; set; }
        private readonly ILogger<AddInternalNewsPage> logger;
        private readonly NewsContext newsContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public int errorCode { get; set; }
        public IFormFile Image { get; set; }

        public AddInternalNewsPage(ILogger<AddInternalNewsPage> logger, NewsContext newsContext, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.newsContext = newsContext;
            this.webHostEnvironment = webHostEnvironment;
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
        [HttpPost]
        public IActionResult OnPostAddInternalNews()
        {
            if (news.Writer == null || news.NewsTitle == null || news.Lead == null || news.Body == null)
                return RedirectToPage("AddInternalNewsPage", new { NewsId = news.Id, errorCode = 1 });
            string fileName = null;
            if(Image != null)
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

        public IActionResult OnPostAddCategory()
        {
            if (category.CategoryName == null)
                return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 1 });
            newsContext.Add(category);
            newsContext.SaveChanges();
            return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 0 });
        }

        public IActionResult OnPostAddSubCategory()
        {
            if (subCategory.SubCategoryName == null)
                return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 1 });
            newsContext.Add(category);
            newsContext.SaveChanges();
            return RedirectToPage("AddNewsPage", new { NewsId = news.Id, errorCode = 0 });
        }

    }
}