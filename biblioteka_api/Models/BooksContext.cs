using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace biblioteka_api.Models
{
    public class BooksContext : IdentityDbContext<IdentityUser>
    {
        public BooksContext(DbContextOptions<BooksContext>options):base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("Claim_role");
            builder.Entity<IdentityUserClaim<string>>().ToTable("Claim_user");
            builder.Entity<IdentityUserToken<string>>().ToTable("Token");
            builder.Ignore<IdentityUserLogin<string>>();
        }

        public DbSet<Book> Books { get; set; } = null; 
        public DbSet<Request> Requests { get; set; } = null;
        public DbSet<User> Users { get; set; } = null;
        
    }
}
