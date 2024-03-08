using UniMagContributions.Dto.AnnualMagazine;

namespace UniMagContributions.Services.Interface
{
	public interface IAnnualMagazineService
	{
		AnnualMagazineDto AddAnnualMagazine(CreateAnnualMagazineDto AnnualMagazineDto);
		AnnualMagazineDto UpdateAnnualMagazine(Guid id, UpdateAnnualMagazineDto AnnualMagazineDto);
		string DeleteAnnualMagazine(Guid id);
		AnnualMagazineDto GetAnnualMagazineById(Guid id);
		List<AnnualMagazineDto> GetAllAnnualMagazine();
	}
}
