using UniMagContributions.Dto.Statistic;
using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IStatisticRepository
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear(StatisticDto statisticDto);
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, int> TotalPublicContributionsByFacultyId(Guid facultyId);
		Dictionary<string, int> GetNumberOfContributionsWithoutFeedback(StatisticDto statisticDto);
		Dictionary<string, double> GetPercentageOfContributionsWithFeedback(StatisticDto statisticDto);
		Dictionary<string, double> GetPercentageOfContributionsWithFeedbackAfter14days(StatisticDto statisticDto);
		Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto);
		Dictionary<string, int> NumberOfAccountsCreated();
        List<Contribution> GetTop6Contribution();
    }
}
