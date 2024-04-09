using UniMagContributions.Dto.Statistic;
using UniMagContributions.Models;

namespace UniMagContributions.Repositories.Interface
{
	public interface IStatisticRepository
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear();
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto);
		Dictionary<string, int> NumberOfAccountsCreated();
        List<Contribution> GetTop6Contribution();
    }
}
