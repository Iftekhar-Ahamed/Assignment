using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class EmployeeAttendance
    {
        [Key] public int employeeId { get; set; }
        public string attendanceDate { get; set;}
        public int isPresent { get; set; }
        public int isAbsent { get; set; }
        public int isOffday { get; set; }
    }
}
