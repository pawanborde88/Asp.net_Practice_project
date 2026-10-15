using EmployeeManagement.Models.DomainModel;

namespace EmployeeManagement.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}