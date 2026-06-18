using AutoMapper;
using EMPManagement.Data;
using Just_Practices_.net.Mapping;
using Just_Practices_.net.Models;
using Just_Practices_.net.Models.Domain_Model;
using Just_Practices_.net.Models.Domain_Model.EmpManagementNew;
using Just_Practices_.net.Models.DTOs;
using Just_Practices_.net.Models.DTOs.CreateEmpDataDTO;
using Just_Practices_.net.Models.DTOs.EmpDataDTO;
using Just_Practices_.net.Repository_Pattern;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Just_Practices_.net.Controllers.EMP_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpManagementNewController : ControllerBase
    {
        private readonly EMPManagementDBContext _empDbContext;
        private readonly IAllEmployess _employeeRepository;
        private readonly IMapper _mapper;

        public EmpManagementNewController(
            EMPManagementDBContext empDbContext,
            IAllEmployess employeeRepository, IMapper mapper)
        {
            _empDbContext = empDbContext;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        #region Create Multiple Employees
        [HttpPost("add_new_employee")]

        public async Task<IActionResult> CreateEmployees(
            [FromBody] List<createEmpDateRequiestClass> employees)
        {
            try
            {
                if (employees == null || !employees.Any())
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Employee list is required.",
                        Data = null
                    });
                }

                var employeeEntities = new List<Employee>();

                foreach (var emp in employees)
                {
                    bool employeeCodeExists = await _empDbContext.Employees
                        .AnyAsync(x => x.EmployeeCode == emp.EmployeeCode);

                    if (employeeCodeExists)
                    {
                        return Conflict($"Employee Code '{emp.EmployeeCode}' already exists.");
                    }

                    var employee = _mapper.Map<Employee>(emp);

                    employee.CreatedAt = DateTime.UtcNow;

                    employeeEntities.Add(employee);
                }

                await _empDbContext.Employees.AddRangeAsync(employeeEntities);
                await _empDbContext.SaveChangesAsync();

                return Ok(new ApiResponse<object>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = $"{employeeEntities.Count} employees created successfully.",
                    Data = employeeEntities.Select(x => new
                    {
                        x.EmpId,
                        x.EmployeeCode,
                        FullName = $"{x.FirstName} {x.LastName}",
                        x.Email,
                        x.PhoneNumber,
                        x.EmergencyContactPhone,
                        x.Department,
                        x.Designation,
                        x.Salary,
                        x.JoiningDate,
                        x.PANNumber,
                        x.AadharNumber,
                        x.IsActive,
                        x.CreatedAt
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

        #endregion

        #region Get All Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();

                return Ok(new ApiResponse<List<EmpDataDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Employees fetched successfully.",
                    Data = employees
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }

        #endregion

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = $"Employee with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<EmpDataDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Employee fetched successfully.",
                    Data = employee
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Data = ex.Message
                });
            }
        }


        // Delete the Employee Data
        [HttpDelete("delete_employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.DeleteAsync(id);

                if (employee == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = $"Employee with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Employee deleted successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }




        //UPdate emplyeee Data 
        [HttpPut("update_employee/{id}")]
        public async Task<IActionResult> UpdateEmployeeData(
      int id,
      [FromBody] EmpDataDTO updateEmployeePayload)
        {
            try
            {
                var employee = await _employeeRepository.UpdateAsync(id, updateEmployeePayload);

                if (employee == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = $"Employee with ID {id} not found.",
                        Data = null
                    });
                }

                var employeeDto = new EmpDataDTO
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
                };

                return Ok(new ApiResponse<EmpDataDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Employee updated successfully.",
                    Data = employeeDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = 400,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}
