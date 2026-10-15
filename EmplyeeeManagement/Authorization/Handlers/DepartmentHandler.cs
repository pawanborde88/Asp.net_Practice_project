using EmployeeManagement.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Authorization.Handlers
{
    public class DepartmentHandler
        : AuthorizationHandler<DepartmentRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DepartmentRequirement requirement)
        {
            var department =
                context.User.FindFirst("Department")?.Value;

            if (department == requirement.Department)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}