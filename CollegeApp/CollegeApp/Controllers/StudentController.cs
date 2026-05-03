using AutoMapper;
using CollegeApp.Configuration;
using CollegeApp.Data.Repository;
using CollegeApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/student")]
    //[EnableCors("AllowGoogle")]

    //[Authorize(Roles = "Superadmin,Admin")]
    //[Authorize(Policy = "LoginFrontendJS")]
    [Authorize(AuthenticationSchemes = "LoginFrontendJS", Roles = "Admin")]

    //[DisableCors]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;    
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository; //REMOVE THE SPECIFIC repository
        //private readonly ICollegeRepository<Data.Student> _studentRepository; //THIS IS A GENERIC REPOSITORY

        private APIResponse _aPIResponse; //not readonly becuase we need to modifty it and want the controller to create the obect itself instead of receiving via DI
        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository /*ICollegeRepository<Data.Student> studentRepository*/)
        {
            _logger = logger;
            _mapper = mapper;
            //_studentRepository = studentRepository;
            _studentRepository = studentRepository;
            _aPIResponse = new APIResponse();
        }

        [HttpGet]
        [Route("all", Name = "GettAllTheStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> GetStudentsAsync()
        {
            try
            {
                _logger.LogInformation("GetStudents method started");
                var student = await _studentRepository.GetAllAsync();
                //var studentdto = _mapper.Map<List<StudentDto>>(student);
                _aPIResponse.Data = _mapper.Map<List<StudentDto>>(student);
                _aPIResponse.Status = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> GetStudentByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid user ID requested: {userId}", id);
                    return BadRequest();
                }
                var student = await _studentRepository.GetAsync(student => student.Id == id);
                if (student is null)
                {
                    _logger.LogInformation("User with ID: {userID} not found", id);
                    return NotFound();
                }
                _aPIResponse.Data = _mapper.Map<StudentDto>(student);
                _aPIResponse.Status = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }

        [HttpGet]
        [Route("{name}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudentNameAsync([FromRoute] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    _logger.LogWarning("Invalid name entered");
                    return BadRequest();
                }
                var student = await _studentRepository.GetAsync(student => student.StudentName.ToLower().Contains(name.ToLower())); //we change the method from getname to get
                if (student is null)
                {
                    _logger.LogWarning("Student does not existing");
                    return NotFound();
                }
                _aPIResponse.Data = _mapper.Map<StudentDto>(student);  //OLAIDE don't ever IN YOUR LIFE MAP in Controller. Do all in the SERVICE
                _aPIResponse.Status = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }

        [HttpPost]
        [Route("create", Name = "CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CreateStudentAsync([FromBody] StudentDto dto)
        {
            try
            {
                //if (studentDTO.AdmissionDate < DateTime.Now)
                //{
                //    ModelState.AddModelError("Admission Error", "Admission date must be greater than or equal to today's date");
                //    return BadRequest(ModelState);
                //}
                if (!ModelState.IsValid)
                    return BadRequest();

                var student = _mapper.Map<Data.Student>(dto);
                var studentAfterCreation = await _studentRepository.CreateAsync(student);

                dto.Id = studentAfterCreation.Id;

                _aPIResponse.Data = dto;
                _aPIResponse.Status = true;
                _aPIResponse.StatusCode = HttpStatusCode.Created;
                //return CreatedAtRoute(routeName: "GetStudentById", routeValues: new { id = dto.Id }, value: dto);
                return CreatedAtRoute(routeName: "GetStudentById", routeValues: new { id = dto.Id }, value: _aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteStudentAsync([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var student = await _studentRepository.GetAsync(student => student.Id == id);

                if (student is null)
                {
                    return NotFound("Student ID can not be found");
                }

                await _studentRepository.DeleteAsync(student);

                _aPIResponse.Data = true;
                _aPIResponse.Status = true;
                _aPIResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_aPIResponse);
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateStudentAsync([FromBody] StudentDto studentDTO)
        {
            try
            {
                if (studentDTO.Id <= 0 || studentDTO is null)
                    return BadRequest();

                var student = _mapper.Map<Data.Student>(studentDTO);

                if (student == null)
                    return NotFound();

                await _studentRepository.UpdateAsync(student);
                return NoContent();
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }

        [HttpPatch]
        [Route("partialUpdate/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PartialUpdateStudentAsync(int id, [FromBody] JsonPatchDocument<StudentDto> patchDocument)
        {
            try
            {
                if (patchDocument is null || id <= 0)
                    return BadRequest();

                var student = await _studentRepository.GetAsync(student => student.Id == id);
                if (student == null)
                    return NotFound();

                var st = _mapper.Map<StudentDto>(student);

                patchDocument.ApplyTo(st, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedStudent = _mapper.Map<Data.Student>(st);
                await _studentRepository.UpdateAsync(updatedStudent);

                return NoContent();
            }
            catch (Exception ex)
            {
                _aPIResponse.Errors.Add(ex.Message);
                _aPIResponse.StatusCode = HttpStatusCode.InternalServerError;
                _aPIResponse.Status = false;
                return _aPIResponse;
            }
        }
    }
}
