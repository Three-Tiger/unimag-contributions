using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
    public interface IImageDetailRepository
    {
        void CreateImageDetail(ImageDetails imageDetails);
        ImageDetails GetImageDetailByName(string name);
        ImageDetails GetImageDetailById(Guid id);
        List<ImageDetails> GetImageDetailByContributionId(Guid contributionId);
        void UpdateImageDetail(ImageDetails imageDetails);
        void DeleteImageDetail(ImageDetails imageDetails);
    }
}
