using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories
{
    public class EmployeeAttendenceRepository : IEmployeeAttendenceRepository
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeAttendenceRepository(EmployeeContext context)
        {
            _employeeContext = context;
        }
        public Task<bool> IsAbsentAnyDay(Employee employee)
        {
            var results = _employeeContext.EmployeeAttendances
                          .Where(entity => entity.employeeId == employee.EmployeeId)
                          .Where(entity => entity.isAbsent == 1)
                          .FirstOrDefault();
            return Task.FromResult(results == null);
        }
        public async Task<List<EmployeeAttendance>> GetMonthlyAttendenceEmployeeAsync(int id, string month)
        {
            var attedanceList = await _employeeContext.EmployeeAttendances.Where(entity => entity.employeeId == id)
                .Where(entity => entity.attendanceDate.StartsWith(month)).ToListAsync();
            return attedanceList;
        }
    }
}
