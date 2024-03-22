using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IContributionRepository
	{
		void CreateContribution(Contribution contribution);
		Contribution GetContributionByTitle(string title);
		Contribution GetContributionById(Guid id);
		List<Contribution> GetAllContribution();
		List<Contribution> GetTop6Contribution();
		List<Contribution> GetContributionIsPublished(int limit);
		List<Contribution> GetContributionByMagazineId(Guid annualManagazinId);
		List<Contribution> GetContributionByUserId(Guid userId);
		void UpdateContribution(Contribution contribution);
		void DeleteContribution(Contribution contribution);
		Contribution IsContributionExist(Guid userId, Guid annualMagazineId);
	}
}
