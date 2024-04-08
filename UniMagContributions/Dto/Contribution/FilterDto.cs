using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.Contribution
{
    public class FilterDto
    {
        public Guid? FacultyId { get; set; }
        public Guid? AnnualMagazineId { get; set; }
        public EStatus? Status { get; set; }
        public bool? IsPublished { get; set; }
    }
}
