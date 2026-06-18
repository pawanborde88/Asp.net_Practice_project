using Just_Practices_.net.Models.Domain_Model;
using Just_Practices_.net.Models.Domain_Model.EmpManagementNew;
using Microsoft.EntityFrameworkCore;

namespace EMPManagement.Data
{
    public class EMPManagementDBContext : DbContext
    {
        public EMPManagementDBContext(DbContextOptions<EMPManagementDBContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        // public DbSet<Customer> Customers { get; set; }  // REMOVED or commented out
        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Employee> Employees { get; set; }

    }
}