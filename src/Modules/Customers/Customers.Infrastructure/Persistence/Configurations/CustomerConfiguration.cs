using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Customers.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Fname).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Lname).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Gender).HasConversion(typeof(string));

            builder.Property(c => c.Email)
                .HasConversion(
                    v => v == null ? null : v.Value,
                    v => v == null ? null : Email.Create(v))
                .IsRequired(false);
            builder.HasIndex(c=> c.Email).IsUnique();

            builder.Property(c=> c.ProfileImage).IsRequired(false);
            builder.Property(c=> c.Bday).IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasConversion(
                    v => v == null ? null : v.Value,
                    v => v == null ? null : PhoneNumber.Create(v))
                .IsRequired(false);
            builder.HasIndex(c => c.PhoneNumber).IsUnique();

            builder.Property(c => c.Height)
                .HasConversion(
                    v => v.Centimeters,
                    v => Height.FromCm(v))
                .IsRequired();

        }
    }
}
