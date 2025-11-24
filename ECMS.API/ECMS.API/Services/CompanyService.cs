using ECMS.API.DTOs.Company;
using ECMS.API.Mappings;
using ECMS.API.Repositories.Interfaces;
using ECMS.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ECMS.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMemoryCache _cache;
        private const string CompanyCacheKey = "company_list";

        public CompanyService(ICompanyRepository companyRepository, IMemoryCache cache)
        {
            _companyRepository = companyRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            if (_cache.TryGetValue(CompanyCacheKey, out IEnumerable<CompanyDto> cachedCompanies))
                return cachedCompanies;

            var companies = await _companyRepository.GetAllAsync();
            var companyDtos = companies.Select(c => c.ToDto()).ToList();

            _cache.Set(CompanyCacheKey, companyDtos, TimeSpan.FromMinutes(30));

            return companyDtos;
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

            _cache.Remove(CompanyCacheKey);
            return entity.ToDto();
        }

        public async Task<bool> UpdateCompanyAsync(int id, CompanySaveDto dto)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
                return false;

            company.UpdateFromDto(dto);
            await _companyRepository.UpdateAsync(company);
            
            _cache.Remove(CompanyCacheKey);
            return true;
        }

        public Task<bool> DeleteCompanyAsync(int id)
        {
            _cache.Remove(CompanyCacheKey);
            return _companyRepository.DeleteAsync(id);
        }
    }
}
