using Customers.Domain.Enums;
using Customers.Domain.Exceptions;
using Customers.Domain.ValueObjects;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Customers.Domain.Entities
{
    public sealed class Customer
    {
        private const int MinimumAge = 18;

        public Guid Id { get; private init; }
        public string Fname { get; private set; } = null!;
        public string Lname { get; private set; } = null!;
        public Gender? Gender { get;  private set; }
        public Email? Email { get; private set; }
        public string? ProfileImage { get; private set; }
        public DateOnly Bday { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
        public Height Height { get; private set; } = null!;

        private readonly List<CustomerAddress> _customerAddresses = new ();
        public IReadOnlyCollection<CustomerAddress> CustomerAddresses => _customerAddresses;
        public DateTime CreatedAt { get; private set; } 
        public DateTime UpdatedAt { get; private set; }

        private Customer() { }

        public Customer(
            string fname,
            string lname,
            Email? email,
            PhoneNumber? phoneNumber,
            Height height,
            DateOnly bday) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fname, nameof(fname));
            ArgumentException.ThrowIfNullOrWhiteSpace(lname, nameof(lname));

            if (email is null && phoneNumber is null)
                throw new NoContactMethodException();

            EnsureValidAge(bday);

            Id = Guid.NewGuid();
            Fname = fname;
            Lname = lname;
            Email = email;
            PhoneNumber = phoneNumber;
            Height = height;
            Bday = bday;

            CreatedAt = UpdatedAt =  DateTime.UtcNow;
        }

        public void AddAddress(CustomerAddress newAddress)
        {
            ArgumentNullException.ThrowIfNull(newAddress, nameof(newAddress));

            if (_customerAddresses.Count == 0)
                newAddress.SetAsPrimary();

            if (_customerAddresses.Any(a => a.Label == newAddress.Label))
                throw new DuplicateException(nameof(CustomerAddress.Label), newAddress.Label);

            _customerAddresses.Add(newAddress);
        }

        public void UpdateEmail(string email)
        {
            Email = Email.Create(email);
            UpdatedAt = DateTime.UtcNow;
        }


        private static void EnsureValidAge(DateOnly birthday)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;

            if (age < MinimumAge)
                throw new CustomerBelowMinimumAgeException(age, MinimumAge);
        }

    }
}