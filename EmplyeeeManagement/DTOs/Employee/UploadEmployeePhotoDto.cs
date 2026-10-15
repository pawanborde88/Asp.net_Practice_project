using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Models.DTOs.Employee
{
    public class UploadEmployeePhotoDto
    {
        public IFormFile Photo { get; set; } = null!;
    }
}