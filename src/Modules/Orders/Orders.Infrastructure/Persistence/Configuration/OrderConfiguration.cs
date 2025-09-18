using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Persistence.Configerations
{

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerId).IsRequired();
            builder.Property(o => o.RestaurantId).IsRequired();
            builder.Property(o => o.Status).IsRequired().HasConversion<string>();
            builder.Property(o => o.PaymentStatus).IsRequired().HasConversion<string>();
            builder.Property(o => o.PaymentMethod).IsRequired().HasConversion<string>();
            builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.DeliveryAddressId).IsRequired();
            builder.Property(o => o.EstimatedDeliveryTime).IsRequired(false);
            builder.Property(o => o.ActualDeliveryTime).IsRequired(false);
            builder.Property(o => o.AssignedDriverId).IsRequired(false);
            builder.Ignore(o => o.DomainEvents);
        }
    }
}
