using UniMagContributions.Constraints;
using UniMagContributions.Dto;

namespace UniMagContributions.Services.Interface
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);

        string getFilePath(string fileName);

        bool DeleteImage(string imageFileName);

        /*Task PostFileAsync(IFormFile fileData, FileType fileType);

        Task PostMultiFileAsync(List<FileUpload> fileData);

        Task DownloadFileById(int fileName);*/
    }
}
