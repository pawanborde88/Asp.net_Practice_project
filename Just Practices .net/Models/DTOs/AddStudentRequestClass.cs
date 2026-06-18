namespace Just_Practices_.net.Models.DTOs
{
    public class AddStudentRequestClass
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StudentPhoto { get; set; }
        public string? StudentSchoolName { get; set; }
        public decimal? student_scholdership_amount { get; set; }
    }
}
