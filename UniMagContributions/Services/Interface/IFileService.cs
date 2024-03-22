using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Models;

namespace UniMagContributions.Services.Interface
{
    public interface IFileService
    {
        Tuple<int, string> SaveFile(IFormFile imageFile, EFolder folderName);

        bool DeleteFile(string filePath);

        byte[] PostFile(FileUploadDto fileUploadDto);

        FileContentResult GetFile(string filePath);

        FileContentResult DownloadFileById(FileDetails fileDetails);

        FileContentResult ReadFileById(FileDetails fileDetails);

        FileContentResult DownloadFileById(ImageDetails imageDetails);

        FileContentResult DownloadMultipleFile(List<FileDetails> fileDetails, EFolder folderName);

        FileContentResult DownloadMultipleFile(List<ImageDetails> fileDetails, EFolder folderName);

        FileContentResult DownloadMultipleFile(List<FileContentResult> fileContentResults, EFolder folderName);
    }
}
