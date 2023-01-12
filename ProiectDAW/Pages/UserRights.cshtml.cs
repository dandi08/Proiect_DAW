using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class UserRightsModel : PageModel
    {
        public static int userId = 1;
        public static int editorId = 2;

        [BindProperty]
        public Accounts account { get; set; }
        public List<Accounts> accounts { get; set; }
        public List<AccountTypes> accountTypes { get; set; }

        private readonly ILogger<UserRightsModel> logger;
        private readonly NewsContext newsContext;

        public UserRightsModel(ILogger<UserRightsModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
            accountTypes = newsContext.AccountTypes.ToList();
        }
        public void OnGet()
        {
            accounts = this.newsContext.Accounts.Include(account => account.Type).ToList();
            accounts = accounts.OrderByDescending(account => account.Username).ToList();
        }

        public IActionResult OnPostRemove()
        {
            account.TypeId = userId;
            newsContext.Update(account);
            newsContext.SaveChanges();
            return RedirectToPage("UserRights");
        }

        public IActionResult OnPostAdd()
        {
            account.TypeId = editorId;
            newsContext.Update(account);
            newsContext.SaveChanges();
            return RedirectToPage("UserRights");
        }

    }
}