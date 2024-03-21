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
		FileContentResult ReadFileById(Guid id);
        FileContentResult DownloadMultipleFile(Guid contributionId);
		FileContentResult DownloadMultipleFileByListContributionId(List<Guid> lstContributionId);
        string DeleteFileByContributionId(Guid contributionId);

    }
}
