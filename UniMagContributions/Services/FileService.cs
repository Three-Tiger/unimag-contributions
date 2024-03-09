using UniMagContributions.Constraints;
using UniMagContributions.Dto.FileDetails;
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

        public byte[] PostFile(FileUploadDto fileUploadDto)
        {
            try
            {
                byte[] bytes = null;

                using (var stream = new MemoryStream())
                {
					fileUploadDto.FileDetails.CopyTo(stream);
                    bytes = stream.ToArray();
                }

                return bytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DownloadFileById(FileDetails fileDetails)
        {
			/*try
            {
                var content = new System.IO.MemoryStream(fileDetails.FileData);

                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "FileDownloaded", fileDetails.FileName);

                CopyStream(content, path);

                return "Download Successful!";
            }
            catch (Exception)
            {
                throw;
            }*/

			try
			{
				var content = new System.IO.MemoryStream(fileDetails.FileData);

				// Specify the directory where you want to save the file
				string downloadDirectory = @"C:\Downloads"; // Change this to your desired directory

				// Ensure the download directory exists; if not, create it
				if (!Directory.Exists(downloadDirectory))
				{
					Directory.CreateDirectory(downloadDirectory);
				}

				// Combine the download directory path with the file name
				var path = Path.Combine(downloadDirectory, fileDetails.FileName);

				// Copy the file stream to the specified path
				CopyStream(content, path);

				return "Download Successful!";
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				throw new Exception("Failed to download file.", ex);
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
