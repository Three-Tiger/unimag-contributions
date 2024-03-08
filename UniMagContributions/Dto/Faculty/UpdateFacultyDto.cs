using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.Faculty
{
	public class UpdateFacultyDto : CreateFacultyDto
	{
		public Guid FacultyId { get; set; }
	}
}
