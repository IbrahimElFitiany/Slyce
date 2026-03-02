using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Domain.Entities;
using Subscriptions.Domain.ValueObjects;

namespace Subscriptions.Infrastructure.Persistence.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Property(s => s.CustomerId)
                .IsRequired();

            builder.Property(s => s.BranchId)
                .IsRequired();

            builder.Property(s => s.DeliveryAddressId)
                .IsRequired();

            builder.ComplexProperty(s => s.TimeFrame, dt =>
            {
                dt.Property(dt => dt.From)
                .IsRequired();

                dt.Property(dt => dt.To)
                .IsRequired();
            });

            builder.OwnsMany(s => s.DeliveryDays, deliveryDay =>
            {
                deliveryDay.ToTable("SubscriptionDeliveryDays");

                deliveryDay.HasKey(nameof(SubscriptionDeliveryDay.Day), "SubscriptionId");

                deliveryDay.WithOwner()
                .HasForeignKey("SubscriptionId");

                deliveryDay.Property(deliveryDay => deliveryDay.Day)
                .HasConversion<string>()
                .IsRequired();
            });

            builder.Property(s => s.StartDate)
                .IsRequired();

            builder.Property(s => s.EndDate)
                .IsRequired();

            builder.OwnsMany(s => s.SubscriptionMeals, m =>
            {
                m.ToTable(nameof(Subscription.SubscriptionMeals));

                m.HasKey(nameof(SubscriptionMeal.MealId),nameof(SubscriptionMeal.SizeId), "SubscriptionId");

                m.WithOwner()
                .HasForeignKey("SubscriptionId");

                m.Property(m => m.MealId)
                .IsRequired();

                m.Property(m => m.SizeId)
                .IsRequired();

                m.Property(m => m.Quantity)
                .IsRequired();

                m.OwnsOne(m => m.PriceAtSubscription, p =>
                {  
                    p.Property(p => p.Amount)
                    .HasPrecision(18, 2)
                    .IsRequired();

                    p.Property(p => p.Currency)
                    .IsRequired();
                });
            });

            builder.ComplexProperty(s => s.TotalPrice, p =>
            {
                p.Property(p => p.Amount)
                .HasPrecision(18,2)
                .IsRequired();

                p.Property(p => p.Currency)
                .IsRequired();
            });

            builder.Property(s => s.BillingCycle)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.ToTable(s => s.HasCheckConstraint(
                "CK_DeliveryTime",
                "\"EndDate\" > \"StartDate\""
            ));
            builder.HasIndex(s => s.CustomerId);
            builder.HasIndex(s => s.BranchId);

        }
    }
}