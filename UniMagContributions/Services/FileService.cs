using UniMagContributions.Constraints;
using UniMagContributions.Dto;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;
        private readonly IFileDetailRepository _fileDetailRepository;

        public FileService(IWebHostEnvironment env, IFileDetailRepository fileDetailRepository)
        {
            environment = env;
            _fileDetailRepository = fileDetailRepository;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "ProfilePicture");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    throw new ArgumentException(msg);
                }

                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();

                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured: " + ex.Message);
            }
        }

        public string getFilePath(string fileName)
        {
            string imagePath = "ProfilePicture/" + fileName;
            return imagePath;
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "ProfilePicture\\", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public string PostFile(FileUploadDto fileUploadDto)
        {
            try
            {
                var fileDetails = new FileDetails()
                {
                    FileName = fileUploadDto.FileDetails.FileName,
                    FileType = fileUploadDto.FileType,
                };

                using (var stream = new MemoryStream())
                {
					fileUploadDto.FileDetails.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                _fileDetailRepository.CreateFileDetail(fileDetails);

                return "Upload File Successful!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string PostMultiFile(List<FileUploadDto> fileData)
        {
            try
            {
                foreach (FileUploadDto file in fileData)
                {
                    var fileDetails = new FileDetails()
                    {
                        FileName = file.FileDetails.FileName,
                        FileType = file.FileType,
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

					_fileDetailRepository.CreateFileDetail(fileDetails);
				}
                return "Upload Multi File Successful!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DownloadFileById(Guid Id)
        {
            try
            {
                var file = _fileDetailRepository.GetFileDetailById(Id);

                var content = new System.IO.MemoryStream(file.FileData);
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "FileDownloaded", file.FileName);

                CopyStream(content, path);

                return "Download Successful!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
