using Customers.Domain.Enums;
using Customers.Domain.ValueObjects;

namespace Customers.Domain.Entities
{
    public sealed class Customer
    {
        public Guid Id { get; private init; }
        public string Fname { get; private set; }
        public string Lname { get; private set; }
        public Gender? Gender { get;  private set; }
        public Email? Email { get; private set; }
        public string? ProfileImage { get; private set; }
        public DateOnly Bday { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
        public Height Height { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.Date;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.Date;

        public Customer(
            string fname,
            string lname,
            Email? email,
            PhoneNumber? phoneNumber,
            Height height,
            DateOnly bday) 
        {
            if (string.IsNullOrWhiteSpace(fname))
                throw new ArgumentException("First name is required.", nameof(fname));

            if (string.IsNullOrWhiteSpace(lname))
                throw new ArgumentException("Last name is required.", nameof(lname));

            if (email is null && phoneNumber is null)
                throw new ArgumentException("Customer must have at least one contact method.");

            if ((DateTime.Today.Year - bday.Year) < 13)
                throw new ArgumentException("Customer must be at least 13 years old.", nameof(bday));

            Id = Guid.NewGuid();
            Fname = fname;
            Lname = lname;
            Email = email;
            PhoneNumber = phoneNumber;
            Height = height;
            Bday = bday;
        }

        public void UpdateEmail(string email)
        {
            Email = Email.Create(email);
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
