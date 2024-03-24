using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Exceptions;
using UniMagContributions.Services.Interface;
using UniMagContributions.Dto.ImageDetail;
using Microsoft.AspNetCore.Authorization;

namespace UniMagContributions.Controllers
{
    [Authorize]
    [Route("api/image-details")]
    [ApiController]
    public class ImageDetailsController : ControllerBase
    {
        private readonly IImageDetailService _imageDetailService;

        public ImageDetailsController(IImageDetailService imageDetailService)
        {
            _imageDetailService = imageDetailService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetContributionPicture(Guid id)
        {
            ResponseDto response = new();
            try
            {
                FileContentResult file = _imageDetailService.GetImage(id);
                return file;
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
        public IActionResult Post([FromForm] CreateImageDetailDto createImageDetailDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseDto response = new();
            try
            {
                response.Message = _imageDetailService.AddImageDetail(createImageDetailDto);
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

        [HttpPost("multiple-image")]
        public IActionResult Post([FromForm] List<CreateImageDetailDto> imageDetails)
        {
            if (imageDetails == null)
            {
                return BadRequest(ModelState);
            }

            ResponseDto response = new();
            try
            {
                response.Message = _imageDetailService.AddMultipleImageDetail(imageDetails);
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
                FileContentResult result = _imageDetailService.DownloadFileById(id);
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
                FileContentResult result = _imageDetailService.DownloadMultipleImage(contributionId);
                return result;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("contribution/{contributionId}")]
        public IActionResult DeleteImageByContributionId(Guid contributionId)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _imageDetailService.DeleteImageByContributionId(contributionId);
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
