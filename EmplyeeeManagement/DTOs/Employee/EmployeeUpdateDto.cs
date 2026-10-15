namespace EmployeeManagement.DTOs.Employee
{
    public class EmployeeUpdateDto
    {
        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
    }
}