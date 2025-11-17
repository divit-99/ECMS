using ECMS.API.Data;
using ECMS.API.Exceptions;
using ECMS.API.Models;
using ECMS.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECMS.API.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
                => await _context.Companies.ToListAsync();

        public async Task<Company?> GetByIdAsync(int? id)
        {
            return await _context.Companies
                        .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<Company> AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx &&
                    sqlEx.Message.Contains("IX_Companies_Domain"))
                {
                    throw new DuplicateResourceException("Domain already exists.");
                }
                throw;
            }
            return company;
        }

        public async Task UpdateAsync(Company company)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx &&
                    sqlEx.Message.Contains("IX_Companies_Domain"))
                {
                    throw new DuplicateResourceException("Domain already exists.");
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _context.Companies
                                .FirstOrDefaultAsync(c => c.ID == id);

            if (company == null)
                return false;

            _context.Companies.Remove(company);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
