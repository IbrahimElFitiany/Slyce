using Identity.Domain.DomainEvents;
using Identity.Domain.Enums;
using Shared.Domain.Common;
using Shared.Domain.ValueObjects;

namespace Identity.Domain.Aggregates
{
    public sealed class User : AggregateRoot
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public string? PasswordHash { get; private set; }
        public UserType UserType { get; private set; }
        
        private User() { }
        private User(
            string firstName,
            string lastName,
            Email email,
            UserType userType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserType = userType;
            CreatedAt = UpdatedAt = DateTime.UtcNow;
        }


        public static User CreateCustomer(
            string firstName,
            string lastName,
            Email email,
            string passwordHash)
        {
            var customer = new User(firstName, lastName, email, UserType.Customer);
            customer.PasswordHash = passwordHash;

            customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id, customer.Email.Value));

            return customer;
        }

    }

}