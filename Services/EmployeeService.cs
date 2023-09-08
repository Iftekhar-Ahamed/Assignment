using System;
using System.Diagnostics;
using System.Globalization;
using Assignment.Constants;
using Assignment.Dtos;
using Assignment.Models;
using Assignment.Repositories;

namespace Assignment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeAttendenceRepository _employeeAttendenceRepository;
        public EmployeeService(IEmployeeRepository employeeRepository,IEmployeeAttendenceRepository employeeAttendenceRepository)
        {
            _repository = employeeRepository;
            _employeeAttendenceRepository = employeeAttendenceRepository;
        }

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            if (await _repository.IsExistEmployeeCodeAsync(updateEmployeeDto.EmployeeCode))
            {
                return false;
            }
            var employee = await _repository.GetEmployeeById(updateEmployeeDto.EmployeeId);

            if (employee == null) return false;
            employee.EmployeeName = updateEmployeeDto.EmployeeName;
            employee.EmployeeCode = updateEmployeeDto.EmployeeCode;
            return await _repository.UpdateEmployeeAsync(employee);
        }

        public async Task<string> GetEmployeeHirarchyAsync(int employeeId)
        {
            var hirarchyNameList = new List<string>();
            var employee = await _repository.GetEmployeeById(employeeId);

            if (employee == null) return string.Empty;

            hirarchyNameList.Add(employee.EmployeeName);
            HashSet<int> employeeIds = new HashSet<int>();

            employeeIds.Add(employee.EmployeeId);

            while (employeeIds.Contains(employee.SupervisorId) == false)
            {
                var supervisor = await _repository.GetEmployeeById(employee.SupervisorId);

                if (supervisor == null) break;

                hirarchyNameList.Add(supervisor.EmployeeName);
                employeeIds.Add(supervisor.EmployeeId);
                employee = supervisor;
            }
            string employeeHiarechy = "";
            for (int i = 0; i < hirarchyNameList.Count; i++)
            {
                employeeHiarechy += hirarchyNameList[i];
                if (i != hirarchyNameList.Count - 1) employeeHiarechy += " -> ";
            }
            return employeeHiarechy;
        }

        public async Task<List<EmployeeAttendenceReportDto>> GetAllEmployeeAttendenceMonthlyReport(int monthNumber, int year)
        {

            string _month = year.ToString() +"-"+ (monthNumber.ToString().Length==1?"0"+ monthNumber.ToString(): monthNumber.ToString());
            string _monthName = "";
            double daysInMonth = DateTime.DaysInMonth(year, monthNumber);

            if (monthNumber >= 1 && monthNumber <= 12)
            {
                _monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber)+" - "+year.ToString();
            }
            else
            {
                return new List<EmployeeAttendenceReportDto>();
            }

            
            List<EmployeeAttendenceReportDto> employeesMonthlyAttendenceList = new List<EmployeeAttendenceReportDto>();

            int batchSize = 10;
            int pageNumber = 1;
            while (true)
            {
                var batch = await _repository.GetAllEmployeesAsync(pageNumber, batchSize);

                if (batch.Count == 0)
                {
                    break;
                }
                foreach (var record in batch)
                {

                    EmployeeAttendenceReportDto emp = new EmployeeAttendenceReportDto();
                    emp.EmployeeName = record.EmployeeName;
                    emp.MonthName = _monthName;

                    var attendanceList = await _employeeAttendenceRepository.GetMonthlyAttendenceEmployeeAsync(record.EmployeeId,_month);

                    if (attendanceList.Count == 0)
                    {
                        continue;
                    }
                    
                    foreach(var day in attendanceList)
                    {
                        emp.TotalPresent += day.isPresent;
                        emp.TotalAbsent += day.isAbsent;
                        emp.TotalOffDay += day.isOffday;
                    }

                    double perDaySalary = (double)(record.EmployeeSalary)/daysInMonth;
                    emp.PayableSalary = (double)(perDaySalary * (double)(emp.TotalPresent + emp.TotalOffDay));
                    employeesMonthlyAttendenceList.Add(emp);
                }
                pageNumber++;
            }

            return employeesMonthlyAttendenceList;
        }

        public async Task<List<Employee>> GetIthHighestSalariedEmployeeAsync(int ith,string sort,int take)
        {

            switch (sort)
            {
                case SortDirection.Ascending:
                    return await _repository.GetIthEmployeeBySalaryAscAsync(ith-1,take);
                case SortDirection.Dscending:
                    return await _repository.GetIthEmployeeBySalaryDscAsync(ith-1,take);
                default:
                    return new List<Employee>();
            }
        }

        public async Task<List<Employee>> GetAllEmployeeSalaryAsync(string sort,bool anyAbsent)
        {
            int batchSize = 10;
            int pageNumber = 1;
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                var batch = await _repository.GetAllEmployeesAsync(pageNumber, batchSize);
                
                
                if (batch.Count==0)
                {
                    break;
                }
                if (anyAbsent)
                {
                    foreach (var record in batch)
                    {
                        if (await _employeeAttendenceRepository.IsAbsentAnyDay(record))
                        {
                            employees.Add(record);
                        }
                    }
                }

                pageNumber++;
            }

            switch (sort)
            {
                case SortDirection.Ascending:
                    employees = employees.OrderBy(employee => employee.EmployeeSalary).ToList();
                    break;
                case SortDirection.Dscending:
                    employees = employees.OrderByDescending(employee => employee.EmployeeSalary).ToList();
                    break;
                default:
                    return new List<Employee>();

            }
            return employees;
        }
    }
}
