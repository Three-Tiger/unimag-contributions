using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.Contribution;
using UniMagContributions.Exceptions;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Authorize(Roles = "Guest, Student, Coordinator, Manager")]
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
            List<ContributionDto> contributions = _contributionService.GetAllContribution();
            return Ok(contributions);
        }

        [AllowAnonymous]
        [HttpGet("published")]
        public IActionResult GetContributionIsPublished(int limit)
        {
            List<ContributionDto> contributions = _contributionService.GetContributionIsPublished(limit);
            return Ok(contributions);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            ResponseDto response = new();
            try
            {
                ContributionDto contributionDto = _contributionService.GetContributionById(id);
                return Ok(contributionDto);
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

        [AllowAnonymous]
        [HttpGet("{id}/image")]
        public IActionResult GetContributionPicture(Guid id)
        {
            ResponseDto response = new();
            try
            {
                FileContentResult file = _contributionService.GetContributionPicture(id);
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

        [HttpGet("manage")]
        public IActionResult GetContrubutionByAnnualMagazineIdAndFacultyId([FromQuery] QueryDto queryDto)
        {
            ResponseDto response = new();
            try
            {
                List<ContributionDto> contributions = _contributionService.GetContributionByMagazineIdAndFacultyId(queryDto);
                return Ok(contributions);
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

        [HttpGet("filter")]
        public IActionResult GetContrubutionByFilter([FromQuery] FilterDto filterDto)
        {
            ResponseDto response = new();
            try
            {
                List<ContributionDto> contributions = _contributionService.GetContributionByFilter(filterDto);
                return Ok(contributions);
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

        [HttpGet("annual-magazine/{annualManagazinId}/user/{userId}")]
        public IActionResult GetContrubutionByAnnualMagazineIdAndUserId(Guid annualManagazinId, Guid userId)
        {
            ResponseDto response = new();
            try
            {
                ContributionDto contribution = _contributionService.GetContributionByMagazineIdAndUserId(annualManagazinId, userId);
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

        [HttpGet("user/{userId}")]
        public IActionResult GetContrubutionByUserId(Guid userId)
        {
            ResponseDto response = new();
            try
            {
                List<ContributionDto> contributions = _contributionService.GetContributionByUserId(userId);
                return Ok(contributions);
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
