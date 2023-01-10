using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class RightsControlModel : PageModel
    {
        private readonly ILogger<RightsControlModel> logger;
        private readonly NewsContext newsContext;

        public RightsControlModel(ILogger<RightsControlModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet()
        {
        }

        public void OnPost(String searchText)
        {
        }
    }
}