using EmployeeManagement.DTOs.Auth;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EmplyeeeManagement.Data;
namespace EmployeeManagement.Data
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(
            IAuthRepository authRepository,
            IJwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("login")]
        public IActionResult LoginGet()
        {
            return Ok(new
            {
                status = "Active",
                message = "Auth login endpoint is live! Send a POST request with your credentials to receive a JWT token.",
                method = "POST",
                payload = new
                {
                    username = "admin",
                    password = "admin123"
                }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var user = await _authRepository.AuthenticateAsync(
                request.Username,
                request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _jwtTokenService.GenerateToken(user);

            var response = new LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            };

            return Ok(response);
        }
    }
}