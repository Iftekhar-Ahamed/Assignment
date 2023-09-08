using Assignment.Dtos;
using Assignment.Models;

namespace Assignment.Services
{
    public interface IEmployeeService
    {
        Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto);
        Task<string> GetEmployeeHirarchyAsync(int employeeId);
        Task<List<EmployeeAttendenceReportDto>> GetAllEmployeeAttendenceMonthlyReport(int month, int year);
        Task<List<Employee>> GetIthHighestSalariedEmployeeAsync(int ith,string sort,int take);
        Task<List<Employee>> GetAllEmployeeSalaryAsync(string sort, bool anyAbsent);
    }
}
