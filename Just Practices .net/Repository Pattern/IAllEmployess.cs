using Just_Practices_.net.Models.Domain_Model.EmpManagementNew;
using Just_Practices_.net.Models.DTOs.EmpDataDTO;

namespace Just_Practices_.net.Repository_Pattern
{
    public interface IAllEmployess
    {
        Task<List<EmpDataDTO>> GetAllAsync();

        Task<EmpDataDTO?> GetByIdAsync(int id);

        Task<Employee> CreateAsync(Employee employee);

        Task<Employee?> UpdateAsync(int id, EmpDataDTO dto);

        Task<Employee?> DeleteAsync(int id);
    }
}