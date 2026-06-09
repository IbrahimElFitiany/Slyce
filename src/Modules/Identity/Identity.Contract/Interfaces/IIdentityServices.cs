namespace Identity.Contract.Interfaces
{
    public interface IIdentityServices
    {
        Task<Guid> CreateRestaurantOwner(string fname,string lname, string email, CancellationToken cancellationToken);
    }
}
