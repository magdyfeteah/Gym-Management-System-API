using GymManagementSystem.Models;
using GymManagementSystem.Responses;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}