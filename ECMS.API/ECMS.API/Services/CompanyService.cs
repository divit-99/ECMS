using ECMS.API.DTOs.Company;
using ECMS.API.Mappings;
using ECMS.API.Repositories.Interfaces;
using ECMS.API.Services.Interfaces;

namespace ECMS.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(c => c.ToDto());
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            return company?.ToDto();
        }

        public async Task<CompanyDto> AddCompanyAsync(CompanySaveDto dto)
        {
            var entity = dto.ToEntity();
            await _companyRepository.AddAsync(entity);
            return entity.ToDto();
        }

        public async Task<bool> UpdateCompanyAsync(int id, CompanySaveDto dto)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
                return false;

            company.UpdateFromDto(dto);

            await _companyRepository.UpdateAsync(company);
            return true;
        }

        public Task<bool> DeleteCompanyAsync(int id)
            => _companyRepository.DeleteAsync(id);
    }
}
