using Assignment.Models;

namespace Assignment.Repositories
{
    public interface IEmployeeAttendenceRepository
    {
        Task<bool> IsAbsentAnyDay(Employee employee);
        Task<List<EmployeeAttendance>> GetMonthlyAttendenceEmployeeAsync(int id, string month);
    }
}
