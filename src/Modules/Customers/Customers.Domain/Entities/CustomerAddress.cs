using Shared.Domain.ValueObjects;

namespace Customers.Domain.Entities
{
    public sealed class CustomerAddress 
    {
        public Guid Id { get; private init; }
        public string Label { get; private set; } = null!;
        public PhoneNumber ContactNumber { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public bool IsPrimary { get; private set; }

        private CustomerAddress() { }   

        public CustomerAddress(string label, PhoneNumber contactNumber, Address address)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(label, nameof(label));
            ArgumentNullException.ThrowIfNull(contactNumber, nameof(contactNumber));
            ArgumentNullException.ThrowIfNull(address, nameof(address));

            Id = Guid.NewGuid();
            Label = label.Trim().ToLowerInvariant();
            ContactNumber = contactNumber;
            Address = address;
        }

        public void SetAsPrimary() => IsPrimary = true;

    }
}
