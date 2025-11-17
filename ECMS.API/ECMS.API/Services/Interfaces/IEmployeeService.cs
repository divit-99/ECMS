using ECMS.API.DTOs.Employee;

namespace ECMS.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> AddEmployeeAsync(EmployeeSaveDto employee);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeSaveDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
