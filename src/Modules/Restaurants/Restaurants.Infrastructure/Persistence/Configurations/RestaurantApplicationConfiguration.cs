using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Restaurants.Infrastructure.Persistence.Configurations
{
    public sealed class RestaurantApplicationConfiguration : IEntityTypeConfiguration<RestaurantApplication>
    {
        public void Configure(EntityTypeBuilder<RestaurantApplication> builder)
        {
            builder.HasKey(ra => ra.Id);

            builder.Property(ra => ra.BrandName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ra => ra.OwnerFirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ra => ra.OwnerLastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ra => ra.CompanyEmail)
                .IsRequired()
                .HasMaxLength(200)
                .HasConversion(e => e.Value, v => Email.Create(v));

            builder.Property(ra => ra.OwnerEmail)
                .IsRequired()
                .HasMaxLength(200)
                .HasConversion(e => e.Value, v => Email.Create(v));

            builder.Property(ra => ra.OwnerMobileNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion(p => p.Value, v => PhoneNumber.Create(v));

            builder.Property(ra => ra.CompanyMobileNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion(p => p.Value, v => PhoneNumber.Create(v));

            builder.Property(ra => ra.RestaurantType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(ra => ra.BranchCount)
                .IsRequired();

            builder.ComplexProperty(ra => ra.MainBranchLocation, bl => { 
                
                bl.Property(bl => bl.City)
                .HasColumnName("City")
                .IsRequired();

                bl.Property(bl => bl.Area)
                .HasColumnName("Area")
                .IsRequired();

                bl.Property(bl => bl.StreetName)
                .HasColumnName("StreetName")
                .IsRequired(false);

                bl.Property(bl => bl.StreetNumber)
                .HasColumnName("StreetNumber")
                .IsRequired(false);

                bl.ComplexProperty(bl => bl.Coordinates, c =>
                {
                    c.Property(c => c.Latitude)
                    .HasColumnName("Latitude")
                    .IsRequired();

                    c.Property(c => c.Longitude)
                    .HasColumnName("Longitude")
                    .IsRequired();
                });
            });

            builder.Property(ra => ra.Description)
                .HasMaxLength(1000);

            builder.Property(ra => ra.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(ra => ra.RejectionReason)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(ra => ra.SubmittedAt)
                .IsRequired();

            builder.Property(ra => ra.ReviewedAt)
                .IsRequired(false);

            builder.Property(ra => ra.ReviewedBy)
                .IsRequired(false);

            builder.HasIndex(ra => ra.BrandName).IsUnique();
            builder.HasIndex("CompanyEmail").IsUnique();
        }
    }
}