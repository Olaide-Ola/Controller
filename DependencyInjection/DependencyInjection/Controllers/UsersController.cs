using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Someone requested user with ID: {userId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID requested: {userId}", id);
                return BadRequest();
            }

            if (id == 99)
            {
                _logger.LogWarning("User {0} not found in database", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully returned user {0}", id);
            return Ok(new { Id = id, Name = "John Doe" });
        }
    }
}
