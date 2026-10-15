using EmployeeManagement.Models.DomainModel;

namespace EmployeeManagement.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}