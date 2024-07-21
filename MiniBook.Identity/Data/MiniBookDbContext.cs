

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniBook.Identity.Models;

namespace MiniBook.Identity.Data
{
    public class MiniBookDbContext : IdentityDbContext<User>
    {
        public MiniBookDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(users =>
            {
                users.HasMany(x => x.Claims).WithOne().HasForeignKey(x=>x.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                users.ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            });
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRole");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
