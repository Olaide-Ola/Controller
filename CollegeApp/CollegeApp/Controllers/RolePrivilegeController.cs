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
    public class RolePrivilegeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<RolePrivilege> _rolePrivilegeRepository;
        private APIResponse _response;
        public RolePrivilegeController(IMapper mapper, ICollegeRepository<RolePrivilege> rolePrivilegeRepository)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _response = new APIResponse();
        }

        [HttpPost("create")]
        public async Task<ActionResult<APIResponse>> CreateRolePrivilegeAsync(RolePrivilegeDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();
                var role = _mapper.Map<RolePrivilege>(dto);

                role.IsDeleted = false;
                role.CreatededDate = DateTime.UtcNow;
                role.ModifiedDate = DateTime.UtcNow;

                var result = await _rolePrivilegeRepository.CreateAsync(role);

                dto.Id = result.Id;
                _response.Data = dto;
                _response.Status = true;
                _response.StatusCode = HttpStatusCode.OK;

                //return Ok(_response);
                return CreatedAtRoute("GetRolePrivilegeById", new { Id = dto.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(ex.Message);
                return _response;
            }


        }

        [HttpGet("all", Name = "GetAllRolePrivilege")]
        public async Task<ActionResult<APIResponse>> GetRolePrivilegeAsync()
        {
            try
            {
                var roles = await _rolePrivilegeRepository.GetAllAsync();

                _response.Data = _mapper.Map<List<RolePrivilegeDto>>(roles);
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

        [HttpGet("{roleId:int}", Name = "GetRolePrivilegeById")]
        public async Task<ActionResult<APIResponse>> GetRolePrivilegeByIdAsync(int roleId)
        {
            try
            {
                if (roleId <= 0)
                    return BadRequest();

                var role = await _rolePrivilegeRepository.GetAllByFilterAsync(role => role.RoleId == roleId);

                if (role == null)
                    return NotFound($"The Role Privilege not found with id: {roleId}");

                _response.Data = _mapper.Map<List<RolePrivilegeDto>>(role);
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

        [HttpGet("{name:alpha}", Name = "GetRolePrivilegeByName")]
        public async Task<ActionResult<APIResponse>> GetRolePrivilegeByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest();

                var role = await _rolePrivilegeRepository.GetAsync(role => role.RolePrivilegeName.Contains(name));

                if (role == null)
                    return NotFound($"The Role Privilege not found with Name: {name}");

                _response.Data = _mapper.Map<RolePrivilegeDto>(role);
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
        public async Task<ActionResult<APIResponse>> UpdateRolePrivilegeAsync(RolePrivilegeDto dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    return BadRequest();

                var existingRole = await _rolePrivilegeRepository.GetAsync(role => role.Id == dto.Id);

                if (existingRole == null)
                    return NotFound();

                var newRole = _mapper.Map<RolePrivilege>(dto);

                await _rolePrivilegeRepository.UpdateAsync(newRole);

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

        [HttpDelete("delete/{id:int}", Name = "DeletePrivilegeRoleById")]
        public async Task<ActionResult<APIResponse>> DeleteRolePrivilegeAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var role = await _rolePrivilegeRepository.GetAsync(role => role.Id == id);

                if (role == null)
                    return BadRequest($"Role not found with id: {id} to delete");

                await _rolePrivilegeRepository.DeleteAsync(role);

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
