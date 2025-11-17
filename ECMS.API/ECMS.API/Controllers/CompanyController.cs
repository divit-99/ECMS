using ECMS.API.DTOs.Company;
using ECMS.API.Exceptions;
using ECMS.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
                throw new NotFoundException($"Company with ID '{id}' NOT found.");

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CompanySaveDto dto)
        {
            var createdCompany = await _companyService.AddCompanyAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CompanySaveDto dto)
        {
            var updated = await _companyService.UpdateCompanyAsync(id, dto);

            if (!updated)
                throw new NotFoundException($"Company with ID '{id}' NOT found.");

            return Ok(new { message = $"Company with ID '{id}' UPDATED successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _companyService.DeleteCompanyAsync(id);

            if (!deleted)
                throw new NotFoundException($"Company with ID '{id}' NOT found.");

            return Ok(new { message = $"Company with ID '{id}' DELETED successfully." });
        }
    }
}
