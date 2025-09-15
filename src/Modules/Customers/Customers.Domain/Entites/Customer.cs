namespace Customers.Domain.Entites
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
        public string? Phone { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.Date;

        public Customer(string Fname , string Lname , Gender gender, DateOnly Bday) {

            Id = Guid.NewGuid();
            this.Fname = Fname;
            this.Lname = Lname;
            this.Gender = gender;
            this.Bday = Bday;
        }

        public void UpdateEmail(string Email)
        {
            this.Email = Email;
        }
    }
}
