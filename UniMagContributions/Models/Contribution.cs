using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniMagContributions.Constraints;

namespace UniMagContributions.Models
{
	public class Contribution
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid ContributionId { get; set; }

		[StringLength(100)]
		public string Title { get; set; }

		public DateTime SubmissionDate { get; set; }

		public EStatus Status { get; set; }

		public bool IsPublished { get; set; } = false;

		public Guid UserId { get; set; }

		public Guid AnnualMagazineId { get; set; }
		
		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		[ForeignKey("AnnualMagazineId")]
		public virtual AnnualMagazine? AnnualMagazine { get; set; }

		public virtual ICollection<Feedback>? Feedbacks { get; set; }

		public virtual ICollection<FileDetails>? FileDetails { get; set; }

		public virtual ICollection<ImageDetails>? ImageDetails { get; set; }
	}
}
