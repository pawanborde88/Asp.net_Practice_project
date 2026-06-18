using AutoMapper;
using Just_Practices_.net.Models.Domain_Model.EmpManagementNew;
using Just_Practices_.net.Models.DTOs.CreateEmpDataDTO;
using Just_Practices_.net.Models.DTOs.EmpDataDTO;

namespace Just_Practices_.net.Mapping
{
    public class AutomapProfile : Profile
    {
        public AutomapProfile()
        {
            CreateMap<createEmpDateRequiestClass, Employee>();

            CreateMap<Employee, EmpDataDTO>();

            CreateMap<Employee, createEmpDateRequiestClass>();
        }
    }
}