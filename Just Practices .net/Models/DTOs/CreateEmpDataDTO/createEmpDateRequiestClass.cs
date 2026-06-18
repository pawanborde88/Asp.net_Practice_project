using System.ComponentModel.DataAnnotations.Schema;

namespace Just_Practices_.net.Models.DTOs.CreateEmpDataDTO
{
    public class createEmpDateRequiestClass
    {
        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string EmergencyContactPhone { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public string? PANNumber { get; set; }

        public string? AadharNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
