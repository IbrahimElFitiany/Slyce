using Restaurants.Domain.ValueObjects;
using Shared.Domain.Common;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Restaurants.Domain.Entities
{
    public sealed class RestaurantBranch : AggregateRoot
    {
        public Guid RestaurantId { get; private init; }
        public string? Name { get; private set; }
        public Address Address { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;

        private readonly List<DailySchedule> _schedule = [];
        public IReadOnlyCollection<DailySchedule> Schedule => _schedule;
        public bool IsActive { get; private set; } = false;

        private RestaurantBranch() {}

        public RestaurantBranch(
            Guid restaurantId,
            string? name,
            Address address,
            PhoneNumber number)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(restaurantId, Guid.Empty);
            ArgumentNullException.ThrowIfNull(address, nameof(address));
            ArgumentNullException.ThrowIfNull(number, nameof(number));

            Id = Guid.NewGuid();
            RestaurantId = restaurantId;
            Name = name;
            Address = address;
            PhoneNumber = number;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void InitializeSchedule(IReadOnlyList<DailySchedule> newSchedule) {

            ArgumentNullException.ThrowIfNull(newSchedule);

            if (newSchedule.Count == 0) 
                throw new InvalidDomainOperationException("Schedule must contain at least one day.");

            if (_schedule.Count > 0) 
                throw new DuplicateException("The branch already has a schedule.");
            
            if (newSchedule.GroupBy(d => d.Day).Any(g => g.Count() > 1)) 
                throw new DuplicateException("A day cannot appear more than once.");
            
            _schedule.AddRange(newSchedule);

            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidDomainOperationException("Already Activated");

            if (Schedule.Count == 0)
                throw new InvalidDomainOperationException("Branch must have a schedule before it can be activated.");

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}