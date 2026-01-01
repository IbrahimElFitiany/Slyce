using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Menus.Domain.Entities;

namespace Menus.Infrastructure.Persistence.Configurations
{
    public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder.HasKey(mc => mc.Id);
            builder.Property(mc => mc.RestaurantId).IsRequired();
            builder.Property(mc => mc.Name).IsRequired().HasMaxLength(200);
            builder.HasIndex(mc => new { mc.RestaurantId, mc.Name }).IsUnique();
        }
    }
}
