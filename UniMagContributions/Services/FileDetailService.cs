using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
	public class FileDetailService : IFileDetailServive
	{
		private readonly IFileService _uploadService;
		private readonly IFileDetailRepository _fileDetailRepository;

		public FileDetailService(IFileService uploadService, IFileDetailRepository fileDetailRepository)
		{
			_uploadService = uploadService;
			_fileDetailRepository = fileDetailRepository;
		}

		public string AddFileDetail(CreateaFileDetailsDto fileDetailsDto)
		{
			byte[] upload = _uploadService.PostFile(fileDetailsDto.FileUpload);

			var fileDetails = new FileDetails()
			{
				FileName = fileDetailsDto.FileUpload.FileDetails.FileName,
				FileData = upload,
				FileType = fileDetailsDto.FileUpload.FileType,
				ContributionId = fileDetailsDto.ContributionId,
			};

			_fileDetailRepository.CreateFileDetail(fileDetails);

			return "Upload Success!";
		}

		public string DownloadFileById(Guid id)
		{
			FileDetails fileDetails = _fileDetailRepository.GetFileDetailById(id);

			return _uploadService.DownloadFileById(fileDetails); ;
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
					byte[] upload = _uploadService.PostFile(fileDetailsDto.FileUpload);

					var fileDetails = new FileDetails()
					{
						FileName = fileDetailsDto.FileUpload.FileDetails.FileName,
						FileData = upload,
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
	}
}
