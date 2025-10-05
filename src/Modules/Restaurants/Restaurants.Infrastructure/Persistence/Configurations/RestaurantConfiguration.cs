using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence.Configurations
{
    public sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();
            builder.Property(r => r.Image).IsRequired(false).HasMaxLength(500);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(1000);
            builder.Property(r => r.Type).HasConversion<string>().IsRequired();
            builder.Property(r=> r.Status).HasConversion<string>().IsRequired();
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.UpdatedAt).IsRequired();
        }
    }
}
