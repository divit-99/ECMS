using ECMS.API.DTOs.Company;
using ECMS.API.Models;

namespace ECMS.API.Mappings
{
    public static class CompanyMapper
    {
        public static CompanyDto ToDto(this Company entity)
        {
            return new CompanyDto
            {
                Id = entity.ID,
                CompanyName = entity.CompanyName,
                Domain = entity.Domain,
                Industry = entity.Industry,
                Website = entity.Website
            };
        }
        public static Company ToEntity(this CompanySaveDto dto)
        {
            return new Company
            {
                CompanyName = dto.CompanyName,
                Domain = dto.Domain,
                Industry = dto.Industry,
                Website = dto.Website
            };
        }

        public static void UpdateFromDto(this Company entity, CompanySaveDto dto)
        {
            entity.CompanyName = dto.CompanyName;
            entity.Domain = dto.Domain;
            entity.Industry = dto.Industry;
            entity.Website = dto.Website;
        }
    }
}
