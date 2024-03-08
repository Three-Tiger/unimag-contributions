using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Services;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
	[Route("api/contributions")]
	[ApiController]
	public class ContributionsController : ControllerBase
	{
		private readonly IContributionService _contributionService;

		public ContributionsController(IContributionService contributionService)
		{
			_contributionService = contributionService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			List<ContributionDto> faculties = _contributionService.GetAllContribution();
			return Ok(faculties);
		}

		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			ResponseDto response = new();
			try
			{
				ContributionDto facultyDto = _contributionService.GetContributionById(id);
				return Ok(facultyDto);
			}
			catch (NotFoundException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status404NotFound, response);
			}
			catch (Exception e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody] CreateContributionDto createContributionDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				ContributionDto contributionDto = _contributionService.AddContribution(createContributionDto);
				return Ok(contributionDto);
			}
			catch (ConflictException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status409Conflict, response);
			}
			catch (Exception e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}
		}

		[HttpPut("{id}")]
		public IActionResult Put(Guid id, [FromBody] UpdateContributionDto updateContributionDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				ContributionDto contribution = _contributionService.UpdateContribution(id, updateContributionDto);

				return Ok(contribution);
			}
			catch (NotFoundException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status404NotFound, response);
			}
			catch (Exception e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			ResponseDto response = new();
			try
			{
				response.Message = _contributionService.DeleteContribution(id);

				return Ok(response);
			}
			catch (NotFoundException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status404NotFound, response);
			}
			catch (Exception e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}
		}
	}
}
