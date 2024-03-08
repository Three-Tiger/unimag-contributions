﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Dto.AnnualMagazine;
using UniMagContributions.Dto.Faculty;
using UniMagContributions.Exceptions;
using UniMagContributions.Services;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
	[Route("api/annualMagazines")]
	[ApiController]
	public class AnnualMagazinesController : ControllerBase
	{
		private readonly IAnnualMagazineService _annualMagazineService;

		public AnnualMagazinesController(IAnnualMagazineService annualMagazineService)
		{
			_annualMagazineService = annualMagazineService;
		}

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
			catch (ConflictException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status409Conflict, response);
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

		[HttpDelete("{id}")]
		public IActionResult Delete(Guid id, [FromBody] AnnualMagazineDto annualMagazineDto)
		{
			ResponseDto response = new();
			try
			{
				string annualMagazine = _annualMagazineService.DeleteAnnualMagazine(id);

				return Ok(annualMagazine);
			}
			catch (ConflictException e)
			{
				response.Message = e.Message;
				return StatusCode(StatusCodes.Status409Conflict, response);
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
	}
}
