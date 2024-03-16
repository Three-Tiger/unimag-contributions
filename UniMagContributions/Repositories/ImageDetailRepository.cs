using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
    public class ImageDetailRepository : IImageDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateImageDetail(ImageDetails imageDetails)
        {
            try
            {
                _context.ImageDetails.Add(imageDetails);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error creating file");
            }
        }

        public void DeleteImageDetail(ImageDetails imageDetails)
        {
            try
            {
                _context.ImageDetails.Remove(imageDetails);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error deleting file");
            }
        }

        public List<ImageDetails> GetImageDetailByContributionId(Guid contributionId)
        {
            try
            {
                List<ImageDetails> imageDetails = _context.ImageDetails.Where(x => x.ContributionId == contributionId).ToList();
                return imageDetails;
            }
            catch (Exception)
            {
                throw new Exception("Error getting file");
            }
        }

        public ImageDetails GetImageDetailById(Guid id)
        {
            try
            {
                ImageDetails image = _context.ImageDetails.FirstOrDefault(x => x.ImageId == id);
                return image;
            }
            catch (Exception)
            {
                throw new Exception("Error getting file");
            }
        }

        public ImageDetails GetImageDetailByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateImageDetail(ImageDetails imageDetails)
        {
            throw new NotImplementedException();
        }
    }
}
