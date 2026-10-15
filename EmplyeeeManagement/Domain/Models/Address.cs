using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Domain.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [Column("address_id")]
        public int AddressId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("state")]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("country")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [Column("pin_code")]
        public string PinCode { get; set; } = string.Empty;

        [Column("employee_id")]
        public int EmployeeId { get; set; }

        // Navigation Property
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }
    }
}