﻿using UniMagContributions.Constraints;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IContributionRepository
	{
		void CreateContribution(Contribution contribution);
		Contribution GetContributionByTitle(string title);
		Contribution GetContributionById(Guid id);
		List<Contribution> GetAllContribution();
		List<Contribution> GetContributionIsPublished(int limit);
		List<Contribution> GetContributionByMagazineIdAndFacultyId(QueryDto queryDto);
		List<Contribution> GetContributionByFilter(FilterDto filterDto);
		List<Contribution> GetContributionByUserId(Guid userId);
		void UpdateContribution(Contribution contribution);
		void DeleteContribution(Contribution contribution);
        List<Contribution> GetContributionByMagazineIdAndUserId(Guid userId, Guid annualMagazineId);
	}
}
