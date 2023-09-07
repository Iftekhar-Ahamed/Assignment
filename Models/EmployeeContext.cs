using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext>options):base(options) 
        { 

        }

        public DbSet<Employee> Employees { get; set;}
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }

    }
}
