using ECMS.API.DTOs.Employee;
using ECMS.API.Models;

namespace ECMS.API.Mappings
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToDto(this Employee e)
        {
            return new EmployeeDto
            {
                Id = e.ID,
                FullName = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                JobTitle = e.JobTitle,
                CompanyId = e.CompanyID,
                CompanyName = e.Company?.CompanyName ?? string.Empty,
                IsActive = e.IsActive
            };
        }

        public static Employee ToEntity(this EmployeeSaveDto dto)
        {
            return new Employee
            {
                Name = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                JobTitle = dto.JobTitle,
                CompanyID = dto.CompanyId,
                IsActive= dto.IsActive
            };
        }

        public static void UpdateFromDto(this Employee e, EmployeeSaveDto dto)
        {
            e.Name = dto.FullName;
            e.Email = dto.Email;
            e.Phone = dto.Phone;
            e.JobTitle = dto.JobTitle;
            e.CompanyID = dto.CompanyId;
            e.IsActive = dto.IsActive;
        }
    }
}
