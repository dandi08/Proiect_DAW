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
    }


}

