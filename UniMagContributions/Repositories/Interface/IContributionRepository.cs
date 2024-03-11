using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IContributionRepository
	{
		void CreateContribution(Contribution contribution);
		Contribution GetContributionByTitle(string title);
		Contribution GetContributionById(Guid id);
		List<Contribution> GetAllContribution();
		List<Contribution> GetContributionByMagazineId(Guid annualManagazinId);
		void UpdateContribution(Contribution contribution);
		void DeleteContribution(Contribution contribution);
	}
}
