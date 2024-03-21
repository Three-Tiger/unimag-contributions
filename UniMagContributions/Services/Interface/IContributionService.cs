using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.Contribution;

namespace UniMagContributions.Services.Interface
{
	public interface IContributionService
	{
		ContributionDto AddContribution(CreateContributionDto contributionDto);
		ContributionDto UpdateContribution(Guid id, UpdateContributionDto contributionDto);
		string DeleteContribution(Guid id);
		ContributionDto GetContributionById(Guid id);
		FileContentResult GetContributionPicture(Guid id);
        List<ContributionDto> GetContributionByMagazineId(Guid annualManagazinId);
		List<ContributionDto> GetAllContribution();
		List<ContributionDto> GetTop6Contribution();
		List<ContributionDto> GetContributionIsPublished(int limit);
		ContributionDto GetContributionByMagazineIdAndUserId(Guid annualManagazinId, Guid userId);
		List<ContributionDto> GetContributionByUserId(Guid userId);
    }
}
