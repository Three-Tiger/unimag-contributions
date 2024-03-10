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
			var result = _fileService.SaveFile(fileDetailsDto.FileUpload.FileDetails, EFolder.ContributionFile);

            // if it is not right format
            if (result.Item1 == 0)
            {
                throw new InvalidException(result.Item2);
            }

            var fileDetails = new FileDetails()
			{
				FileName = fileDetailsDto.FileUpload.FileDetails.FileName,
				FilePath = result.Item2,
				FileType = fileDetailsDto.FileUpload.FileType,
				ContributionId = fileDetailsDto.ContributionId,
			};

			_fileDetailRepository.CreateFileDetail(fileDetails);

			return "Upload Success!";
		}

		public FileContentResult DownloadFileById(Guid id)
		{
			FileDetails fileDetails = _fileDetailRepository.GetFileDetailById(id);
			return _fileService.DownloadFileById(fileDetails, EFolder.ContributionFile);
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
					var result = _fileService.SaveFile(fileDetailsDto.FileUpload.FileDetails, EFolder.ContributionFile);

					var fileDetails = new FileDetails()
					{
						FileName = fileDetailsDto.FileUpload.FileDetails.FileName,
						FilePath = result.Item2,
						FileType = fileDetailsDto.FileUpload.FileType,
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
    }
}
