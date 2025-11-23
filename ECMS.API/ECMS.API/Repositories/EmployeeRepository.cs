using ECMS.API.Data;
using ECMS.API.Exceptions;
using ECMS.API.Models;
using ECMS.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ECMS.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            var query = _context.Employees
            .Include(e => e.Company)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(e =>
                    e.Name.ToLower().Contains(term) ||
                    e.Email.ToLower().Contains(term));
                    //|| e.JobTitle.ToLower().Contains(term) ||
                    //e.Company.CompanyName.ToLower().Contains(term));
            }

            return await query
                        .OrderBy(e => e.ID)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                        .Include(e => e.Company)
                        .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx &&
                    sqlEx.Message.Contains("IX_Employees_Email"))
                {
                    throw new DuplicateResourceException("Email already exists.");
                }
                throw;
            }
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx &&
                    sqlEx.Message.Contains("IX_Employees_Email"))
                {
                    throw new DuplicateResourceException("Email already exists.");
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.ID == id);

            if (employee == null)
                return false;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetCountAsync(string? search)
        {
            var query = _context.Employees
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(e =>
                    e.Name.ToLower().Contains(term) ||
                    e.Email.ToLower().Contains(term));
                    //|| e.JobTitle.ToLower().Contains(term) ||
                    //e.Company.CompanyName.ToLower().Contains(term));
            }
            return await query.CountAsync();
        }
    }
}
