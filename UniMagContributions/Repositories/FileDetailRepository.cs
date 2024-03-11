using UniMagContributions.Models;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Repositories
{
	public class FileDetailRepository : IFileDetailRepository
	{
		private readonly ApplicationDbContext _context;

        public FileDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

		public void CreateFileDetail(FileDetails fileDetails)
		{
			try
			{
				_context.FileDetails.Add(fileDetails);
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw new Exception("Error creating file");
			}
		}

		public void DeleteFileDetail(FileDetails fileDetails)
		{
			throw new NotImplementedException();
		}

		public FileDetails GetFileDetailById(Guid id)
		{
			try
			{
				FileDetails file = _context.FileDetails.FirstOrDefault(x => x.FileId == id);
				return file;
			}
			catch (Exception)
			{
				throw new Exception("Error getting file");
			}
		}

		public List<FileDetails> GetFileDetailByContributionId(Guid contributionId)
		{
			try
			{
                List<FileDetails> file = _context.FileDetails.Where(x => x.ContributionId == contributionId).ToList();
                return file;
            }
            catch (Exception)
			{
                throw new Exception("Error getting file");
            }
		}


        public Role GetFileDetailByName(string name)
		{
			throw new NotImplementedException();
		}

		public void UpdateFileDetail(FileDetails fileDetails)
		{
			throw new NotImplementedException();
		}

		FileDetails IFileDetailRepository.GetFileDetailByName(string name)
		{
			throw new NotImplementedException();
		}
	}
}
