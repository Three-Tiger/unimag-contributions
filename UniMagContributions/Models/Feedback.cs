using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMagContributions.Models
{
	public class Feedback
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid FeedBackId { get; set; }

		public string Content { get; set; }

		public DateTime FeedbackDate { get; set; }

		public Guid UserId { get; set; }

		public Guid ContributionId { get; set; }

		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		[ForeignKey("ContributionId")]
		public virtual Contribution? Contribution { get; set; }
	}
}
