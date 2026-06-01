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
        public Height? Height { get; private set; }
        public Weight? Weight { get; private set; }
        public ActivityRate ActivityRate { get; private set; }

        private readonly List<CustomerAddress> _customerAddresses = [];
        public IReadOnlyCollection<CustomerAddress> CustomerAddresses => _customerAddresses;

        private readonly List<Guid> _allergens = [];
        public IReadOnlyCollection<Guid> Allergens => _allergens;

        private readonly List<Guid> _dietPreferences = [];
        public IReadOnlyCollection<Guid> DietPreferences => _dietPreferences;

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

            if (_customerAddresses.Any(a => a.Label.Equals(newAddress.Label, StringComparison.InvariantCultureIgnoreCase)))
                throw new DuplicateException(nameof(CustomerAddress.Label), newAddress.Label);

            _customerAddresses.Add(newAddress);
        }

        public void RemoveAddress(Guid addressId)
        {
            //TODO: throw Domain specefic exception 
            var address = _customerAddresses.Find(a => a.Id == addressId)
                ?? throw new InvalidDomainOperationException("Address not found.");

            _customerAddresses.Remove(address);
        }

        public void UpdateGender(Gender gender)
        {
            if (Gender == gender)
                return;

            Gender = gender;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateWeight(Weight weight)
        {
            if (Weight == weight) 
                return;

            Weight = weight;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateHeight(Height height)
        {
            if (Height == height)
                return;

            Height = height;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateActivityRate(ActivityRate activityRate)
        {
            if (ActivityRate == activityRate)
                return;

            ActivityRate = activityRate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAllergens(IEnumerable<Guid> allergenIds)
        {
            ArgumentNullException.ThrowIfNull(allergenIds);
            _allergens.Clear();
            _allergens.AddRange(allergenIds.Distinct());
        }

        public void UpdateDietPreferences(IEnumerable<Guid> dietPreferenceIds)
        {
            ArgumentNullException.ThrowIfNull(dietPreferenceIds);
            _dietPreferences.Clear();
            _dietPreferences.AddRange(dietPreferenceIds.Distinct());
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