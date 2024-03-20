using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
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
        public IActionResult GetAcceptanceRejectionRate()
        {
            var getAcceptanceRejectionRate = _statisticsService.GetAcceptanceRejectionRate();
            return Ok(getAcceptanceRejectionRate);
        }

        [HttpGet("number-of-accounts-created")]
        public IActionResult NumberOfAccountsCreated()
        {
            var numberOfAccountsCreated = _statisticsService.NumberOfAccountsCreated();
            return Ok(numberOfAccountsCreated);
        }
    }
}
