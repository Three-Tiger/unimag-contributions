using System.ComponentModel.DataAnnotations;
using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.FileDetails
{
    public class FileUploadDto
    {
        [Required(ErrorMessage = "The FileDetails can not empty!")]
        public IFormFile FileDetails { get; set; }

        public EFileType? FileType { get; set; }
    }
}
