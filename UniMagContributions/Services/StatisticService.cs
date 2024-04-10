using AutoMapper;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Statistic;
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

		public Dictionary<string, Dictionary<string, int>> GetContributionsByFacultyAndAcademicYear(StatisticDto statisticDto)
		{
			return _statisticsRepository.GetContributionsByFacultyAndAcademicYear(statisticDto); ;
		}

		public Dictionary<string, Dictionary<string, double>> GetPercentageContributionsByFacultyAndAcademicYear()
		{
			return _statisticsRepository.GetPercentageContributionsByFacultyAndAcademicYear();
		}

		public Dictionary<string, double> GetAcceptanceRejectionRate(StatisticDto statisticDto)
		{
            return _statisticsRepository.GetAcceptanceRejectionRate(statisticDto);
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

		public Dictionary<string, int> TotalPublicContributionsByFacultyId(Guid facultyId)
		{
			return _statisticsRepository.TotalPublicContributionsByFacultyId(facultyId);
		}

		public Dictionary<string, int> GetNumberOfContributionsWithoutFeedback(StatisticDto statisticDto)
		{
			return _statisticsRepository.GetNumberOfContributionsWithoutFeedback(statisticDto);
		}

		public Dictionary<string, double> GetPercentageOfContributionsWithFeedback(StatisticDto statisticDto)
		{
			return _statisticsRepository.GetPercentageOfContributionsWithFeedback(statisticDto);
		}

		public Dictionary<string, double> GetPercentageOfContributionsWithFeedbackAfter14days(StatisticDto statisticDto)
		{
			return _statisticsRepository.GetPercentageOfContributionsWithFeedbackAfter14days(statisticDto);
		}
	}
}
