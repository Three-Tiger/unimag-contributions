using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMagContributions.Models
{
	public class AnnualMagazine
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid AnnualMagazineId { get; set; }
		
		[StringLength(50)]
		public string AcademicYear { get; set; }
		
		[StringLength(200)]
		public string Title { get; set; }

		public string Description { get; set; }
	
		public DateTime ClosureDate { get; set; }

		public DateTime FinalClosureDate { get; set; }

		public virtual ICollection<Contribution>? Contributions { get; set; }
	}
}
