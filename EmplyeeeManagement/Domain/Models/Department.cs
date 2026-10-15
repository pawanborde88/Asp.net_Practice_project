using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Domain.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [Column("department_id")]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("department_name")]
        public string DepartmentName { get; set; } = string.Empty;

        [StringLength(250)]
        [Column("description")]
        public string? Description { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}