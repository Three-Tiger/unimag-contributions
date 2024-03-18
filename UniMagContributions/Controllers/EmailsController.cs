using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Dto;
using UniMagContributions.Models;
using UniMagContributions.Services.Interface;

namespace UniMagContributions.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendMail([FromBody] Message message)
        {
            ResponseDto response = new()
            {
                Message = _emailService.SendEmail(message)
            };
            return Ok(response);
        }

        [HttpPost("async")]
        public async Task<IActionResult> SendMailAsync([FromBody] Message message)
        {
            ResponseDto response = new()
            {
                Message = await _emailService.SendEmailAsync(message)
            };
            return Ok(response);
        }
    }
}
