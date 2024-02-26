using UniMagContributions.Constraints;

namespace UniMagContributions.Dto
{
    public class FileUpload
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}
