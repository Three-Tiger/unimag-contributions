using System.ComponentModel.DataAnnotations;
using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.Contribution
{
	public class ContributionDto
	{
		public Guid ContributionId { get; set; }

		public string Title { get; set; }

		public DateTime SubmissionDate { get; set; }

		public EStatus Status { get; set; }

		public Guid UserId { get; set; }

		public Guid AnnualMagazineId { get; set; }
	}
}
