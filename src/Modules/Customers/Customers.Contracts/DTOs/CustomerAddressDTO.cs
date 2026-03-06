namespace Customers.Contracts.DTOs
{
    public record CustomerAddressDTO(
        Guid Id,
        string ContactNumber,
        string City,
        string Area,
        string? StreetName,
        string? StreetNumber,
        double Longitude,
        double Latitude);

}
