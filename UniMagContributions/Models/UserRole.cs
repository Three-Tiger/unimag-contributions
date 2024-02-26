using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMagContributions.Models
{
    public class UserRole
    {
        [Key]
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
