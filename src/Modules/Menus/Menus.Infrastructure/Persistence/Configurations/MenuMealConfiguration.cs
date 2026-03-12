using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Menus.Domain.Entities;
using Menus.Domain.ValueObjects;

namespace Menus.Infrastructure.Persistence.Configurations
{
    public class MenuMealConfiguration : IEntityTypeConfiguration<MenuMeal>
    {
        public void Configure(EntityTypeBuilder<MenuMeal> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CategoryId)
                .IsRequired();

            builder.Property(m => m.RestaurantId)
                .IsRequired();

            builder.HasOne<MenuCategory>()
               .WithMany()
               .HasForeignKey(m => m.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(m => m.Image)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.Available)
                .IsRequired();

            builder.Property(m => m.Reviewed)
                .IsRequired();

            builder.OwnsMany(m => m.Ingredients, i => {

                i.WithOwner().HasForeignKey("MealId");

                i.Property(i => i.FoodId)
                .IsRequired();

                i.Property(i => i.Name)
                .IsRequired();

                i.HasKey("MealId", nameof(MealIngredient.FoodId));

            });

            builder.OwnsMany(m => m.Sizes, ms =>
            {
                ms.ToTable("MealSizes");

                ms.HasKey(ms => ms.Id);
                
                ms.WithOwner().HasForeignKey("MealId");

                ms.Property(ms => ms.Name)
                .IsRequired()
                .HasMaxLength(300);

                // EF Core does not support using ComplexProperty inside owned collections yet,
                //https://github.com/dotnet/efcore/issues/33170
                // so OwnsOne is used for Price and Nutrition instead.

                ms.OwnsOne(ms => ms.Price, p =>
                {
                    p.Property(x => x.Amount)
                     .HasPrecision(10, 2)
                     .HasColumnName("price_amount");

                    p.Property(x => x.Currency)
                     .HasMaxLength(3)
                     .HasColumnName("price_currency");
                });

                ms.Property(ms => ms.SortOrder)
                .IsRequired();

                ms.OwnsOne(ms => ms.Nutrition, n =>
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

                ms.OwnsMany(ms => ms.IngredientQuantities, iq =>
                {
                    iq.ToTable("IngredientQuantities");

                    iq.WithOwner()
                    .HasForeignKey("MealSizeId");

                    iq.Property(mi => mi.MealIngredientId)
                      .IsRequired();

                    iq.Property(mi => mi.Quantity)
                    .HasPrecision(8, 2)
                    .IsRequired();

                    iq.HasKey("MealSizeId", nameof(IngredientQuantity.MealIngredientId));
                });

                ms.HasIndex("MealId", nameof(MealSize.Name)).IsUnique();
                ms.HasIndex("MealId", nameof(MealSize.SortOrder)).IsUnique();
            });

            builder.HasIndex(m => new { m.RestaurantId, m.Name })
                .IsUnique();
        }
    }
}