using FoodEntity = Food.Domain.Entities.Food;
using Microsoft.EntityFrameworkCore;

namespace Food.Infrastructure.Persistence
{
    public sealed class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) { }
        public DbSet<FoodEntity> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Food");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}