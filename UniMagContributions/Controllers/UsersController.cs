using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Services.Interface;
using UniMagContributions.Exceptions;
using UniMagContributions.Dto.User;
using Microsoft.AspNetCore.Authorization;

namespace UniMagContributions.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("{id}/image")]
        public IActionResult GetProfilePicture(Guid id)
        {
            ResponseDto response = new();
            try
            {
                FileContentResult file = _userService.GetUserProfile(id);
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

        [HttpGet]
        public IActionResult Get()
        {
            ResponseDto response = new();
            try
            {
                List<UserDto> users = _userService.GetAllUser();
                return Ok(users);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            ResponseDto response = new();
            try
            {
                UserDto user = _userService.GetUserById(id);
                return Ok(user);
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
        public IActionResult Create([FromForm] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseDto response = new();
            try
            {
                UserDto user = _userService.CreateUser(createUserDto);
                return Ok(user);
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

        [HttpPut("{id}/profile")]
        public IActionResult UpdateProfile(Guid id, [FromForm] UpdateUserDto updateUserDto)
        {
            ResponseDto response = new();
            try
            {
                UserDto user = _userService.UpdateProfile(id, updateUserDto);
                return Ok(user);
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

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromForm] UpdateUserDto updateUserDto)
        {
            ResponseDto response = new();
            try
            {
                UserDto user = _userService.UpdateUser(id, updateUserDto);
                return Ok(user);
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

        [HttpPut("{id}/change-password")]
        public IActionResult ChangePassword(Guid id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _userService.ChangePassword(id, changePasswordDto);
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                response.Message = e.Message;
                return StatusCode(StatusCodes.Status404NotFound, response);
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
        public IActionResult Delete(Guid id)
        {
            ResponseDto response = new();
            try
            {
                response.Message = _userService.DeleteUser(id);
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
