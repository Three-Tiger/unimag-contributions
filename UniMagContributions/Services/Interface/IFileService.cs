using UniMagContributions.Constraints;
using UniMagContributions.Dto;

namespace UniMagContributions.Services.Interface
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);

        string getFilePath(string fileName);

        bool DeleteImage(string imageFileName);

        string PostFile(FileUploadDto fileUploadDto);

        string PostMultiFile(List<FileUploadDto> fileData);

        string DownloadFileById(Guid id);
    }
}
