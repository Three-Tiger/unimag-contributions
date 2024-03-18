using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class FileDetailService : IFileDetailServive
    {
        private readonly IFileService _fileService;
        private readonly IFileDetailRepository _fileDetailRepository;

        public FileDetailService(IFileService uploadService, IFileDetailRepository fileDetailRepository)
        {
            _fileService = uploadService;
            _fileDetailRepository = fileDetailRepository;
        }

        public string AddFileDetail(CreateaFileDetailsDto fileDetailsDto)
        {
            var result = _fileService.SaveFile(fileDetailsDto.FileUpload, EFolder.ContributionFile);

            // if it is not right format
            if (result.Item1 == 0)
            {
                throw new InvalidException(result.Item2);
            }

            var fileDetails = new FileDetails()
            {
                FileName = fileDetailsDto.FileUpload.FileName,
                FilePath = result.Item2,
                FileType = GetFileType(fileDetailsDto.FileUpload.FileName),
                ContributionId = fileDetailsDto.ContributionId,
            };

            _fileDetailRepository.CreateFileDetail(fileDetails);

            return "Upload Success!";
        }

        public EFileType GetFileType(string fileName)
        {
            return Path.GetExtension(fileName) switch
            {
                ".pdf" => EFileType.PDF,
                ".docx" => EFileType.DOCX,
                _ => EFileType.PDF,
            };
        }

        public FileContentResult DownloadFileById(Guid id)
        {
            FileDetails fileDetails = _fileDetailRepository.GetFileDetailById(id);
            return _fileService.DownloadFileById(fileDetails);
        }

        public string DeleteFileDetail(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<FileDetailDto> GetAllFileDetail()
        {
            throw new NotImplementedException();
        }

        public FileDetailDto GetFileDetailById(Guid id)
        {
            throw new NotImplementedException();
        }

        public FileDetailDto UpdateFileDetail(Guid id, UpdateFileDetailsDto fileDetailsDto)
        {
            throw new NotImplementedException();
        }

        public string AddMultipleFileDetail(List<CreateaFileDetailsDto> FileDetailDto)
        {
            try
            {
                foreach (var fileDetailsDto in FileDetailDto)
                {
                    var result = _fileService.SaveFile(fileDetailsDto.FileUpload, EFolder.ContributionFile);

                    // if it is not right format
                    if (result.Item1 == 0)
                    {
                        throw new InvalidException(result.Item2);
                    }

                    var fileDetails = new FileDetails()
                    {
                        FileName = fileDetailsDto.FileUpload.FileName,
                        FilePath = result.Item2,
                        FileType = GetFileType(fileDetailsDto.FileUpload.FileName),
                        ContributionId = fileDetailsDto.ContributionId,
                    };

                    _fileDetailRepository.CreateFileDetail(fileDetails);
                }

                return "Upload Success!";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload files.", ex);
            }
        }

        public FileContentResult DownloadMultipleFile(Guid contributionId)
        {
            List<FileDetails> fileDetails = _fileDetailRepository.GetFileDetailByContributionId(contributionId);
            FileContentResult result = _fileService.DownloadMultipleFile(fileDetails, EFolder.ContributionFile);
            return result;
        }

        public FileContentResult DownloadMultipleFileByListContributionId(List<Guid> lstContributionId)
        {
            List<FileContentResult> lstResult = new List<FileContentResult>();

            foreach (Guid contributionId in lstContributionId)
            {
                List<FileDetails> fileDetails = _fileDetailRepository.GetFileDetailByContributionId(contributionId);
                FileContentResult fileResult = _fileService.DownloadMultipleFile(fileDetails, EFolder.ContributionFile);
                lstResult.Add(fileResult);
            }

            FileContentResult result = _fileService.DownloadMultipleFile(lstResult, EFolder.ContributionFile);

            return result;
        }

        public string DeleteFileByContributionId(Guid contributionId)
        {
            List<FileDetails> fileDetails = _fileDetailRepository.GetFileDetailByContributionId(contributionId);
            foreach (var file in fileDetails)
            {
                _fileService.DeleteFile(file.FilePath);
                _fileDetailRepository.DeleteFileDetail(file);
            }
            return "Delete successful";
        }
    }
}
