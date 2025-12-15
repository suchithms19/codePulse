using Microsoft.EntityFrameworkCore;
using codePulse.AI.Models.Domain;

namespace codePulse.AI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}