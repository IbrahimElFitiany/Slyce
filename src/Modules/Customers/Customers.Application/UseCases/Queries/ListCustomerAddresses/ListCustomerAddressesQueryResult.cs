namespace Customers.Application.UseCases.Queries.ListCustomerAddresses
{
    public sealed record ListCustomerAddressesQueryResult(IReadOnlyCollection<CustomerAddressResult> CustomerAddresses);

    public sealed record CustomerAddressResult(
        Guid AddressId,
        string Label,
        bool IsPrimary,
        string PhoneNumber,
        string City,
        string Area, 
        string? StreetName,
        string? StreetNumber,
        double Latitude,
        double Longitude);
}