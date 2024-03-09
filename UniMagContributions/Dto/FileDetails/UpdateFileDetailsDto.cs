using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.FileDetails
{
	public class UpdateFileDetailsDto
	{

		public Guid FileId { get; set; }

		public string FileName { get; set; }

		public Guid ContributionId { get; set; }

		public FileUploadDto FileUpload { get; set; }
	}
}
