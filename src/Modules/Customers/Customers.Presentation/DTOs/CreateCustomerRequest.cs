namespace Customers.Presentation.DTOs
{
    public sealed record CreateCustomerRequest(
        string Fname,
        string Lname,
        string Email,
        string PhoneNumber,
        DateOnly BirthDay);
}
