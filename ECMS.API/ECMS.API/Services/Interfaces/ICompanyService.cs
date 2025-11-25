using ECMS.API.DTOs.Company;

namespace ECMS.API.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto?> GetCompanyByIdAsync(int id);
        Task<CompanyDto> AddCompanyAsync(CompanySaveDto dto);
        Task<bool> UpdateCompanyAsync(int id, CompanySaveDto dto);
        Task<bool> DeleteCompanyAsync(int id);
    }
}
