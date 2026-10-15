using AutoMapper;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Models.DTOs.Employee;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IFileService _fileService;

        public EmployeeController(
       IEmployeeRepository employeeRepository,

       IMapper mapper,
       ILogger<EmployeeController> logger,
       IFileService fileService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
            _fileService = fileService;
        }
        [Authorize(Policy = "AllEmployees")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            _logger.LogInformation("Fetching all employees.");
            var employeeDtos = _mapper.Map<List<EmployeeReadDto>>(employees);

            return Ok(employeeDtos);
        }
        [Authorize(Roles = "Admin,HR,Employee")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            _logger.LogInformation("Fetching employee with ID: {EmployeeId}", id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeReadDto>(employee);

            return Ok(employeeDto);
        }

[Authorize(Policy = "AdminOrHR")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto createDto)
        {
            var employee = _mapper.Map<Employee>(createDto);

            employee = await _employeeRepository.CreateAsync(employee);

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employee);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation(
         "Creating employee. EmployeeCode: {EmployeeCode}, Name: {EmployeeName}, DepartmentId: {DepartmentId}",
         employee.EmployeeCode,
         employee.FirstName,
         employee.DepartmentId); return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = employeeReadDto.EmployeeId },
                employeeReadDto
            );
        }
        [Authorize(Policy = "AdminOrHR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
       int id,
       [FromBody] EmployeeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = _mapper.Map<Employee>(updateDto);

            var updatedEmployee = await _employeeRepository.UpdateAsync(id, employee);

            if (updatedEmployee == null)
            {
                return NotFound();
            }
            _logger.LogInformation(
                "Updating employee. EmployeeId: {EmployeeId}, Salary: {Salary}",
                id,
                employee.Salary);
            var employeeReadDto = _mapper.Map<EmployeeReadDto>(updatedEmployee);

            return Ok(employeeReadDto);
        }
       [Authorize(Policy = "AdminOnly")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.DeleteAsync(id);
            _logger.LogInformation(
               "Deleting employee. EmployeeId: {EmployeeId}",
               id);
            if (employee == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        [Authorize(Policy = "HRDepartment")]
        [HttpGet("hr-data")]
        public IActionResult GetHRData()
        {
            return Ok("HR Department Only");
        }
        [Authorize]
        [HttpPost("upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadEmployeePhotoDto model)
        {
            if (model.Photo == null || model.Photo.Length == 0)
                return BadRequest("Please select a file.");

            var fileName = await _fileService.UploadEmployeePhotoAsync(model.Photo);

            return Ok(new
            {
                Message = "Photo uploaded successfully.",
                FileName = fileName,
                Path = $"Uploads/Employees/{fileName}"
            });
        }

        [Authorize]
        [HttpPost("{employeeId}/upload-photo")]
        public async Task<IActionResult> UploadPhoto(
            int employeeId,
            [FromForm] UploadEmployeePhotoDto model)
        {
            if (model.Photo == null || model.Photo.Length == 0)
                return BadRequest("Please select a file.");

            var fileName = await _fileService.UploadEmployeePhotoAsync(model.Photo);

            var path = $"Uploads/Employees/{fileName}";

            await _employeeRepository.UpdateProfilePhotoAsync(employeeId, path);

            return Ok(new
            {
                Message = "Photo uploaded successfully.",
                ProfilePhoto = path
            });
        }
        [Authorize]
        [HttpGet("{employeeId}/photo")]
        public async Task<IActionResult> GetEmployeePhoto(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeWithProfilePhotoAsync(employeeId);

            if (employee == null)
                return NotFound("Employee not found.");

            if (string.IsNullOrEmpty(employee.ProfilePhotoPath))
                return NotFound("Profile photo not found.");

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                employee.ProfilePhotoPath);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Photo file does not exist.");

            var extension = Path.GetExtension(filePath).ToLower();

            var contentType = extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };

            return PhysicalFile(filePath, contentType);
        }
        [Authorize]
        [HttpDelete("{employeeId}/photo")]
        public async Task<IActionResult> DeletePhoto(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
                return NotFound("Employee not found.");

            if (string.IsNullOrEmpty(employee.ProfilePhotoPath))
                return BadRequest("Employee has no profile photo.");

            await _fileService.DeleteEmployeePhotoAsync(employee.ProfilePhotoPath);

            await _employeeRepository.RemoveProfilePhotoAsync(employeeId);

            return Ok(new
            {
                Message = "Profile photo deleted successfully."
            });
        }

        [Authorize]
        [HttpPost("{employeeId}/upload-documents")]
        public async Task<IActionResult> UploadDocuments(
    int employeeId,
    [FromForm] UploadEmployeeDocumentsDto model)
        {
            if (model.Files == null || !model.Files.Any())
            {
                return BadRequest("Please select at least one file.");
            }

            var uploadedFiles =
                await _fileService.UploadEmployeeDocumentsAsync(model.Files);

            return Ok(new
            {
                Message = "Documents uploaded successfully.",
                Files = uploadedFiles
            });
        }
    }
}