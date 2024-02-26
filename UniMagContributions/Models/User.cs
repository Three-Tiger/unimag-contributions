using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniMagContributions.Constraints;

namespace UniMagContributions.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string? ProfilePicture { get; set; }

        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
