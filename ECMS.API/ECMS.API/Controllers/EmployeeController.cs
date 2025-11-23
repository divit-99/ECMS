using ECMS.API.DTOs.Employee;
using ECMS.API.Exceptions;
using ECMS.API.Filters;
using ECMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(
                                    [FromQuery] int pageNumber = 1,
                                    [FromQuery] int pageSize = 10,
                                    [FromQuery] string? search = null)
        {
            var (data, totalCount) = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize, search);

            return Ok(new
            {
                data,
                pageNumber,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                throw new NotFoundException($"Employee with ID '{id}' NOT found.");
            
            return employee;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateEmployeeEmailFilter))]
        public async Task<ActionResult> Create(EmployeeSaveDto employeeSaveDto)
        {
            var createdEmployee = await _employeeService.AddEmployeeAsync(employeeSaveDto);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateEmployeeEmailFilter))]
        public async Task<ActionResult> Update(int id, EmployeeSaveDto employeeDto)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            if (updatedEmployee == null)
                throw new NotFoundException($"Employee with ID '{id}' NOT found.");

            return Ok(new
            {
                message = "Employee details updated successfully",
                data = updatedEmployee
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);

            if (!deleted)
                throw new NotFoundException($"Employee with ID '{id}' NOT found.");

            return Ok(new { message = $"Employee with ID '{id}' DELETED successfully." });
        }
    }

}
