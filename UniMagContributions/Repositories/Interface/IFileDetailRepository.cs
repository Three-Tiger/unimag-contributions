using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IFileDetailRepository
	{
		void CreateFileDetail(FileDetails fileDetails);
		/*FileDetails GetFileDetailByName(string name);*/
		FileDetails GetFileDetailById(Guid id);
		void UpdateFileDetail(FileDetails fileDetails);
		void DeleteFileDetail(FileDetails fileDetails);
	}
}
