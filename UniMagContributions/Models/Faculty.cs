using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMagContributions.Models
{
	public class Faculty
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid FacultyId { get; set; }

		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(255)]
		public string Description { get; set; }

		public virtual ICollection<User>? Users { get; set; }
	}
}
