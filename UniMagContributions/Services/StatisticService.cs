using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
	public class StatisticService : IStatisticService
	{
		private readonly IStatisticRepository _statisticsRepository;

		public StatisticService(IStatisticRepository statisticsRepository)
		{
			_statisticsRepository = statisticsRepository;
		}

		public Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear()
		{
			return _statisticsRepository.GetContributionsByFacultyAndAcademicYear(); ;
		}

		public Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear()
		{
			return _statisticsRepository.GetPercentageContributionsByFacultyAndAcademicYear();
		}

		public Dictionary<string, double> GetAcceptanceRejectionRate()
		{
            return _statisticsRepository.GetAcceptanceRejectionRate();
        }

		public Dictionary<string, int> NumberOfAccountsCreated()
		{
            return _statisticsRepository.NumberOfAccountsCreated();
        }
	}
}
