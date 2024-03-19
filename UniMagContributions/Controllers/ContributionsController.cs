using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            List<ContributionDto> contributions = _contributionService.GetAllContribution();
            return Ok(contributions);
        }

        [HttpGet("top-6")]
        public IActionResult GetTop6()
        {
            List<ContributionDto> contributions = _contributionService.GetTop6Contribution();
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

        [HttpGet("annual-magazine/{annualManagazinId}")]
        public IActionResult GetContrubutionByAnnualMagazineId(Guid annualManagazinId)
        {
            ResponseDto response = new();
            try
            {
                List<ContributionDto> contributions = _contributionService.GetContributionByMagazineId(annualManagazinId);
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
