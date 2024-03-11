using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Dto.User;

namespace UniMagContributions.Dto.Contribution
{
	public class ContributionDto
	{
		public Guid ContributionId { get; set; }

		public string Title { get; set; }

		public DateTime SubmissionDate { get; set; }

		public string Status { get; set; }

		public UserDto? User { get; set; }

		public AnnualMagazineDto? AnnualMagazine { get; set; }

		public ICollection<FileDetailDto>? FileDetails { get; set; }
	}
}
