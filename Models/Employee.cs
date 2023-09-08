using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Employee
    {
        [Key] public int EmployeeId { get; set; }
        public string EmployeeName { get; set;}
        public string EmployeeCode { get; set;}

        public int EmployeeSalary { get; set;}

        public int SupervisorId { get; set;}
    }
}
