using UniMagContributions.Constraints;

namespace UniMagContributions.Dto
{
    public class FileUploadDto
    {
        public IFormFile FileDetails { get; set; }
        public EFileType FileType { get; set; }
    }
}
