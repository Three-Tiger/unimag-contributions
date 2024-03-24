using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.FileDetails;
using UniMagContributions.Exceptions;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Authorize]
    [Route("api/file-details")]
	[ApiController]
	public class FileDetailsController : ControllerBase
	{
		private readonly IFileDetailServive _fileDetailServive;

		public FileDetailsController(IFileDetailServive fileDetailServive)
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
			if (fileDetails.Count == 0)
			{
				return BadRequest(ModelState);
			}

			ResponseDto response = new();
			try
			{
				response.Message = _fileDetailServive.AddMultipleFileDetail(fileDetails);
				return Ok(response);
			}
			catch (InvalidException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status400BadRequest, response);
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

        [HttpGet("{id}/read")]
        public IActionResult ReadFile(Guid id)
        {
            ResponseDto response = new();
            try
            {
                FileContentResult result = _fileDetailServive.ReadFileById(id);
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

        [HttpPost("contribution/download-multiple")]
        public IActionResult DownloadMultipleFileByListContributionId(List<Guid> lstContributionId)
        {
            ResponseDto response = new();
            try
            {
                FileContentResult result = _fileDetailServive.DownloadMultipleFileByListContributionId(lstContributionId);
                return result;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("contribution/{contributionId}")]
		public IActionResult DeleteFileByContributionId(Guid contributionId)
		{
            ResponseDto response = new();
            try
            {
                response.Message = _fileDetailServive.DeleteFileByContributionId(contributionId);
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
