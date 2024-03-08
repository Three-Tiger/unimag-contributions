using System.ComponentModel.DataAnnotations;

namespace UniMagContributions.Dto.Faculty
{
    public class FacultyDto
    {
        public Guid FacultyId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
