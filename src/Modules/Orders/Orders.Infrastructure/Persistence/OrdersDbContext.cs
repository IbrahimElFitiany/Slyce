using Microsoft.EntityFrameworkCore;
using Orders.Domain.Aggregates.Carts;
using Orders.Domain.Aggregates.Order;

namespace Orders.Infrastructure.Persistence
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("orders");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
