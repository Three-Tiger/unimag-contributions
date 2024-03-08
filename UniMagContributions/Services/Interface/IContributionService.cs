using UniMagContributions.Dto.Contribution;

namespace UniMagContributions.Services.Interface
{
	public interface IContributionService
	{
		ContributionDto AddContribution(CreateContributionDto contributionDto);
		ContributionDto UpdateContribution(Guid id, UpdateContributionDto contributionDto);
		string DeleteContribution(Guid id);
		ContributionDto GetContributionById(Guid id);
		List<ContributionDto> GetAllContribution();
	}
}
