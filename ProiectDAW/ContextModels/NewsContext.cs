using ProiectDAW.Models;
using Microsoft.EntityFrameworkCore;


namespace ProiectDAW.ContextModels
{
    public class NewsContext : DbContext
    {
        public NewsContext (DbContextOptions<NewsContext> options) : base (options)
        {

        }
        public DbSet<News> News { get; set; }  
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AccountTypes> AccountTypes { get; set; }
        public DbSet<Proposals> Proposals { get; set; } 

    }

}
