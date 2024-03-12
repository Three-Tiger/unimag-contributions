namespace UniMagContributions.Repositories.Interface
{
	public interface IStatisticRepository
	{
		Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear();
		Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear();
		public Dictionary<string, Dictionary<string, int>> GetNumberOfContributorsByFacultyAndAcademicYear();
	}
}
