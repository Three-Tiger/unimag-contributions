using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Statistic;

namespace UniMagContributions.Services.Interface
{
	public interface IStatisticService
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear();
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto);
		Dictionary<string, int> NumberOfAccountsCreated();
        List<ContributionDto> GetTop6Contribution();
    }
}
