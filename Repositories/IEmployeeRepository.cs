using Assignment.Models;

namespace Assignment.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeById(int employeeId);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<Employee> AddEmployee(Employee employee);
        Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int batchSize);
        Task<bool> IsExistEmployeeCodeAsync(string employeeCode);
        Task<Employee> GetEmployeeByEmployeeCodeAsync(string employeeCode);
        Task<List<Employee>> GetIthEmployeeBySalaryDscAsync(int Ith,int take);
        Task<List<Employee>> GetIthEmployeeBySalaryAscAsync(int Ith, int take);

    }
}
