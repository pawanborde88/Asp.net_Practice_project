using EMPManagement.Data;
using Just_Practices_.net.Models.Domain_Model;
using Microsoft.AspNetCore.Mvc;

namespace Just_Practices_.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class STudentNewDataController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                var firstNames = new[]
                {
            "Pawan", "Sanket", "Rohit", "Akash", "Swapnil",
            "Pratik", "Saurabh", "Ajinkya", "Mahesh", "Madhusudan"
        };

                var lastNames = new[]
                {
            "Borde", "Patil", "Jadhav", "Shinde", "Deshmukh",
            "Kulkarni", "Pawar", "More", "Chavan", "Gaikwad"
        };

                var random = new Random();
                var usedIds = new HashSet<int>();

                var students = new List<Student>();

                for (int i = 0; i < 100; i++)
                {
                    int studentId;

                    do
                    {
                        studentId = random.Next(1000, 99999);
                    }
                    while (!usedIds.Add(studentId));

                    string firstName = firstNames[i % firstNames.Length];
                    string lastName = lastNames[i % lastNames.Length];

                    students.Add(new Student
                    {
                        student_id = studentId,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i + 1}@gmail.com",
                        DateOfBirth = new DateTime(
                            random.Next(1995, 2005),
                            random.Next(1, 13),
                            random.Next(1, 28)),
                        StudentPhoto = $"https://randomuser.me/api/portraits/men/{i % 100}.jpg"
                    });
                }

                var result = students.Select(s => new
                {
                    s.student_id,
                    s.FirstName,
                    s.LastName,
                    s.Email,
                    DateOfBirth = s.DateOfBirth.ToString("dd-MMMM-yyyy"),
                    s.StudentPhoto
                });

                return Ok(new
                {
                    success = true,
                    message = "Students fetched successfully.",
                    totalRecords = result.Count(),
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object?)null
                });
            }
        }

    }
}




