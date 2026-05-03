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


        public static User Create(
            string firstName,
            string lastName,
            Email email,
            string passwordHash,
            UserType userType)
        {
            var user = new User(firstName, lastName, email, userType);
            user.PasswordHash = passwordHash;

            return user;
        }
        public static User CreateWithoutPassword(
            string firstName,
            string lastName,
            Email email,
            UserType userType)
        {
            return new User(firstName, lastName, email, userType);
        }

    }

}