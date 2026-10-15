using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task<Employee> CreateAsync(Employee employee);

        Task<Employee?> UpdateAsync(int id, Employee employee);

        Task<Employee?> DeleteAsync(int id);
        Task UpdateProfilePhotoAsync(int employeeId, string photoPath);
        Task<Employee?> GetEmployeeWithProfilePhotoAsync(int employeeId);
        Task RemoveProfilePhotoAsync(int employeeId);

        Task AddEmployeeDocumentsAsync(List<EmployeeDocument> documents);

Task<List<EmployeeDocument>> GetEmployeeDocumentsAsync(int employeeId);

Task DeleteEmployeeDocumentAsync(int documentId);
    }
}