using Microsoft.EntityFrameworkCore;
using compito_17_03.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace compito_17_03.Data
{
    public class Compito1703DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public Compito1703DbContext(DbContextOptions<Compito1703DbContext> options) : base(options) { }

        public DbSet<Studente> Studenti { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.User).WithMany(u => u.ApplicationUserRole).HasForeignKey(a => a.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.Role).WithMany(r => r.ApplicationUserRole).HasForeignKey(a => a.RoleId);
        }

    }
}
