using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniMagContributions.Constraints;
using UniMagContributions.Models;

namespace UniMagContributions.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get all students");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Get student with id {id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Create student");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"Update student with id {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Delete student with id {id}");
        }
    }
}
