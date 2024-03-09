using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.FileDetails
{
	public class CreateaFileDetailsDto
	{
		public Guid ContributionId { get; set; }

		public FileUploadDto FileUpload { get; set; }
	}
}
