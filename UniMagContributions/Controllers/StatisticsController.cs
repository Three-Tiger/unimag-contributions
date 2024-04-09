using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Statistic;
using UniMagContributions.Services;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Authorize(Roles = "Administrator, Coordinator, Student")]
    [Route("api/statistics")]
	[ApiController]
	public class StatisticsController : ControllerBase
	{
		private readonly IStatisticService _statisticsService;

		public StatisticsController(IStatisticService statisticsService)
		{
			_statisticsService = statisticsService;
		}

		[HttpGet("number-of-contributions")]
		public IActionResult GetContributionsByFacultyAndAcademicYear()
		{
			var contributionsByFacultyAndAcademicYear = _statisticsService.GetContributionsByFacultyAndAcademicYear();
			return Ok(contributionsByFacultyAndAcademicYear);
		}

		[HttpGet("percentage-of-contributions")]
		public IActionResult GetPercentageContributionsByFacultyAndAcademicYear()
		{
			var contributionsByFacultyAndAcademicYear = _statisticsService.GetPercentageContributionsByFacultyAndAcademicYear();
			return Ok(contributionsByFacultyAndAcademicYear);
		}

        [HttpGet("acceptance-rejection-rate")]
        public IActionResult GetAcceptanceRejectionRate([FromQuery] StatisticDto statisticDto)
        {
            var getAcceptanceRejectionRate = _statisticsService.GetAcceptanceRejectionRate(statisticDto);
            return Ok(getAcceptanceRejectionRate);
        }

        [HttpGet("number-of-accounts-created")]
        public IActionResult NumberOfAccountsCreated()
        {
            var numberOfAccountsCreated = _statisticsService.NumberOfAccountsCreated();
            return Ok(numberOfAccountsCreated);
        }

        [HttpGet("top-6")]
        public IActionResult GetTop6()
        {
            List<ContributionDto> contributions = _statisticsService.GetTop6Contribution();
            return Ok(contributions);
        }
    }
}
