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

        /// <summary>
        /// Retrieves all employees with pagination, sorting, and optional search.
        /// </summary>
        /// <param name="pageNumber">Page index starting from 1.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="search">Optional search keyword.</param>
        /// <param name="sortBy">Sort field.</param>
        /// <param name="sortDir">Sort direction (asc/desc).</param>
        /// <returns>Paginated list of employees.</returns>
        /// <response code="200">Employees retrieved successfully.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null,
            string sortBy = "name",
            string sortDir = "asc"
        )
        {
            var (data, totalCount) = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize, search, sortBy, sortDir);

            return Ok(new
            {
                data,
                pageNumber,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        /// <summary>
        /// Retrieves a single employee by ID.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <returns>Employee details.</returns>
        /// <response code="200">Employee found.</response>
        /// <response code="404">Employee not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                throw new NotFoundException($"Employee with ID '{id}' NOT found!");

            return employee;
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employeeSaveDto">Employee details.</param>
        /// <returns>Newly created employee.</returns>
        /// <response code="201">Employee created successfully.</response>
        /// <response code="400">Validation error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), 201)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidateEmployeeEmailFilter))]
        public async Task<ActionResult> Create(EmployeeSaveDto employeeSaveDto)
        {
            var createdEmployee = await _employeeService.AddEmployeeAsync(employeeSaveDto);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="employeeDto">Updated employee details.</param>
        /// <returns>Update result message.</returns>
        /// <response code="200">Employee updated successfully.</response>
        /// <response code="404">Employee not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ServiceFilter(typeof(ValidateEmployeeEmailFilter))]
        public async Task<ActionResult> Update(int id, EmployeeSaveDto employeeDto)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);

            if (updatedEmployee == null)
                throw new NotFoundException($"Employee with ID '{id}' NOT found!");

            return Ok(new
            {
                message = "Employee details updated successfully!",
                data = updatedEmployee
            });
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <returns>Delete confirmation message.</returns>
        /// <response code="200">Employee deleted.</response>
        /// <response code="404">Employee not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);

            if (!deleted)
                throw new NotFoundException($"Employee with ID '{id}' NOT found!");

            return Ok(new { message = $"Employee deleted successfully!" });
        }
    }

}
