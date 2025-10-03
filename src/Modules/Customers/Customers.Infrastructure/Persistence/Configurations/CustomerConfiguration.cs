using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Customers.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Email).IsRequired(false);
            builder.Property(c => c.Fname).IsRequired();
            builder.Property(c => c.Lname).IsRequired();
            builder.Property(c=> c.Gender).HasConversion(typeof(string));
        }
    }
}
