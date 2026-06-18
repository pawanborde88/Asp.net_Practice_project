using EMPManagement.Data;
using Just_Practices_.net.Models.Domain_Model.EmpManagementNew;
using Just_Practices_.net.Models.DTOs.EmpDataDTO;
using Microsoft.EntityFrameworkCore;

namespace Just_Practices_.net.Repository_Pattern
{
    public class EmployeeRepository : IAllEmployess
    {
        private readonly EMPManagementDBContext _empDbContext;

        public EmployeeRepository(EMPManagementDBContext empDbContext)
        {
            _empDbContext = empDbContext;
        }

        public async Task<List<EmpDataDTO>> GetAllAsync()
        {
            return await _empDbContext.Employees
                .AsNoTracking()
                .Select(employee => new EmpDataDTO
                {
                    EmpId = employee.EmpId,
                    EmployeeCode = employee.EmployeeCode,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    EmergencyContactPhone = employee.EmergencyContactPhone,
                    Department = employee.Department,
                    Designation = employee.Designation,
                    Salary = employee.Salary,
                    JoiningDate = employee.JoiningDate,
                    PANNumber = employee.PANNumber,
                    AadharNumber = employee.AadharNumber,
                    IsActive = employee.IsActive,
                    CreatedAt = employee.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<EmpDataDTO?> GetByIdAsync(int id)
        {
            return await _empDbContext.Employees
                .AsNoTracking()
                .Where(employee => employee.EmpId == id)
                .Select(employee => new EmpDataDTO
                {
                    EmpId = employee.EmpId,
                    EmployeeCode = employee.EmployeeCode,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    EmergencyContactPhone = employee.EmergencyContactPhone,
                    Department = employee.Department,
                    Designation = employee.Designation,
                    Salary = employee.Salary,
                    JoiningDate = employee.JoiningDate,
                    PANNumber = employee.PANNumber,
                    AadharNumber = employee.AadharNumber,
                    IsActive = employee.IsActive,
                    CreatedAt = employee.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _empDbContext.Employees.AddAsync(employee);
            await _empDbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> UpdateAsync(int id, EmpDataDTO dto)
        {
            var employee = await _empDbContext.Employees
                .FirstOrDefaultAsync(x => x.EmpId == id);

            if (employee == null)
            {
                return null;
            }

            employee.EmployeeCode = dto.EmployeeCode;
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.EmergencyContactPhone = dto.EmergencyContactPhone;
            employee.Department = dto.Department;
            employee.Designation = dto.Designation;
            employee.Salary = dto.Salary;
            employee.JoiningDate = dto.JoiningDate;
            employee.PANNumber = dto.PANNumber;
            employee.AadharNumber = dto.AadharNumber;
            employee.IsActive = dto.IsActive;

            await _empDbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> DeleteAsync(int id)
        {
            var employee = await _empDbContext.Employees
                .FirstOrDefaultAsync(x => x.EmpId == id);

            if (employee == null)
            {
                return null;
            }

            _empDbContext.Employees.Remove(employee);

            await _empDbContext.SaveChangesAsync();

            return employee;
        }
    }
}