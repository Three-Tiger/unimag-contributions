using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IFacultyRepository
	{
		void CreateFaculty(Faculty faculty);
		Faculty GetFacultyByName(string name);
		Faculty GetFacultyById(Guid id);
		List<Faculty> GetAllFaculty(int limit);
		void UpdateFaculty(Faculty faculty);
		void DeleteFaculty(Faculty faculty);
	}
}
