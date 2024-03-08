using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.AnnualMagazine
{
	public class CreateAnnualMagazineDto
	{
		[Required(ErrorMessage = "The Academic Year can not empty!")]
		public string AcademicYear { get; set; }

		[Required(ErrorMessage = "The Title can not empty!")]
		public string Title { get; set; }

		[Required(ErrorMessage = "The Description can not empty!")]
		public string Description { get; set; }

		[Required(ErrorMessage = "The Closure Date can not empty!")]
		public DateTime ClosureDate { get; set; }

		[Required(ErrorMessage = "The Final Closure Date can not empty!")]
		public DateTime FinalClosureDate { get; set; }
	}
}
