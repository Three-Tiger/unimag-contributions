using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.IO.Compression;
using UniMagContributions.Constraints;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment env)
        {
            environment = env;
        }

        public Tuple<int, string> SaveFile(IFormFile file, EFolder folderName)
        {
            try
            {
                string wwwPath = this.environment.WebRootPath;
                string path = Path.Combine(wwwPath, folderName.ToString());

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                string ext = Path.GetExtension(file.FileName);
                string[] allowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".pdf", ".docx", ".zip", ".rar" };

                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    throw new InvalidException(msg);
                }

                string uniqueString = Guid.NewGuid().ToString();
                string newFileName = uniqueString + ext;
                string fileWithPath = Path.Combine(path, newFileName);

                var stream = new FileStream(fileWithPath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();

                return new Tuple<int, string>(1, Path.Combine(folderName.ToString(), newFileName));
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured: " + ex.Message);
            }
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, filePath);

                if (File.Exists(path))
                {
                    File.Delete(path);
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
                byte[]? bytes = null;

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

        public FileContentResult GetFile(string filePath)
        {
            try
            {
                string wwwPath = this.environment.WebRootPath;
                var file = Path.Combine(wwwPath, filePath);

                if (!File.Exists(file))
                {
                    throw new NotFoundException("File not found");
                }

                byte[] fileBytes = File.ReadAllBytes(file);
                return new FileContentResult(fileBytes, "image/jpeg");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FileContentResult DownloadFileById(FileDetails fileDetails)
        {
            try
            {
                string wwwPath = this.environment.WebRootPath;
                var file = Path.Combine(wwwPath, fileDetails.FilePath);

                // Read the file content into a memory stream
                using (var fileStream = new FileStream(file, FileMode.Open))
                {
                    var memoryStream = new MemoryStream();
                    fileStream.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    // Return the file content as a FileContentResult
                    return new FileContentResult(memoryStream.ToArray(), "application/octet-stream")
                    {
                        FileDownloadName = fileDetails.FileName
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FileContentResult DownloadFileById(ImageDetails imageDetails)
        {
            try
            {
                string wwwPath = this.environment.WebRootPath;
                var file = Path.Combine(wwwPath, imageDetails.ImagePath);

                // Read the file content into a memory stream
                using (var fileStream = new FileStream(file, FileMode.Open))
                {
                    var memoryStream = new MemoryStream();
                    fileStream.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    // Return the file content as a FileContentResult
                    return new FileContentResult(memoryStream.ToArray(), "application/octet-stream")
                    {
                        FileDownloadName = imageDetails.ImageName
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FileContentResult DownloadMultipleFile(List<FileDetails> fileDetails, EFolder folderName)
        {
            try
            {
                string wwwPath = this.environment.WebRootPath;
                var files = fileDetails.Select(x => Path.Combine(wwwPath, x.FilePath)).ToList();

                using (var memoryStream = new MemoryStream())
                {
                    using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var file in files)
                        {
                            var fileInfo = new FileInfo(file);
                            if (!fileInfo.Exists)
                            {
                                throw new FileNotFoundException($"File not found: {fileInfo.FullName}");
                            }

                            var entry = zipArchive.CreateEntry(fileInfo.Name);

                            using (var entryStream = entry.Open())
                            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    string uniqueString = Guid.NewGuid().ToString();
                    var zipFileName = $"{folderName}-{uniqueString}.zip";
                    return new FileContentResult(memoryStream.ToArray(), "application/zip")
                    {
                        FileDownloadName = zipFileName
                    };
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception($"Failed to download files. {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while downloading files. {ex.Message}");
            }
        }

        public FileContentResult DownloadMultipleFile(List<ImageDetails> imageDetails, EFolder folderName)
        {
            try
            {
                string wwwPath = this.environment.WebRootPath;
                var files = imageDetails.Select(x => Path.Combine(wwwPath, x.ImagePath)).ToList();

                using (var memoryStream = new MemoryStream())
                {
                    using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var file in files)
                        {
                            var fileInfo = new FileInfo(file);
                            if (!fileInfo.Exists)
                            {
                                throw new FileNotFoundException($"File not found: {fileInfo.FullName}");
                            }

                            var entry = zipArchive.CreateEntry(fileInfo.Name);

                            using (var entryStream = entry.Open())
                            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    string uniqueString = Guid.NewGuid().ToString();
                    var zipFileName = $"{folderName}-{uniqueString}.zip";
                    return new FileContentResult(memoryStream.ToArray(), "application/zip")
                    {
                        FileDownloadName = zipFileName
                    };
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception($"Failed to download files. {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while downloading files. {ex.Message}");
            }
        }

        public FileContentResult DownloadMultipleFile(List<FileContentResult> fileContentResults, EFolder folderName)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var file in fileContentResults)
                        {
                            var entry = zipArchive.CreateEntry(file.FileDownloadName);

                            using (var entryStream = entry.Open())
                            using (var fileStream = new MemoryStream(file.FileContents))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    string uniqueString = Guid.NewGuid().ToString();
                    var zipFileName = $"{folderName}-{uniqueString}.zip";
                    return new FileContentResult(memoryStream.ToArray(), "application/zip")
                    {
                        FileDownloadName = zipFileName
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while downloading files. {ex.Message}");
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
