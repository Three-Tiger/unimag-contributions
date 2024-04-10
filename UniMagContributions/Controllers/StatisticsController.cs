using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Statistic;
using UniMagContributions.Services;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Authorize(Roles = "Administrator, Coordinator, Manager, Guest")]
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
		public IActionResult GetContributionsByFacultyAndAcademicYear([FromQuery] StatisticDto statisticDto)
		{
			var contributionsByFacultyAndAcademicYear = _statisticsService.GetContributionsByFacultyAndAcademicYear(statisticDto);
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

		[HttpGet("number-of-contributions-faculty/{facultyId}")]
		public IActionResult NumberOfContributionByFacultyId(Guid facultyId)
		{
			var numberOfContributionByFacultyId = _statisticsService.TotalPublicContributionsByFacultyId(facultyId);
			return Ok(numberOfContributionByFacultyId);
		}


		[HttpGet("top-6")]
        public IActionResult GetTop6()
        {
            List<ContributionDto> contributions = _statisticsService.GetTop6Contribution();
            return Ok(contributions);
        }

		[HttpGet("contribution-without-feedback")]
		public IActionResult GetContributionWithoutComment([FromQuery] StatisticDto statisticDto)
		{
			var contributions = _statisticsService.GetNumberOfContributionsWithoutFeedback(statisticDto);
			return Ok(contributions);
		}

		[HttpGet("percentage-contribution-feedback")]
		public IActionResult GetPercentFeedback([FromQuery] StatisticDto statisticDto)
		{
			var contributions = _statisticsService.GetPercentageOfContributionsWithFeedback(statisticDto);
			return Ok(contributions);
		}

		[HttpGet("percentage-contribution-feedback-after-14days")]
		public IActionResult Get([FromQuery] StatisticDto statisticDto)
		{
			var contributions = _statisticsService.GetPercentageOfContributionsWithFeedbackAfter14days(statisticDto);
			return Ok(contributions);
		}
	}
}
