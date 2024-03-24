using UniMagContributions.Dto.Contribution;

namespace UniMagContributions.Services.Interface
{
	public interface IStatisticService
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear();
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, double> GetAcceptanceRejectionRate();
		Dictionary<string, int> NumberOfAccountsCreated();
        List<ContributionDto> GetTop6Contribution();
    }
}
