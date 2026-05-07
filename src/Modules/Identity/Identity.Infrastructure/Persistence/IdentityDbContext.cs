using Identity.Domain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Common;

namespace Identity.Infrastructure.Persistence
{
    public sealed class IdentityDbContext : DbContext
    {
        private readonly IMediator _publisher;
        public IdentityDbContext( DbContextOptions<IdentityDbContext> options, IMediator publisher) : base(options) 
        {
            _publisher = publisher;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

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