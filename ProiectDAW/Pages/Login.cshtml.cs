using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;


namespace ProiectDAW.Pages
{
    public class LoginModel : PageModel
    {
        public int errorCode { get; set; }
        public bool IsLoggedIn { get; set; }
        [BindProperty]
        public Accounts Account { get; set; }

        private readonly ILogger<LoginModel> logger;

        private readonly NewsContext newsContext;
        public LoginModel(ILogger<LoginModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }
        public void OnGet(int errorCode)
        {
            this.errorCode = errorCode;
            Account = new Accounts();
        }
        public IActionResult OnPost()
        {
            LoggingSystem loggingSystem = LoggingSystem.GetInstance();
            Accounts account = loggingSystem.LogIn(Account, newsContext.Accounts.Include(type => type.Type).ToList());
            if (account != null)
            {
                loggingSystem.Account = account;
                return RedirectToPage("Index");
            }

            return RedirectToPage("Login", new { errorCode = 1 });
        }

    }
}
