using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.FileDetails;

namespace UniMagContributions.Services.Interface
{
	public interface IFileDetailServive
	{
		string AddFileDetail(CreateaFileDetailsDto FileDetailDto);
		string AddMultipleFileDetail(List<CreateaFileDetailsDto> FileDetailDto);
		FileDetailDto UpdateFileDetail(Guid id, UpdateFileDetailsDto FileDetailDto);
		string DeleteFileDetail(Guid id);
		FileDetailDto GetFileDetailById(Guid id);
		List<FileDetailDto> GetAllFileDetail();
        FileContentResult DownloadFileById(Guid id);
        FileContentResult DownloadMultipleFile(Guid contributionId);
		string DeleteFileByContributionId(Guid contributionId);
    }
}
