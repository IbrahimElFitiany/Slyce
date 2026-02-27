using Restaurants.Domain.Enums;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Restaurants.Domain.Entities
{
    public sealed class RestaurantApplication
    {
        public Guid Id { get; private init; }
        public string BrandName { get; private set; } = null!;
        public string OwnerFirstName { get; private set; } = null!;
        public string OwnerLastName { get; private set; } = null!;
        public Email CompanyEmail { get; private set; } = null!;
        public PhoneNumber OwnerMobileNumber { get; private set; } = null!;
        public PhoneNumber CompanyMobileNumber { get; private set; } = null!;
        public RestaurantType RestaurantType { get; private set; }
        public int BranchCount { get; private set; }
        public Address MainBranchLocation { get; private set; } = null!;
        public string? Description { get; private set; }
        public ApplicationStatus Status { get; private set; } = ApplicationStatus.Pending;
        public string? RejectionReason { get; private set; }

        public DateTime SubmittedAt { get; private init; }
        public DateTime? ReviewedAt { get; private set; }
        public Guid? ReviewedBy { get; private set; }

        private RestaurantApplication () { }
        public RestaurantApplication(
            string brandName,
            string ownerFirstName,
            string ownerLastName,
            Email companyEmail,
            PhoneNumber ownerMobileNumber,
            PhoneNumber companyMobileNumber,
            RestaurantType restaurantType,
            Address mainBranchLocation,
            int branchCount = 1,
            string? description = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(brandName);
            ArgumentException.ThrowIfNullOrWhiteSpace(ownerFirstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(ownerLastName);
            ArgumentOutOfRangeException.ThrowIfLessThan(branchCount, 1);

            Id = Guid.NewGuid();
            BrandName = brandName;
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            CompanyEmail = companyEmail;
            OwnerMobileNumber = ownerMobileNumber;
            CompanyMobileNumber = companyMobileNumber;
            RestaurantType = restaurantType;
            BranchCount = branchCount;
            MainBranchLocation = mainBranchLocation;
            Description = description;

            SubmittedAt = DateTime.UtcNow;
        }

        public void Approve(Guid adminId)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(adminId,Guid.Empty);

            if (Status != ApplicationStatus.Pending)
                throw new InvalidDomainOperationException("Only pending applications can be approved");

            Status = ApplicationStatus.Approved;
            ReviewedAt = DateTime.UtcNow;
            ReviewedBy = adminId;
            RejectionReason = null;
        }
        public void Reject(Guid adminId, string reason)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(adminId, Guid.Empty);
            ArgumentException.ThrowIfNullOrWhiteSpace(reason);

            if (Status != ApplicationStatus.Pending)
                throw new InvalidDomainOperationException("Only pending applications can be rejected");

            Status = ApplicationStatus.Rejected;
            ReviewedAt = DateTime.UtcNow;
            ReviewedBy = adminId;
            RejectionReason = reason;
        }
    }

}
