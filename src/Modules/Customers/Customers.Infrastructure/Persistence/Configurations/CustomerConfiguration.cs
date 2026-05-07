using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.ValueObjects;


namespace Customers.Infrastructure.Persistence.Configurations
{
    public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Gender)
                .HasConversion<string>();

            builder.Property(c=> c.ProfileImage)
                .IsRequired(false);

            builder.Property(c=> c.Bday)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasConversion(
                    v => v == null ? null : v.Value,
                    v => v == null ? null : PhoneNumber.Create(v))
                .IsRequired(false);

            builder.Property(c => c.Height)
                .HasConversion(
                    v => v.Centimeters,
                    v => Height.FromCm(v))
                .IsRequired();

            builder.OwnsMany(c => c.CustomerAddresses, ca => {

                ca.ToTable("CustomerAddresses");

                ca.HasKey(ca => ca.Id);

                ca.Property(x => x.Id)
                 .ValueGeneratedNever();

                ca.WithOwner()
                .HasForeignKey("CustomerId");

                ca.Property(a => a.Label)
                .IsRequired();

                ca.Property(a => a.ContactNumber)
                .HasConversion(
                    v => v == null ? null : v.Value,
                    v => PhoneNumber.Create(v!))
                .IsRequired();

                //still no support for ComplexProperty in owned collections
                //https://github.com/dotnet/efcore/issues/33170
                // so OwnsOne is used for OperatingHours instead.
                ca.OwnsOne(a => a.Address, a => {

                    a.Property(a => a.City)
                    .HasColumnName(nameof(Address.City))
                    .IsRequired();

                    a.Property(a => a.Area)
                    .HasColumnName(nameof(Address.Area))
                    .IsRequired();

                    a.Property(a => a.StreetName)
                    .HasColumnName(nameof(Address.StreetName))
                    .IsRequired(false);

                    a.Property(a => a.StreetNumber)
                    .HasColumnName(nameof(Address.StreetNumber))
                    .IsRequired(false);

                    a.OwnsOne(a => a.Coordinates, c =>
                    {
                        c.Property(c => c.Latitude)
                        .HasColumnName(nameof(Coordinates.Latitude))
                        .IsRequired();

                        c.Property(c => c.Longitude)
                        .HasColumnName(nameof(Coordinates.Longitude))
                        .IsRequired();
                    });

                });

                ca.Property(ca => ca.IsPrimary)
                .IsRequired();

                ca.HasIndex(nameof(CustomerAddress.Label), "CustomerId").IsUnique();
            });

            builder.HasIndex(c => c.PhoneNumber).IsUnique();
        }
    }
}