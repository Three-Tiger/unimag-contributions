using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Dto.Feedback;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Dto.ImageDetail;
using UniMagContributions.Dto.User;

namespace UniMagContributions.Dto.Contribution
{
	public class ContributionDto
	{
		public Guid ContributionId { get; set; }

		public string Title { get; set; }

		public DateTime SubmissionDate { get; set; }

		public string Status { get; set; }

        public bool IsPublished { get; set; }

        public UserDto? User { get; set; }

		public AnnualMagazineDto? AnnualMagazine { get; set; }

		public ICollection<FileDetailDto>? FileDetails { get; set; }

		public ICollection<ImageDetailDto>? ImageDetails { get; set; }

        public ICollection<FeedbackDto>? Feedbacks { get; set; }
    }
}
