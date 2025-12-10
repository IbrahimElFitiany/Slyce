using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence.Configurations
{
    public sealed class RestaurantApplicationConfiguration : IEntityTypeConfiguration<RestaurantApplication>
    {
        public void Configure(EntityTypeBuilder<RestaurantApplication> builder)
        {
            builder.Property(r => r.OwnerFirstName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.OwnerLastName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.CompanyEmail).IsRequired().HasMaxLength(200);
            builder.Property(r => r.MobileNumber).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Description).HasMaxLength(1000);
            builder.Property(r => r.Status).HasConversion<string>().IsRequired();
            builder.Property(r => r.RestaurantType).HasConversion<string>().IsRequired();
            builder.Property(r => r.BrandName).IsRequired().HasMaxLength(200);
            builder.Property(r => r.RejectionReason).HasMaxLength(1000).IsRequired(false);
        }
    }
}