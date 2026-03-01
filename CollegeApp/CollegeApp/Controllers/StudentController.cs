using CollegeApp.Model;
using CollegeApp.Model.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("all", Name = "GettAllTheStudents")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<StudentDTO>> GetStudents()
        {
            _logger.LogInformation("GetStudents endpoint has started");

            var students = new List<StudentDTO>();
            foreach (var item in CollegeRespository.GetAllStudents())
            {
                StudentDTO student = new StudentDTO()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    Email = item.Email,
                    Address = item.Address
                };
                students.Add(student);
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudent(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID requested: {userId}", id);
                return BadRequest();
            }

            var student = CollegeRespository.GetStudent(id);
            if (student is null)
            {
                _logger.LogInformation("User with ID: {userID} not found", id);
                return NotFound();
            }

            StudentDTO studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest();

            var student = CollegeRespository.GetStudentByName(name);
            if (student is null)
                return NotFound();

            StudentDTO studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("create", Name = "CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateStudent([FromBody] StudentDTO studentDTO)
        {


            //if (studentDTO.AdmissionDate < DateTime.Now)
            //{
            //    ModelState.AddModelError("Admission Error", "Admission date must be greater than or equal to today's date");
            //    return BadRequest(ModelState);
            //}

            var student = new Student()
            {
                StudentName = studentDTO.StudentName,
                Email = studentDTO.Email,
                Address = studentDTO.Address
            };
            CollegeRespository.AddStudent(student);
            return CreatedAtRoute(routeName: "GetStudentById", routeValues: new { id = student.Id }, value: studentDTO);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRespository.DeleteStudent(id);
            if (student is true)
            {
                return Ok("Deleted Successfully");
            }
            else return NotFound("Student ID can not be found");
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateStudent([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO.Id <= 0)
                return BadRequest();

            var student = CollegeRespository.UpdateStudent(studentDTO.Id);
            if (student is not null)
            {
                student.StudentName = studentDTO.StudentName;
                student.Email = studentDTO.Email;
                student.Address = studentDTO.Address;
            }
            return NoContent();
        }

        [HttpPatch]
        [Route("partialUpdate/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PartialUpdateStudent(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument is null || id <= 0)
                return BadRequest();

            var student = CollegeRespository.UpdateStudent(id);
            if (student is null)
                return NotFound();
            
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
            };

            patchDocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            student.Id = studentDTO.Id;
            student.StudentName = studentDTO.StudentName;
            student.Email = studentDTO.Email;
            student.Address = studentDTO.Address;

            return NoContent();
        }
    }
}
