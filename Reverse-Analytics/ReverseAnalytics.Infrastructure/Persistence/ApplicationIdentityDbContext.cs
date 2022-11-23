using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReverseAnalytics.Infrastructure.Persistence
{
    public class ApplicationIdentityDbContext : IdentityDbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity => entity.ToTable("User"));
            builder.Entity<IdentityRole>(entity => entity.ToTable("Role"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("User_Role"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("User_Claim"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("Role_Claim"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("User_Login"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("User_Token"));
        }
    }
}
