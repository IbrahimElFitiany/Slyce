using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Menus.Domain.Entities;

namespace Menus.Infrastructure.Persistence.Configurations
{
    public class MenuMealConfiguration : IEntityTypeConfiguration<MenuMeal>
    {
        public void Configure(EntityTypeBuilder<MenuMeal> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CategoryId).IsRequired();
            builder.Property(m => m.Name).IsRequired().HasMaxLength(300);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(2000);
            builder.Property(m => m.Image).IsRequired().HasMaxLength(500);
            builder.Property(m => m.Available).IsRequired();
            builder.HasOne<MenuCategory>()
                   .WithMany()
                   .HasForeignKey(m => m.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
