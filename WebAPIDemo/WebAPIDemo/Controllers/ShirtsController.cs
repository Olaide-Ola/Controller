using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Filters;
using WebAPIDemo.Model;
using WebAPIDemo.Model.Repositories;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api/shirts")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirt()
        {
            return Ok(ShirtRepository.GetAllShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilterAttribte]
        public ActionResult<Shirt> GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        public IActionResult CreateShirt(Shirt shirt)
        {
            return Ok("Creating a shirt and the ID is: {shirt.ShirtId}");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            if (id != shirt.ShirtId) return BadRequest();
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilterAttribte]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }
    }
}
