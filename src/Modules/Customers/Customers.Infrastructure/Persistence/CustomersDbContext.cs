using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Persistence
{
    public sealed class CustomersDbContext: DbContext
    {
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options): base(options){}

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("customers");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
