namespace UniMagContributions.Dto.ImageDetail
{
    public class ImageDetailDto
    {
        public Guid ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public Guid ContributionId { get; set; }
    }
}
