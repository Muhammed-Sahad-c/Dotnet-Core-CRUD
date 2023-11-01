using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManagementSystem.Models;
using System.Reflection;
using System.Text.Json;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        StudentDataAccessLayer studentDataAccessLayer = null;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
            studentDataAccessLayer = new StudentDataAccessLayer();
        }

        // POST /student/addstudent
        [HttpPost]
        [Route("/student/addstudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> AddStudent([FromBody]Student student)
        {
            try
            {
                if (student == null)
                    return BadRequest("Body Not Found");

                var addedStudent = studentDataAccessLayer.AddStudent(student);

                if (addedStudent != null)
                    return CreatedAtAction("AddStudent", addedStudent);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add student");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding student: " + ex.Message);
            }
        }

        // GET /student/getallstudents
        [HttpGet]
        [Route("/student/getallstudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult getAllStudents() {
            try
            {
                var students = studentDataAccessLayer.GetAllStudent();

                if (students != null)
                {
                    if (students.Any())
                        return Ok(students);
                    else
                        return NotFound("No students found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Null response received from data layer");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT /student/edit/
        [HttpPut]
        [Route("/student/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult EditStudent([FromBody] Student student)
        {
            try
            {
                if (student == null)
                    return BadRequest("Body Not Found");

                studentDataAccessLayer.EditStudent(student);
                return Ok("Successfully updated student");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        // DELETE /student/delete/3
        [HttpDelete]
        [Route("/student/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid ID");

                string message = studentDataAccessLayer.DeleteStudent(id);

                if (message == "Student not found")
                    return NotFound("Student not found");
                else if (message == "Success")
                    return Ok("Student deleted successfully");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting student");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error occurred: {ex.Message}");
            }
        }

        // GET /student/details/3
        [HttpGet]
        [Route("/student/details/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetStudentDetails(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid ID");

                var studentDetails = studentDataAccessLayer.GetStudentDetails(id);

                if (studentDetails != null)
                    return Ok(studentDetails);
                else
                    return NotFound("Student not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error occurred: {ex.Message}");
            }
        }
    }
}
