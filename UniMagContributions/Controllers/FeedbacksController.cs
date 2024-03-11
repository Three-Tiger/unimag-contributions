using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Dto;
using UniMagContributions.Dto.Feedback;
using UniMagContributions.Exceptions;
using UniMagContributions.Services;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
	[Route("api/feedbacks")]
	[ApiController]
	public class FeedbacksController : ControllerBase
	{
		private readonly IFeedbackService _feedbackService;

		public FeedbacksController(IFeedbackService feedbackService)
		{
			_feedbackService = feedbackService;
		}

		[HttpGet("/api/getAllFeedbacks/{id}")]
		public IActionResult GetAllFeedbacks(Guid id)
		{
			List<FeedbackDto> feedbacksList = _feedbackService.GetAllFeedback(id);
			return Ok(feedbacksList);
		}

		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			ResponseDto response = new();
			try
			{
				FeedbackDto facultyDto = _feedbackService.GetFeedbackById(id);
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
		public IActionResult Post([FromBody] CreateFeedbackDto createFeedbackDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				FeedbackDto feedbackDto = _feedbackService.AddFeedback(createFeedbackDto);
				return Ok(feedbackDto);
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
		public IActionResult Put(Guid id, [FromBody] UpdateFeedbackDto updateFeedbackDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				FeedbackDto feedback = _feedbackService.UpdateFeedback(id, updateFeedbackDto);

				return Ok(feedback);
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
				response.Message = _feedbackService.DeleteFeedback(id);

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
