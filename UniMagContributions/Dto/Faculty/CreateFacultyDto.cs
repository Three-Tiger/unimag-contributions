using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.Faculty
{
	public class CreateFacultyDto
	{
		[Required(ErrorMessage = "The faculty name can not empty!")]
		public string Name { get; set; }

		[Required(ErrorMessage = "The description can not empty!")]
		public string Description { get; set; }
	}
}
