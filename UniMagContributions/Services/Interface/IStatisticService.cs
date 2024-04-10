using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Statistic;

namespace UniMagContributions.Services.Interface
{
	public interface IStatisticService
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear();
		Dictionary<string, int> TotalPublicContributionsByFacultyId(Guid facultyId);
		Dictionary<string, int> GetNumberOfContributionsWithoutFeedback(Guid annualMagazineId);
		Dictionary<string, double> GetPercentageOfContributionsWithFeedback(Guid annualMagazineId);
		Dictionary<string, double> GetPercentageOfContributionsWithFeedbackAfter14days(Guid annualMagazineId);
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto);
		Dictionary<string, int> NumberOfAccountsCreated();
        List<ContributionDto> GetTop6Contribution();
    }
}
