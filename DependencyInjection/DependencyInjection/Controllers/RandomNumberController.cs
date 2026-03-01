using DependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomNumberController : ControllerBase
    {
        private readonly IRandom _random1;
        private readonly IRandom _random2;
        public RandomNumberController(IRandom random)
        {
            _random1 = random;
            _random2 = random;
        }
         
        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"{_random1.GetNumber()}, {_random2.GetNumber()}");
        }
    }
}
