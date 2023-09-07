using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Employee
    {
        [Key] public int employeeId { get; set; }
        public string employeeName { get; set;}
        public string employeeCode { get; set;}

        public int employeeSalary { get; set;}

        public int supervisorId { get; set;}
    }
}
