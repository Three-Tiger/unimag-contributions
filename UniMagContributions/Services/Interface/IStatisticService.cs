namespace UniMagContributions.Services.Interface
{
	public interface IStatisticService
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear();
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		Dictionary<string, Dictionary<string, int>> GetNumberOfContributorsByFacultyAndAcademicYear();
	}
}
