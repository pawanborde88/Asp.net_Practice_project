using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Just_Practices_.net.Models.Domain_Model.EmpManagementNew
{
    public class Employee
    {
        [Key]
        [Column("emp_id")]
        public int EmpId { get; set; }

        [Column("employee_code")]
        public string EmployeeCode { get; set; } = string.Empty;

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("EmergencyContactPhone")]
        public string EmergencyContactPhone { get; set; } = string.Empty;

        [Column("department")]
        public string Department { get; set; } = string.Empty;

        [Column("designation")]
        public string Designation { get; set; } = string.Empty;

        [Column("salary")]
        public decimal Salary { get; set; }

        [Column("joining_date")]
        public DateTime JoiningDate { get; set; }
        [Column("pan_number")]
        public string? PANNumber { get; set; }
        [Column("aadhar_number")]
        public string? AadharNumber { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}