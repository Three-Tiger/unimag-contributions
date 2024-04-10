using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Statistic;

namespace UniMagContributions.Services.Interface
{
	public interface IStatisticService
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear(StatisticDto statisticDto);
		Dictionary<string, int> TotalPublicContributionsByFacultyId(Guid facultyId);
		Dictionary<string, int> GetNumberOfContributionsWithoutFeedback(StatisticDto statisticDto);
		Dictionary<string, double> GetPercentageOfContributionsWithFeedback(StatisticDto statisticDto);
		Dictionary<string, double> GetPercentageOfContributionsWithFeedbackAfter14days(StatisticDto statisticDto);
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto);
		Dictionary<string, int> NumberOfAccountsCreated();
        List<ContributionDto> GetTop6Contribution();
    }
}
