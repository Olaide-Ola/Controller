using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/demo")]
    public class DemoController : ControllerBase
    {
        private readonly IMyLogger _myLogger;
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger, IMyLogger myLogger)
        {
            _logger = logger;
            _myLogger = myLogger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message from Information method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Error method");
            _logger.LogCritical("Log message from Critical method");

            _myLogger.Log("Olaide Software Engineering");

            //var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            //Console.WriteLine(key);

            return Ok(new List<string> { "John", "Mary" });
        }
    }
}
