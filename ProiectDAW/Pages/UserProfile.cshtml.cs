using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly ILogger<UserRightsModel> logger;
        private readonly NewsContext newsContext;

        public List<AccountTypes> accountTypes { get; set; }

        public UserProfileModel(ILogger<UserRightsModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
            accountTypes = newsContext.AccountTypes.ToList();
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            LoggingSystem.GetInstance().Account = null;
            return Redirect("Index");
        }
    }
}