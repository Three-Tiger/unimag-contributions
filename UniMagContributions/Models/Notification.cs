using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniMagContributions.Models
{
	public class Notification
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid NotificationId { get; set; }

		public string Content { get; set; }

		public DateTime NotificationDate { get; set; }

		public Guid UserId { get; set; }

		public Guid ContributionId { get; set; }

		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		[ForeignKey("ContributionId")]
		public virtual Contribution? Contribution { get; set; }
	}
}
