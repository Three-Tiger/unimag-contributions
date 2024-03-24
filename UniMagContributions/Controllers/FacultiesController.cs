using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Exceptions;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
	[Authorize(Roles = "Administrator")]
	[Route("api/faculties")]
	[ApiController]
	public class FacultiesController : ControllerBase
	{
		private readonly IFacultyService _facultyService;

		public FacultiesController(IFacultyService facultyService)
		{
			_facultyService = facultyService;
		}

        [AllowAnonymous]
        [HttpGet]
		public IActionResult Get()
		{
			List<FacultyDto> faculties = _facultyService.GetAllFaculty();
			return Ok(faculties);
		}

		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			ResponseDto response = new();
			try
			{
				FacultyDto facultyDto = _facultyService.GetFacultyById(id);
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
		public IActionResult Post([FromBody] CreateFacultyDto createFacultyDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				FacultyDto facultyDto = _facultyService.AddFaculty(createFacultyDto);
				return Ok(facultyDto);
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
		public IActionResult Put(Guid id, [FromBody] UpdateFacultyDto updateFacultyDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				FacultyDto faculty = _facultyService.UpdateFaculty(id, updateFacultyDto);

				return Ok(faculty);
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
				response.Message = _facultyService.DeleteFaculty(id);

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
