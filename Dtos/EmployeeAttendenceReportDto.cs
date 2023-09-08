namespace Assignment.Dtos
{
    public class EmployeeAttendenceReportDto
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string MonthName { get; set; } = string.Empty;
        public double PayableSalary { get; set; }
        public int TotalPresent { get; set; }
        public int TotalAbsent { get; set; }
        public int TotalOffDay { get; set; }

    }
}
