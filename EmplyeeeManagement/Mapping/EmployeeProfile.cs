using AutoMapper;
using EmployeeManagement.Domain;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.DTOs.Employee;

namespace EmployeeManagement.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();

            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}