using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniMagContributions.Constraints;

namespace UniMagContributions.Models
{
	public class ImageDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ImageId { get; set; }
		
		[StringLength(255)]
		public string ImageName {  get; set; }

		public string ImagePath { get; set; }

		public Guid ContributionId { get; set; }

		[ForeignKey("ContributionId")]
		public virtual Contribution? Contribution { get; set; }
	}
}
