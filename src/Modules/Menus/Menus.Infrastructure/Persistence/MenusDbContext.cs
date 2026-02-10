using Menus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Menus.Infrastructure.Persistence
{
    public sealed class MenusDbContext : DbContext
    {
        public MenusDbContext(DbContextOptions<MenusDbContext> options) : base(options) { }

        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuMeal> MenuMeals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("menus");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MenusDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}