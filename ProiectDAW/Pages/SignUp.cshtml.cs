using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ProiectDAW.Pages
{
    public enum MessageType { Success, Error, Info, Warning };
    public class SignUpModel : PageModel
    {
        public int errorCode { get; set; }
        public bool IsLoggedIn { get; set; }
        [BindProperty]
        public string checkPassword { get; set; }
        [BindProperty]
        public Accounts Account { get; set; }

        private readonly ILogger<SignUpModel> logger;

        private readonly NewsContext newsContext;
        public SignUpModel(ILogger<SignUpModel> logger, NewsContext newsContext)
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
            Accounts account = loggingSystem.SearchUsername(Account, newsContext.Accounts.Include(type => type.Type).ToList());

            Regex regex = new Regex("[a-zA-Z]");

            if (!regex.IsMatch(Account.Username))
                return RedirectToPage("SignUp", new { errorCode = 3 });

            if (Account.Password != checkPassword)
                return RedirectToPage("SignUp", new { errorCode = 1 });

            if (account != null)
                return RedirectToPage("SignUp", new { errorCode = 2 });

            Account.TypeId = UserRightsModel.userId;
            newsContext.Add(Account);
            newsContext.SaveChanges();
            loggingSystem.Account = Account;
            return RedirectToPage("Index");
            
        }
    }
}
