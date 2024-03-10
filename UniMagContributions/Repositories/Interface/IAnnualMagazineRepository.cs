using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IAnnualMagazineRepository
	{
		void CreateAnnualMagazine(AnnualMagazine annualMagazine);
		AnnualMagazine GetAnnualMagazineByAcademicYear(string name);
		AnnualMagazine GetAnnualMagazineById(Guid id);
		List<AnnualMagazine> GetAllAnnualMagazine();
		void UpdateAnnualMagazine(AnnualMagazine annualMagazine);
		void DeleteAnnualMagazine(AnnualMagazine annualMagazine);
		bool CheckUpdateAcademicYear(string academicYear, Guid id);
	}
}
