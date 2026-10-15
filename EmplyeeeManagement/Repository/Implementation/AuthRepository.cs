using EmployeeManagement.Models.DomainModel;
using EmployeeManagement.Repository.Interfaces;
using EmplyeeeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == username &&
                    x.PasswordHash == password &&
                    x.IsActive);
        }
    }
}