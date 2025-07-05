using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ahrenburg.city.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ahrenburg.city.Models.BlogPost> BlogPosts { get; set; }
        // Hier können weitere DbSets für eigene Modelle ergänzt werden
    }
}
