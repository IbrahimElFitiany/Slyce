using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;
using Restaurants.Domain.ValueObjects;
using Shared.Domain.ValueObjects;
using System.Reflection.Emit;
using System;

namespace Restaurants.Infrastructure.Persistence.Configurations
{
    public sealed class RestaurantBranchConfiguration : IEntityTypeConfiguration<RestaurantBranch>
    {
        public void Configure(EntityTypeBuilder<RestaurantBranch> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.RestaurantId)
                .IsRequired();

            builder.ComplexProperty(b => b.Address, builder =>
            {
                builder.Property(a => a.City)
                .HasColumnName(nameof(Address.City))
                .IsRequired();

                builder.Property(a => a.Area)
                .HasColumnName(nameof(Address.Area))
                .IsRequired();

                builder.Property(a => a.StreetName)
                .HasColumnName(nameof(Address.StreetName))
                .IsRequired(false);

                builder.Property(a => a.StreetNumber)
                .HasColumnName(nameof(Address.StreetNumber))
                .IsRequired(false);

                builder.ComplexProperty(a => a.Coordinates, builder =>
                {
                    builder.Property(c => c.Latitude)
                    .HasColumnName(nameof(Coordinates.Latitude))
                    .IsRequired();

                    builder.Property(c => c.Longitude)
                    .HasColumnName(nameof(Coordinates.Longitude))
                    .IsRequired();
                });

            });

            builder.ComplexProperty(b => b.PhoneNumber, builder =>
            {
                builder.Property(n => n.Value)
                .HasColumnName(nameof(PhoneNumber))
                .IsRequired();
            });

            //TODO: basic config considering jsonb to avoid joins on fetch
            builder.OwnsMany(b => b.Schedule, ds =>
            {
                ds.ToTable(nameof(DailySchedule));

                ds.WithOwner()
                 .HasForeignKey("BranchId");

                ds.HasKey("BranchId", nameof(DailySchedule.Day));

                ds.Property(s => s.Day)
                .HasConversion<string>();


                //still no support for ComplexProperty in owned collections
                //https://github.com/dotnet/efcore/issues/33170
                // so OwnsOne is used for OperatingHours instead.
                ds.OwnsOne(s => s.OperatingHours, oh =>
                {
                    oh.Property(oh => oh.OpeningTime)
                    .HasColumnName(nameof(OperatingHours.OpeningTime))
                    .IsRequired();

                    oh.Property(oh => oh.ClosingTime)
                    .HasColumnName(nameof(OperatingHours.ClosingTime))
                    .IsRequired();
                });

            });

            
            builder
                .HasOne<Restaurant>()
                .WithMany()
                .HasForeignKey(b => b.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}