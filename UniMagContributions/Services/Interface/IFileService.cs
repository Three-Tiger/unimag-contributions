using UniMagContributions.Constraints;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Models;

namespace UniMagContributions.Services.Interface
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);

        string getFilePath(string fileName);

        bool DeleteImage(string imageFileName);

        byte[] PostFile(FileUploadDto fileUploadDto);

        string DownloadFileById(FileDetails fileDetails);

		/*string DownloadFileByContributionId(Guid ContributionId);*/
	}
}
