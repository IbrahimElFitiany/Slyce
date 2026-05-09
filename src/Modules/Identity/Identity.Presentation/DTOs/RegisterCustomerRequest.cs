namespace Identity.Presentation.DTOs
{
    public sealed record RegisterCustomerRequest(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Password,
        DateOnly BirthDay);
}