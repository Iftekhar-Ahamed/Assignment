using Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAttendenceController : ControllerBase
    {
        private readonly EmployeeContext employeeContext;
        public EmployeeAttendenceController(EmployeeContext _employeeContext)
        {
            this.employeeContext = _employeeContext;
        }
    }
}
