namespace Customers.Domain.Entities
{
    public enum Gender
    {
        F,
        M
    }
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Fname { get; private set; }
        public string Lname { get; private set; }
        public Gender Gender { get;  private set; }
        public string? Email { get; private set; }
        public string? ProfileImage { get; private set; }
        public DateOnly Bday { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.Date;

        public Customer(Guid id , string fname , string lname, Gender gender, string email, DateOnly bday) {

            Id = id;
            Fname = fname;
            Lname = lname;
            Gender = gender;
            Email = email;
            Bday = bday;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }
    }
}
