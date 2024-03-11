using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.AnnualMagazine
{
	public class AnnualMagazineDto
	{
		public Guid AnnualMagazineId { get; set; }

		public string AcademicYear { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime ClosureDate { get; set; }

		public DateTime FinalClosureDate { get; set; }
	}
}
