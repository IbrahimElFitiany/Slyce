using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SlyceAPI.Models
{
    public enum Gender
    {
        F,
        M
    }
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Fname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lname { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ProfileImage { get; set; }

        [Required]
        public DateTime Bday { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
    }
}
