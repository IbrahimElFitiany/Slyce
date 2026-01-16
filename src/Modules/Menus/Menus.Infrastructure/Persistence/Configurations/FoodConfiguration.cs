using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Menus.Domain.Entities;

namespace Menus.Infrastructure.Persistence.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name).IsRequired().HasMaxLength(255);

            builder.Property(f => f.Image).IsRequired().HasMaxLength(255);

            builder.Property(f => f.Source).HasMaxLength(50);

            builder.ComplexProperty(f => f.NutritionPer100g, n =>
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
