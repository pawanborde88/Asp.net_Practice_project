using EmployeeManagement.Domain.Models;
using EmployeeManagement.Repository.Interfaces;
using EmplyeeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace EmployeeManagement.Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all employees from database.");

            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Address)
                .ToListAsync();
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            _logger.LogInformation(
    "Retrieving employee with ID: {EmployeeId}",
    id);
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {

            // Validate that the department exists
            var departmentExists = await _context.Departments
                .AnyAsync(d => d.DepartmentId == employee.DepartmentId);

            if (!departmentExists)
            {
                throw new InvalidOperationException($"Department with ID {employee.DepartmentId} does not exist.");
            }

            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();
            _logger.LogInformation(
    "Creating employee with code: {EmployeeCode}",
    employee.EmployeeCode);
            return employee;
        }

        public async Task<Employee?> UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.EmployeeCode = employee.EmployeeCode;
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.DepartmentId = employee.DepartmentId;

            await _context.SaveChangesAsync();
            _logger.LogInformation(
    "Employee updated successfully with ID: {EmployeeId}",
    employee.EmployeeId);
            return existingEmployee;
        }

        public async Task<Employee?> DeleteAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return null;
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
            _logger.LogInformation(
    "Employee deleted successfully with ID: {EmployeeId}",
    id);
            return employee;
        }
        public async Task UpdateProfilePhotoAsync(int employeeId, string photoPath)
        {
            var employee = await _context.Employees.FindAsync(employeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            employee.ProfilePhotoPath = photoPath;

            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetEmployeeWithProfilePhotoAsync(int employeeId)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }
        public async Task RemoveProfilePhotoAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);

            if (employee == null)
                throw new Exception("Employee not found.");

            employee.ProfilePhotoPath = null;

            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeeDocumentsAsync(
            List<EmployeeDocument> documents)
        {
            await _context.EmployeeDocuments.AddRangeAsync(documents);

            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeDocument>> GetEmployeeDocumentsAsync(int employeeId)
        {
            return await _context.EmployeeDocuments
                .Where(d => d.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task DeleteEmployeeDocumentAsync(int documentId)
        {
            var document = await _context.EmployeeDocuments.FindAsync(documentId);

            if (document != null)
            {
                _context.EmployeeDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}