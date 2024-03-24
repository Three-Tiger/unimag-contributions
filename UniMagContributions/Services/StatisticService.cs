using AutoMapper;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Models;
using UniMagContributions.Repositories;
using UniMagContributions.Repositories.Interface;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Services
{
	public class StatisticService : IStatisticService
	{
        private readonly IMapper _mapper;
        private readonly IStatisticRepository _statisticsRepository;

		public StatisticService(IMapper mapper, IStatisticRepository statisticsRepository)
		{
			_mapper = mapper;
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

        public List<ContributionDto> GetTop6Contribution()
        {
            List<Contribution> contributionList = _statisticsRepository.GetTop6Contribution();
            return _mapper.Map<List<ContributionDto>>(contributionList);
        }
    }
}
