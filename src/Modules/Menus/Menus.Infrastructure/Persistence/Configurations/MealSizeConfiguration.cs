using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Menus.Domain.Entities;

namespace Menus.Infrastructure.Persistence.Configurations
{
    public class MealSizeConfiguration : IEntityTypeConfiguration<MealSize>
    {
        public void Configure(EntityTypeBuilder<MealSize> builder)
        {
            builder.HasKey(ms => ms.Id);

            builder.Property(ms => ms.MenuItemId).IsRequired();

            builder.HasOne<MenuMeal>()
                .WithMany()
                .HasForeignKey(ms => ms.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ms => ms.Name).IsRequired().HasMaxLength(300);

            builder.ComplexProperty(ms => ms.Price, p =>
            {
                p.Property(x => x.Amount)
                 .HasPrecision(10, 2)
                 .HasColumnName("price_amount");
                p.Property(x => x.Currency)
                 .HasMaxLength(3)
                 .HasColumnName("price_currency");
            });

            builder.ComplexProperty(ms => ms.Nutrition, n =>
            {
                n.Property(x => x.Calories).HasColumnName("Calories");
                n.Property(x => x.TotalFat).HasColumnName("TotalFat");
                n.Property(x => x.SaturatedFat).HasColumnName("SaturatedFat");
                n.Property(x => x.TransFat).HasColumnName("TransFat");
                n.Property(x => x.Cholesterol).HasColumnName("Cholesterol");
                n.Property(x => x.SodiumMg).HasColumnName("SodiumMg");
                n.Property(x => x.TotalCarbohydrate).HasColumnName("TotalCarbohydrate");
                n.Property(x => x.DietaryFiber).HasColumnName("DietaryFiber");
                n.Property(x => x.SugarGrams).HasColumnName("SugarGrams");
                n.Property(x => x.Protein).HasColumnName("Protein");
                n.Property(x => x.VitaminD).HasColumnName("VitaminD");
                n.Property(x => x.CalciumMg).HasColumnName("CalciumMg");
                n.Property(x => x.IronMg).HasColumnName("IronMg");
                n.Property(x => x.PotassiumMg).HasColumnName("PotassiumMg");
                n.Property(x => x.VitaminAMcg).HasColumnName("VitaminAMcg");
                n.Property(x => x.VitaminCMg).HasColumnName("VitaminCMg");
            });

        }
    }
}
