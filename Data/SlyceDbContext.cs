using Microsoft.EntityFrameworkCore;
using SlyceAPI.Models;
using System;

namespace SlyceAPI.Data
{
    public class SlyceDbContext: DbContext
    {
        public SlyceDbContext(DbContextOptions<SlyceDbContext> options): base(options) { }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Phone)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Customer>()
                .Property(c => c.Gender)
                .HasConversion<string>();
        }
    }
}
