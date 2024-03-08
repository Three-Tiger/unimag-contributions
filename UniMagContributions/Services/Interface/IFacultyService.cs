using UniMagContributions.Dto.Faculty;
using UniMagContributions.Models;

namespace UniMagContributions.Services.Interface
{
    public interface IFacultyService
	{
		FacultyDto AddFaculty(CreateFacultyDto facultyDto);
		FacultyDto UpdateFaculty(Guid id, UpdateFacultyDto facultyDto);
		string DeleteFaculty(Guid id);
		FacultyDto GetFacultyById(Guid id);
		List<FacultyDto> GetAllFaculty();
	}
}
