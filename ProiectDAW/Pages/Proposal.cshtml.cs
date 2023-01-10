using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectDAW.Models;
using ProiectDAW.ContextModels;
using Microsoft.EntityFrameworkCore;

namespace ProiectDAW.Pages
{
    public class ProposalModel : PageModel
    {
        private readonly ILogger<ProposalModel> logger;
        private readonly NewsContext newsContext;
        public List<Proposals> Prop { get; set; }
        [BindProperty]
        public Proposals proposal { get; set; } 

        public ProposalModel(ILogger<ProposalModel> logger, NewsContext newsContext)
        {
            this.logger = logger;
            this.newsContext = newsContext;
        }

        public void OnGet()
        {
            Prop = this.newsContext.Proposals.ToList();
            Prop=Prop.OrderByDescending(x=>x.Date).ToList();
        }

        public IActionResult OnPost(String searchText)
        {
            newsContext.Add(proposal);
            newsContext.SaveChanges();
            return RedirectToPage("/Proposal");
        }
    }
}