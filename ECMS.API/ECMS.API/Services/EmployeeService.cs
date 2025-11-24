using ECMS.API.DTOs.Employee;
using ECMS.API.Mappings;
using ECMS.API.Repositories.Interfaces;
using ECMS.API.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ECMS.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
        }

        //public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        //{
        //    var employees = await _employeeRepository.GetAllAsync();
        //    return employees.Select(e => e.ToDto());
        //}

        public async Task<(IEnumerable<EmployeeDto> Data, int TotalCount)> GetAllEmployeesAsync
        (
            int pageNumber,
            int pageSize,
            string? search,
            string sortBy,
            string sortDir
        )
        {
            var employees = await _employeeRepository.GetAllAsync(pageNumber, pageSize, search, sortBy, sortDir);
            var count = await _employeeRepository.GetCountAsync(search);

            var dtos = employees.Select(e => e.ToDto());
            return (dtos, count);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee?.ToDto();
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeSaveDto dto)
        {
            await ValidateCompanyAsync(dto.CompanyId);

            var entity = dto.ToEntity();

            await _employeeRepository.AddAsync(entity);
            return entity.ToDto();
        }

        public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, EmployeeSaveDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return null;

            await ValidateCompanyAsync(dto.CompanyId);

            employee.UpdateFromDto(dto);

            await _employeeRepository.UpdateAsync(employee);

            return employee.ToDto();
        }

        public Task<bool> DeleteEmployeeAsync(int id)
                            => _employeeRepository.DeleteAsync(id);

        private async Task ValidateCompanyAsync(int companyId)
        {
            if (companyId <= 0)
                throw new ValidationException("CompanyId MUST be greater than 0.");

            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company == null)
                throw new ValidationException($"Company with ID '{companyId}' does NOT exist.");
        }
    }
}
