using Microsoft.AspNetCore.Mvc;
using Assignment.Constants;
using Assignment.Dtos;
using Assignment.Services;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(IEmployeeService employeeService) {
           _employeeService = employeeService;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            if (await _employeeService.UpdateEmployeeAsync(updateEmployeeDto))
            {
                return Ok("Employee updated successfully");
            }
            return NotFound("Employee not updated");
        }

        [HttpGet]
        [Route("GetThirdSalariedEmployee")]
        public async Task<IActionResult> GetThirdHighestSalariedEmployee()
        {
            return Ok(await _employeeService.GetIthHighestSalariedEmployeeAsync(3,SortDirection.Dscending,1));
        }

        [HttpGet]
        [Route("GetHierarchy")]
        public async Task<IActionResult> GetHierarchy([FromQuery] int employeeId)
        {
            return Ok(await _employeeService.GetEmployeeHirarchyAsync(employeeId));
        }

        [HttpGet]
        [Route("MonthlyReport")]
        public async Task<IActionResult> GetAllEmployeeAttendenceMonthlyReport([FromQuery] int month, int year)
        {
            return Ok(await _employeeService.GetAllEmployeeAttendenceMonthlyReport(month, year));
        }

        [HttpGet]
        [Route("GetAllEmployeeSalaryNotAnyAbsentRecordDSC")]
        public async Task<IActionResult> GetAllEmployeeSalaryAsync()
        {
            return Ok(await _employeeService.GetAllEmployeeSalaryAsync(SortDirection.Dscending,true));
        }
    }
}
