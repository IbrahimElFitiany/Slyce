namespace Identity.Contract.Interfaces
{
    public interface IIdentityServices
    {
        Task CreateRestaurantOwner(string fname,string lname, string email, CancellationToken cancellationToken);
    }
}
