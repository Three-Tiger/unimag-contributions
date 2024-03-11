using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IFileDetailRepository
	{
		void CreateFileDetail(FileDetails fileDetails);
		FileDetails GetFileDetailByName(string name);
		FileDetails GetFileDetailById(Guid id);
		List<FileDetails> GetFileDetailByContributionId(Guid contributionId);
		void UpdateFileDetail(FileDetails fileDetails);
		void DeleteFileDetail(FileDetails fileDetails);
	}
}
