using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Exceptions;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Authorize(Roles = "Administrator, Coordinator, Manager")]
    [Authorize]
    [Route("api/annual-magazines")]
	[ApiController]
	public class AnnualMagazinesController : ControllerBase
	{
		private readonly IAnnualMagazineService _annualMagazineService;

		public AnnualMagazinesController(IAnnualMagazineService annualMagazineService)
		{
			_annualMagazineService = annualMagazineService;
		}

        [AllowAnonymous]
        [HttpGet]
		public IActionResult Get()
		{
			List<AnnualMagazineDto> annualMagazines = _annualMagazineService.GetAllAnnualMagazine();
			return Ok(annualMagazines);
		}

		[HttpGet("{id}")]
		public IActionResult Get(Guid id)
		{
			ResponseDto response = new();
			try
			{
				AnnualMagazineDto annualMagazineDto = _annualMagazineService.GetAnnualMagazineById(id);
				return Ok(annualMagazineDto);
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
		public IActionResult Post([FromBody] CreateAnnualMagazineDto createAnnualMagazineDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				AnnualMagazineDto annualMagazineDto = _annualMagazineService.AddAnnualMagazine(createAnnualMagazineDto);
				return Ok(annualMagazineDto);
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
		public IActionResult Put(Guid id, [FromBody] UpdateAnnualMagazineDto updateAMDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				AnnualMagazineDto faculty = _annualMagazineService.UpdateAnnualMagazine(id, updateAMDto);

				return Ok(faculty);
			}
			catch (NotFoundException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status404NotFound, response);
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

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
			ResponseDto response = new();
			try
			{
				response.Message = _annualMagazineService.DeleteAnnualMagazine(id);

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
