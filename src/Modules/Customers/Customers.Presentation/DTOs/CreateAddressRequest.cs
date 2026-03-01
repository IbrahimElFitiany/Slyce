namespace Customers.Presentation.DTOs
{
    public sealed record CreateAddressRequest(
        string Label,
        string StreetName,
        string StreetNumber,
        string Area,
        string City,
        double Latitude,
        double Longitude,
        string ContactNumber);
}
