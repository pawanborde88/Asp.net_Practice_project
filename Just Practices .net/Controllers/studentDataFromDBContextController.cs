using EMPManagement.Data;
using Just_Practices_.net.Models;
using Just_Practices_.net.Models.Domain_Model;
using Just_Practices_.net.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Just_Practices_.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDataFromDBContextController : ControllerBase
    {
        private readonly EMPManagementDBContext _empDbContext;

        public StudentDataFromDBContextController(EMPManagementDBContext empDbContext)
        {
            _empDbContext = empDbContext;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _empDbContext.Students.ToList();

            var studentDto = new List<StudentDTOsClass>();

            foreach (var student in students)
            {
                studentDto.Add(new StudentDTOsClass
                {
                    student_id = student.student_id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    DateOfBirth = student.DateOfBirth,
                    StudentPhoto = student.StudentPhoto,
                    StudentSchoolName = student.StudentSchoolName,
                    student_scholdership_amount = student.student_scholdership_amount
                });
            }

            return Ok(studentDto);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _empDbContext.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }



        //Post -create a new students
        [HttpPost]
        public IActionResult CreateANewStudents([FromBody] AddStudentRequestClass addNewStudentPayload)
        {
            try
            {
                var domainStudent = new Student
                {
                    FirstName = addNewStudentPayload.FirstName,
                    LastName = addNewStudentPayload.LastName,
                    Email = addNewStudentPayload.Email,
                    DateOfBirth = addNewStudentPayload.DateOfBirth,
                    StudentPhoto = addNewStudentPayload.StudentPhoto,
                    StudentSchoolName = addNewStudentPayload.StudentSchoolName,
                    student_scholdership_amount = addNewStudentPayload.student_scholdership_amount
                };

                _empDbContext.Students.Add(domainStudent);
                _empDbContext.SaveChanges();

                var studentDto = new StudentDTOsClass
                {
                    student_id = domainStudent.student_id,
                    FirstName = domainStudent.FirstName,
                    LastName = domainStudent.LastName,
                    Email = domainStudent.Email,
                    DateOfBirth = domainStudent.DateOfBirth,
                    StudentPhoto = domainStudent.StudentPhoto,
                    StudentSchoolName = domainStudent.StudentSchoolName,
                    student_scholdership_amount = domainStudent.student_scholdership_amount
                };

                var response = new ApiResponse<StudentDTOsClass>
                {
                    StatusCode = 201,
                    Success = true,
                    Message = "Student created successfully.",
                    Data = studentDto
                };

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>
                {
                    StatusCode = 400,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Data = null
                };

                return BadRequest(response);
            }
        }




        //UPdate Student Data 

        [HttpPut("{id}")]
        public IActionResult UpdateStudentData(
    int id,
    [FromBody] updateStudentDPO updateStudentPayload)
        {
            try
            {
                var existingStudent = _empDbContext.Students.Find(id);

                if (existingStudent == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = $"Student with ID {id} not found.",
                        Data = null
                    });
                }

                // Update fields
                existingStudent.FirstName = updateStudentPayload.FirstName;
                existingStudent.LastName = updateStudentPayload.LastName;
                existingStudent.Email = updateStudentPayload.Email;
                existingStudent.DateOfBirth = updateStudentPayload.DateOfBirth;
                existingStudent.StudentPhoto = updateStudentPayload.StudentPhoto;
                existingStudent.StudentSchoolName = updateStudentPayload.StudentSchoolName;
                existingStudent.student_scholdership_amount = updateStudentPayload.student_scholdership_amount;

                _empDbContext.SaveChanges();

                var studentDto = new StudentDTOsClass
                {
                    student_id = existingStudent.student_id,
                    FirstName = existingStudent.FirstName,
                    LastName = existingStudent.LastName,
                    Email = existingStudent.Email,
                    DateOfBirth = existingStudent.DateOfBirth,
                    StudentPhoto = existingStudent.StudentPhoto,
                    StudentSchoolName = existingStudent.StudentSchoolName,
                    student_scholdership_amount = existingStudent.student_scholdership_amount
                };

                return Ok(new ApiResponse<StudentDTOsClass>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Student updated successfully.",
                    Data = studentDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Data = null
                });
            }
        }

        // Delete sudents

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var existingStudent = _empDbContext.Students.Find(id);
                if (existingStudent == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = $"Student with ID {id} not found.",
                        Data = null
                    });
                }
                _empDbContext.Students.Remove(existingStudent);
                _empDbContext.SaveChanges();
                return Ok(new ApiResponse<object>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Student deleted successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Data = null
                });
            }

        }
    }
}