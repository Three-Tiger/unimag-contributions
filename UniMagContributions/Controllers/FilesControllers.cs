using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Exceptions;
using UniMagContributions.Models;
using UniMagContributions.Services;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Route("api/file-details")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IFileDetailServive _fileDetailServive;

		public FilesController(IFileDetailServive fileDetailServive)
		{
			_fileDetailServive = fileDetailServive;
		}

		[HttpPost]
		public IActionResult Post([FromForm] CreateaFileDetailsDto createaFileDetailsDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				response.Message = _fileDetailServive.AddFileDetail(createaFileDetailsDto);
				return Ok(response);
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

		[HttpPost("multiple-file")]
		public IActionResult Post([FromForm] List<CreateaFileDetailsDto> fileDetails)
		{
			if (fileDetails == null)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				response.Message = _fileDetailServive.AddMultipleFileDetail(fileDetails);
				return Ok(response);
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

		[HttpGet("{id}/download")]
		public IActionResult DownloadFile(Guid id)
		{
			ResponseDto response = new();
			try
			{
                FileContentResult result = _fileDetailServive.DownloadFileById(id);
				return result;
			}
			catch (Exception e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status500InternalServerError, response);
			}
		}

        [HttpGet("{contributionId}/download-multiple")]
        public IActionResult DownloadMultipleFile(Guid contributionId)
        {
            ResponseDto response = new();
            try
            {
                FileContentResult result = _fileDetailServive.DownloadMultipleFile(contributionId);
                return result;
            }
            catch (Exception e)
            {
				response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
