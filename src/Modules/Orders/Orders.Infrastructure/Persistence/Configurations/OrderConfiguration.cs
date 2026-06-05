using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Aggregates.Order;

namespace Orders.Infrastructure.Persistence.Configurations
{

    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.BranchId)
                .IsRequired();

            builder.Property(o => o.SubscriptionId)
                .IsRequired(false);

            builder.OwnsMany(o => o.OrderItems, oi =>
            {
                oi.ToTable(nameof(Order.OrderItems));

                oi.WithOwner().HasForeignKey("OrderId");

                oi.HasKey(x => x.Id);

                oi.Property(oi => oi.MealId)
                .IsRequired();

                oi.Property(oi => oi.SizeId)
                .IsRequired();

                oi.Property(oi => oi.Quantity)
                .IsRequired();

                oi.OwnsOne(oi => oi.UnitPrice, p =>
                {
                    p.Property(x => x.Amount)
                     .HasPrecision(10, 2)
                     .HasColumnName("unit_price_amount");

                    p.Property(x => x.Currency)
                     .HasMaxLength(3)
                     .HasColumnName("unit_price_currency");
                });

                oi.OwnsOne(oi => oi.TotalPrice, p =>
                {
                    p.Property(x => x.Amount)
                     .HasPrecision(10, 2)
                     .HasColumnName("total_price_amount");

                    p.Property(x => x.Currency)
                     .HasMaxLength(3)
                     .HasColumnName("total_price_currency");
                });

                oi.Property(oi => oi.CreatedAt)
                .IsRequired();

                oi.Property(oi => oi.UpdatedAt)
                .IsRequired();

                oi.HasIndex("OrderId", nameof(OrderItem.MealId), nameof(OrderItem.SizeId))
                .IsUnique();
            });

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.ComplexProperty(o => o.TotalPrice, p =>
            {
                p.Property(x => x.Amount)
                 .HasPrecision(10, 2)
                 .HasColumnName("price_amount");

                p.Property(x => x.Currency)
                 .HasMaxLength(3)
                 .HasColumnName("price_currency");
            });

            builder.Property(o => o.PaymentStatus)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(o => o.PaymentMethod)
                .IsRequired()
                .HasConversion<string>();

            builder.ComplexProperty(o => o.DeliveryAddress, a =>
            {
                a.Property(a => a.City)
                .IsRequired();

                a.Property(a => a.Area)
                .IsRequired();

                a.Property(a => a.StreetName)
                .IsRequired();

                a.Property(a => a.StreetNumber)
                .IsRequired();

                a.ComplexProperty(a => a.Coordinates, c =>
                {
                    c.Property(c => c.Latitude)
                    .IsRequired();

                    c.Property(c => c.Longitude)
                    .IsRequired();
                });
            });

            builder.OwnsOne(o => o.DeliveryTimeFrame, tf =>
            {
                tf.Property(t => t.From).HasColumnName("DeliveryTimeFrame_From");
                tf.Property(t => t.To).HasColumnName("DeliveryTimeFrame_To");
            });

            builder.Property(o => o.EstimatedDeliveryTime)
                .IsRequired(false);

            builder.Property(o => o.ActualDeliveryTime)
                .IsRequired(false);

            builder.Property(o => o.AssignedDriverId)
                .IsRequired(false);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .IsRequired();

            builder.Ignore(o => o.DomainEvents);
        }
    }
}