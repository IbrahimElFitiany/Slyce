using Restaurants.Domain.Enums;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Domain.Entities
{
    public sealed class RestaurantApplication
    {
        public Guid Id { get; private init; }
        public string BrandName { get; private set; }
        public string OwnerFirstName { get; private set; }
        public string OwnerLastName { get; private set; }
        public string CompanyEmail { get; private set; }
        public string MobileNumber { get; private set; }
        public RestaurantType RestaurantType { get; private set; }
        public int Branches { get; private set; }
        public string? Description { get; private set; }
        public ApplicationStatus Status { get; private set; } = ApplicationStatus.Pending;
        public string? RejectionReason { get; private set; }

        public DateTime SubmittedAt { get; private init; } = DateTime.UtcNow;
        public DateTime? ReviewedAt { get; private set; }
        public Guid? ReviewedBy { get; private set; }

        public RestaurantApplication(
            string brandName,
            string ownerFirstName,
            string ownerLastName,
            string companyEmail,
            string mobileNumber,
            RestaurantType restaurantType,
            int branches = 1,
            string? description = null)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                throw new CantBeEmptyException(nameof(brandName));

            if (string.IsNullOrWhiteSpace(ownerFirstName))
                throw new CantBeEmptyException(nameof(ownerFirstName));

            if (string.IsNullOrWhiteSpace(ownerLastName))
                throw new CantBeEmptyException(nameof(ownerLastName));

            if (string.IsNullOrWhiteSpace(companyEmail))
                throw new CantBeEmptyException(nameof(companyEmail));

            if (string.IsNullOrWhiteSpace(mobileNumber))
                throw new CantBeEmptyException(nameof(mobileNumber));

            if (branches < 1)
                throw new InvalidBranchNumberException(branches);

            Id = Guid.NewGuid();
            BrandName = brandName;
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            CompanyEmail = companyEmail;
            MobileNumber = mobileNumber;
            RestaurantType = restaurantType;
            Branches = branches;
            Description = description;
        }

        public void Approve(Guid adminId)
        {
            Status = ApplicationStatus.Approved;
            ReviewedAt = DateTime.UtcNow;
            ReviewedBy = adminId;
            RejectionReason = null;
        }

        public void Reject(Guid adminId, string reason)
        {
            Status = ApplicationStatus.Rejected;
            ReviewedAt = DateTime.UtcNow;
            ReviewedBy = adminId;
            RejectionReason = reason;
        }
    }

}
