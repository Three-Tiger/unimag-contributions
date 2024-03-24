using UniMagContributions.Constraints;

namespace UniMagContributions.Dto.Contribution
{
    public class FilterDto
    {
        public Guid? AnnualMagazineId { get; set; }
        public EStatus? Status { get; set; }
        public bool? IsPublished { get; set; }
    }
}
