using System.ComponentModel.DataAnnotations;

namespace Just_Practices_.net.Models.Domain_Model
{
    public class Department
    {
        [Key]
        public int student_id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
