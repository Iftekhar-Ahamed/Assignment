using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeRepository(EmployeeContext context)
        {
            _employeeContext = context;
        }

        public Task<Employee> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int batchSize)
        {
            var batch = await _employeeContext.Employees
                .OrderBy(entity => entity.EmployeeId)
                .Skip((pageNumber - 1) * batchSize)
                .Take(batchSize)
                .ToListAsync();

            return batch;
        }




        public async Task<Employee> GetEmployeeByEmployeeCodeAsync(string employeeCode)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            return await _employeeContext.Employees.FindAsync(employeeId);
        }

        public async Task<bool> IsExistEmployeeCodeAsync(string employeeCode)
        {
            var result =_employeeContext.Employees.Where(entity => entity.EmployeeCode == employeeCode).FirstOrDefault();
            return result!=null;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _employeeContext.Update(employee);
            var updateCount = await _employeeContext.SaveChangesAsync();
            return updateCount > 0;
        }

        public async Task<List<Employee>> GetIthEmployeeBySalaryDscAsync(int Ith, int take)
        {
            var batch = await _employeeContext.Employees
                .OrderByDescending(entity => entity.EmployeeSalary)
                .Skip(Ith)
                .Take(take)
                .ToListAsync();
            return batch;
        }
        public async Task<List<Employee>> GetIthEmployeeBySalaryAscAsync(int Ith, int take)
        {
            var batch = await _employeeContext.Employees
                .OrderBy(entity => entity.EmployeeSalary)
                .Skip(Ith)
                .Take(take)
                .ToListAsync();
            return batch;
        }
        
    }
}
