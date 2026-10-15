using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Domain.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("employee_code")]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(15)]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("salary", TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("department_id")]
        public int DepartmentId { get; set; }

        [Column("profile_photo_path")]
        public string? ProfilePhotoPath { get; set; }

        // Navigation Property (Many-to-One)
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        // Navigation Property (One-to-One)
        public Address? Address { get; set; }
        public ICollection<EmployeeDocument> Documents { get; set; }
    = new List<EmployeeDocument>();
    }
}