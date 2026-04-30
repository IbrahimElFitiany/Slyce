using MediatR;
using Menus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Common;

namespace Menus.Infrastructure.Persistence
{
    public sealed class MenusDbContext : DbContext
    {
        private readonly IMediator _publisher;
        public MenusDbContext( DbContextOptions<MenusDbContext> options, IMediator publisher) : base(options) 
        {
            _publisher = publisher;
        }

        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuMeal> MenuMeals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("menus");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MenusDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var result = await base.SaveChangesAsync(ct);

            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, ct);
            }

            return result;
        }
    }
}