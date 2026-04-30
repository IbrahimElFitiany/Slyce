using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Infrastructure.Persistence
{
    public sealed class SubscriptionsDbContext : DbContext
    {
        public SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> options) : base(options) { }

        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Subscriptions");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}