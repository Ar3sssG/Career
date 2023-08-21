using System;
using CareerDataAccess.CareerModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CareerDataAccess
{
    public class CareerDbContext : IdentityDbContext<User, Role, int>
    {
        public CareerDbContext(DbContextOptions<CareerDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplications> JobsApplications { get; set; }
        public DbSet<Industries> JobsIndustries { get; set; }
        public DbSet<Professions> Professions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UsersTokens");
        }
    }
}
