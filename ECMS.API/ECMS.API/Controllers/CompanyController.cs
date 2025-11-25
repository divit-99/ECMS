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

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>List of all companies.</returns>
        /// <response code="200">Returns the company list.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDto>), 200)]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        /// <summary>
        /// Retrieves a company by ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <returns>Company details.</returns>
        /// <response code="200">Company found.</response>
        /// <response code="404">Company not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CompanyDto>> GetById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
                throw new NotFoundException($"Company with ID '{id}' NOT found!");

            return Ok(company);
        }

        /// <summary>
        /// Creates a new company.
        /// </summary>
        /// <param name="dto">Company details to create.</param>
        /// <returns>The newly created company.</returns>
        /// <response code="201">Company created successfully.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDto), 201)]
        public async Task<ActionResult> Create(CompanySaveDto dto)
        {
            var createdCompany = await _companyService.AddCompanyAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="id">Company ID to update.</param>
        /// <param name="dto">Updated company details.</param>
        /// <returns>Update status message.</returns>
        /// <response code="200">Company updated.</response>
        /// <response code="404">Company not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, CompanySaveDto dto)
        {
            var updated = await _companyService.UpdateCompanyAsync(id, dto);

            if (!updated)
                throw new NotFoundException($"Company with ID '{id}' NOT found!");

            return Ok(new { message = $"Company with ID '{id}' UPDATED successfully!" });
        }

        /// <summary>
        /// Deletes a company by ID.
        /// </summary>
        /// <param name="id">Company ID.</param>
        /// <returns>Delete confirmation message.</returns>
        /// <response code="200">Company deleted.</response>
        /// <response code="404">Company not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _companyService.DeleteCompanyAsync(id);

            if (!deleted)
                throw new NotFoundException($"Company with ID '{id}' NOT found!");

            return Ok(new { message = $"Company with ID '{id}' DELETED successfully!" });
        }
    }
}
