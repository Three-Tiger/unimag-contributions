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


		// GET api/<StatisticsController>/5
		[HttpGet("1")]
		public IActionResult GetContributionsByFacultyAndAcademicYear()
		{
			var contributionsByFacultyAndAcademicYear = _statisticsService.GetContributionsByFacultyAndAcademicYear();
			return Ok(contributionsByFacultyAndAcademicYear);
		}

		[HttpGet("2")]
		public IActionResult GetPercentageContributionsByFacultyAndAcademicYear()
		{
			var contributionsByFacultyAndAcademicYear = _statisticsService.GetPercentageContributionsByFacultyAndAcademicYear();
			return Ok(contributionsByFacultyAndAcademicYear);
		}

		[HttpGet("3")]
		public IActionResult GetNumberOfContributorsByFacultyAndAcademicYear()
		{
			var contributionsByFacultyAndAcademicYear = _statisticsService.GetNumberOfContributorsByFacultyAndAcademicYear();
			return Ok(contributionsByFacultyAndAcademicYear);
		}
	}
}
