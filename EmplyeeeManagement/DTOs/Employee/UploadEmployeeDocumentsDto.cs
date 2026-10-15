using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Models.DTOs.Employee
{
    public class UploadEmployeeDocumentsDto
    {
        public List<IFormFile> Files { get; set; } = new();
    }
}