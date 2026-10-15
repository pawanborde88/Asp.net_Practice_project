using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs.Employee
{
    public class EmployeeCreateDto
    {
        [Required]
        [StringLength(10)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Range(1, 10000000)]
        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}