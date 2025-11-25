using ECMS.API.DTOs.Employee;

namespace ECMS.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDto> Data, int TotalCount)> GetAllEmployeesAsync(
            int pageNumber,
            int pageSize,
            string? search,
            string sortBy,
            string sortDir
        );
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> AddEmployeeAsync(EmployeeSaveDto employee);
        Task<EmployeeDto?> UpdateEmployeeAsync(int id, EmployeeSaveDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
