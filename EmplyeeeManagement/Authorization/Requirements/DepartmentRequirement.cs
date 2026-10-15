using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Authorization.Requirements
{
    public class DepartmentRequirement : IAuthorizationRequirement
    {
        public string Department { get; }

        public DepartmentRequirement(string department)
        {
            Department = department;
        }
    }
}