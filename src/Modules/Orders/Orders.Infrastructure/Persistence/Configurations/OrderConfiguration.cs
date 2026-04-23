using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Aggregates.Order;

namespace Orders.Infrastructure.Persistence.Configurations
{

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.RestaurantId)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(o => o.PaymentStatus)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(o => o.PaymentMethod)
                .IsRequired()
                .HasConversion<string>();

            builder.OwnsOne(o => o.TotalPrice, p =>
            {
                p.Property(x => x.Amount)
                 .HasPrecision(10, 2)
                 .HasColumnName("price_amount");

                p.Property(x => x.Currency)
                 .HasMaxLength(3)
                 .HasColumnName("price_currency");
            });

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.DeliveryAddressId)
                .IsRequired();

            builder.Property(o => o.EstimatedDeliveryTime)
                .IsRequired(false);

            builder.Property(o => o.ActualDeliveryTime)
                .IsRequired(false);

            builder.Property(o => o.AssignedDriverId)
                .IsRequired(false);

            builder.Ignore(o => o.DomainEvents);
        }
    }
}
