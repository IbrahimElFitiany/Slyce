
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Aggregates.Carts;

namespace Orders.Infrastructure.Persistence.Configurations
{
    public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CustomerId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(c => c.BranchId)
                .ValueGeneratedNever()
                .IsRequired();

            builder.OwnsMany(c => c.CartItems, ci => {

                ci.HasKey("MealId", nameof(Cart.Id));

                ci.WithOwner()
                .HasForeignKey("CartId");

                ci.Property(ci => ci.MealId)
                .IsRequired();

                ci.Property(ci => ci.SizeId)
                .IsRequired();
            });

            builder.HasIndex(c => c.CustomerId)
                .IsUnique();
        }
    }
}
