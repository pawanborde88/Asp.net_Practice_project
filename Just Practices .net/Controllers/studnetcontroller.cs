using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Just_Practices_.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studnetcontroller : ControllerBase
    {
        [HttpGet]
        //https://localhost:7130/api/studnet
        public IActionResult GetAllStudents()
        {
            string[] studentNames =
 {
    "Pawan",
    "Sanket",
    "Rohit",
    "Akash",
    "Swapnil",
    "Pratik",
    "Saurabh",
    "Ajinkya",
    "Mahesh",
    "Madhusudan",
    "Amol",
    "Nilesh",
    "Ganesh",
    "Vishal",
    "Shubham",
    "Omkar",
    "Tushar",
    "Kunal",
    "Abhijit",
    "Yogesh",
    "Sachin",
    "Vikas",
    "Aniket",
    "Rushikesh",
    "Harshal",
    "Tejas",
    "Chetan",
    "Nitin",
    "Santosh",
    "Deepak",
    "Ravindra",
    "Shreyas",
    "Sameer",
    "Sstudent_iddhesh",
    "Aditya",
    "Atharva",
    "Parth",
    "Mangesh",
    "Dhananjay",
    "Bhushan",
    "Chaitanya",
    "Ketan",
    "Umesh",
    "Vaibhav",
    "Sunil",
    "Milind",
    "Rajesh",
    "Vivek",
    "Ashish",
    "Anand"
};

            return Ok(studentNames);
        }
    }
}
