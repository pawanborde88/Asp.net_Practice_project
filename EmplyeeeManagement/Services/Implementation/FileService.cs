using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Services.Implementation
{
    public class FileService : IFileService
    {
        public async Task<string> UploadEmployeePhotoAsync(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new Exception("Only JPG, JPEG and PNG files are allowed.");

            const long maxFileSize = 2 * 1024 * 1024;

            if (file.Length > maxFileSize)
                throw new Exception("Maximum file size allowed is 2 MB.");

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                "Employees");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }

        public async Task DeleteEmployeePhotoAsync(string photoPath)
        {
            var fullPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                photoPath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            await Task.CompletedTask;
        }

        public async Task<List<string>> UploadEmployeeDocumentsAsync(List<IFormFile> files)
        {
            var uploadedFiles = new List<string>();

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                "Documents");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                var extension = Path.GetExtension(file.FileName);

                var fileName = $"{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                uploadedFiles.Add($"Uploads/Documents/{fileName}");
            }

            return uploadedFiles;
        }
    }
}