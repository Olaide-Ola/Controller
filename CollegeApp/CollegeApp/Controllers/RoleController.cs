using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Role> _roleRepository;
        private APIResponse _response;
        public RoleController(IMapper mapper, ICollegeRepository<Role> roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _response = new APIResponse();
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRoleAsync(RoleDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();
                var role = _mapper.Map<Role>(dto);

                role.IsDeleted = false;
                role.CreatededDate = DateTime.UtcNow;
                role.ModifiedDate = DateTime.UtcNow;

                var result = await _roleRepository.CreateAsync(role);

                dto.Id = result.Id;
                _response.Data = dto;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;

                //return Ok(_response);
                return CreatedAtRoute("GetRoleById", new {Id = dto.Id}, _response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }


        }

        [HttpGet("all", Name = "GetAllRoles")]
        public async Task<ActionResult<APIResponse>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();

                _response.Data = _mapper.Map<List<RoleDto>>(roles);
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }
        }

        [HttpGet("{id:int}", Name = "GetRoleById")]
        public async Task<ActionResult<APIResponse>> GetRoleByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var role = await _roleRepository.GetAsync(role => role.Id == id);

                if (role == null)
                    return NotFound($"The Role not found with id: {id}");

                _response.Data = _mapper.Map<RoleDto>(role);
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }
        }

        [HttpGet("{name:alpha}", Name = "GetRoleByName")]
        public async Task<ActionResult<APIResponse>> GetRoleByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest();

                var role = await _roleRepository.GetAsync(role => role.RoleName == name);

                if (role == null)
                    return NotFound($"The Role not found with Name: {name}");

                _response.Data = _mapper.Map<RoleDto>(role);
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponse>> UpdateRoleAsync(RoleDto dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    return BadRequest();

                var existingRole = await _roleRepository.GetAsync(role => role.Id == dto.Id);

                if (existingRole == null)
                    return NotFound();

                var newRole = _mapper.Map<Role>(dto);

                await _roleRepository.UpdateAsync(newRole);

                _response.Data = newRole;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }
        }

        [HttpDelete("delete/{id:int}", Name = "DeleteRoleById")]
        public async Task<ActionResult<APIResponse>> DeleteRoleAsync(int id)
        {
            try
            {
                if ( id <= 0)
                    return BadRequest();

                var role = await _roleRepository.GetAsync(role => role.Id == id);

                if (role == null)
                    return BadRequest($"Role not found with id: {id} to delete");

                await _roleRepository.DeleteAsync(role);

                _response.Data = true; 
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }
        }
    }
}
