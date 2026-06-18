using System;
using System.ComponentModel.DataAnnotations;

namespace Just_Practices_.net.Models.Domain_Model
{
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Key]
        public int student_id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? StudentPhoto { get; set; }

        public string? StudentSchoolName { get; set; }

        public decimal? student_scholdership_amount { get; set; }
    }
}
