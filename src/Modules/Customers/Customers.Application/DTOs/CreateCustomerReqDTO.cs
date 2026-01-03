namespace Customers.Application.DTOs
{
    public record CreateCustomerReqDTO(
        string fname,
        string lname,
        string email,
        string phoneNumber,
        string password,
        DateOnly birthDay
    );
}
