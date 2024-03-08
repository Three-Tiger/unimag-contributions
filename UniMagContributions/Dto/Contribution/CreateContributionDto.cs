using System.ComponentModel.DataAnnotations;
using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.Contribution
{
	public class CreateContributionDto
	{
		[Required(ErrorMessage = "The Title can not empty!")]
		public string Title { get; set; }

		[Required(ErrorMessage = "The Submission Date can not empty!")]
		public DateTime SubmissionDate { get; set; }

		[Required(ErrorMessage = "The Status can not empty!")]
		public string Status { get; set; }

		public Guid UserId { get; set; }

		public Guid AnnualMagazineId { get; set; }
	}
}
