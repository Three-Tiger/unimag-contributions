using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.ImageDetail
{
    public class CreateImageDetailDto
    {
        public Guid ContributionId { get; set; }

        [Required(ErrorMessage = "The image name can not empty!")]
        public IFormFile FileUpload { get; set; }
    }
}
