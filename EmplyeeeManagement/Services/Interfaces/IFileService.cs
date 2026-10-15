using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadEmployeePhotoAsync(IFormFile file);
        Task DeleteEmployeePhotoAsync(string photoPath);
        Task<List<string>> UploadEmployeeDocumentsAsync(List<IFormFile> files);
    }
}