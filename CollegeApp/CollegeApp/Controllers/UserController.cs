using CollegeApp.Model;
using CollegeApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private APIResponse _response;
        public UserController(IUserService userService)
        {
            _userService = userService;
            _response = new APIResponse();
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<APIResponse>> CreateUserAsync(UserDto dto)
        {
            try
            {
                var userCreated = await _userService.CreateUserAsync(dto);
                _response.Data = userCreated;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Status = false;
                return _response;
            }
        }

        [HttpGet]
        [Route("getalluser")]
        public async Task<ActionResult<APIResponse>> GetUsersAsync()
        {
            try
            {
                var users = await _userService.GetUsersAsync();

                _response.Data = users;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Status = false;
                return _response;
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUsersById")]
        public async Task<ActionResult<APIResponse>> GetUserByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var user = await _userService.GetUserByIdAsync(id);
                if (user is null)
                    return NotFound();

                _response.Data = user;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Status = false;
                return _response;
            }
        }

        [HttpGet]
        [Route("{username}", Name = "GetUsersByName")]
        public async Task<ActionResult<APIResponse>> GetUserByNameAsync([FromRoute] string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return BadRequest();

                var user = await _userService.GetUserByUsernameAsync(username);
                if (user is null)
                    return NotFound();


                _response.Data = user;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Status = false;
                return _response;
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<APIResponse>> UpdateUserAsync([FromBody] UserDto dto)
        {
            try
            {
                if (dto.Id <= 0 || dto is null)
                    return BadRequest();

                var user = await _userService.UpdateUserAsync(dto);

                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Data = user;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Status = false;
                return _response;
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteUserAsync([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var user = await _userService.DeleteUserAsync(id);

                _response.Data = user;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Errors.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Status = false;
                return _response;
            }
        }
    }
}
