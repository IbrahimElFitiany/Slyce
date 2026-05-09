using Customers.Domain.Enums;
using Customers.Domain.Exceptions;
using Customers.Domain.ValueObjects;
using Shared.Domain.Common;
using Shared.Domain.Exceptions;

namespace Customers.Domain.Entities
{
    public sealed class Customer : AggregateRoot
    {
        private const int MinimumAge = 18;

        public Gender? Gender { get;  private set; }
        public string? ProfileImage { get; private set; }
        public DateOnly Bday { get; private set; }
        public Height? Height { get; private set; } = null!;

        private readonly List<CustomerAddress> _customerAddresses = [];
        public IReadOnlyCollection<CustomerAddress> CustomerAddresses => _customerAddresses;

        private Customer() { }

        private Customer(Guid id, Height? height, DateOnly bday) 
        {
            EnsureValidAge(bday);

            Id = id;
            Height = height;
            Bday = bday;
            CreatedAt = UpdatedAt =  DateTime.UtcNow;
        }

        public static Customer Create(
            Guid id,
            Gender? gender,
            DateOnly birthDay,
            Height? height,
            string? profilePic)
        {
            var customer = new Customer(id, height, birthDay);
            customer.ProfileImage = profilePic;
            customer.Gender = gender;

            return customer;
        }


        public void AddAddress(CustomerAddress newAddress)
        {
            ArgumentNullException.ThrowIfNull(newAddress, nameof(newAddress));

            if (_customerAddresses.Count == 0)
                newAddress.SetAsPrimary();

            if (_customerAddresses.Any(a => a.Label == newAddress.Label.ToLowerInvariant()))
                throw new DuplicateException(nameof(CustomerAddress.Label), newAddress.Label);

            _customerAddresses.Add(newAddress);
        }

        public void UpdateGender(Gender gender)
        {
            if (Gender == gender)
                return;

            Gender = gender;
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