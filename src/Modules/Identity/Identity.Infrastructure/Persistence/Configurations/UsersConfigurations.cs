using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Identity.Domain.Aggregates;
using Shared.Domain.ValueObjects;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired();

            builder.Property(u => u.LastName)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasConversion(
                    v => v == null ? null : v.Value,
                    v => v == null ? null : Email.Create(v))
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .HasConversion(
                    v => v == null ? null : v.Value,
                    v => v == null ? null : PhoneNumber.Create(v))
                .IsRequired(false);

            builder.Property(u => u.PasswordHash)
                .IsRequired(false);

            builder.Property(u => u.UserType)
                .IsRequired()
                .HasConversion<string>();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(c => c.PhoneNumber)
                .IsUnique();
        }
    }
}