using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Models;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
	[Route("api/file-details")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IFileService _uploadService;

		public FilesController(IFileService uploadService)
		{
			_uploadService = uploadService;
		}

		/// <summary>
		/// Single File Upload
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpPost("PostSingleFile")]
		public async Task<ActionResult> PostSingleFile([FromForm] FileUploadDto fileUploadDto)
		{
			if (fileUploadDto == null)
			{
				return BadRequest();
			}

			ResponseDto response = new();
			try
			{
				response.Message = _uploadService.PostFile(fileUploadDto);
				return Ok(response);
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Multiple File Upload
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpPost("PostMultipleFile")]
		public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadDto> fileDetails)
		{
			if (fileDetails == null)
			{
				return BadRequest();
			}

			ResponseDto response = new();
			try
			{
				response.Message = _uploadService.PostMultiFile(fileDetails);
				return Ok();
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Download File
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpGet("DownloadFile")]
		public async Task<ActionResult> DownloadFile(Guid id)
		{
			try
			{
				ResponseDto response = new();
				response.Message = _uploadService.DownloadFileById(id);
				return Ok();
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
