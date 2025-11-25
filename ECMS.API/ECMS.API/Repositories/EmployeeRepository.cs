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

        public async Task<IEnumerable<Employee>> GetAllAsync(int pageNumber, int pageSize, string? search, string sortBy, string sortDir)
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

            query = ApplySorting(query, sortBy, sortDir);

            return await query
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
                    throw new DuplicateResourceException("Email already exists!");
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
                    throw new DuplicateResourceException("Email already exists!");
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

            _context.Employees.Remove(employee);
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

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, string sortBy, string sortDir)
        {
            sortBy = sortBy?.ToLower();
            sortDir = sortDir?.ToLower();
            bool asc = sortDir == "asc";

            return sortBy switch
            {
                "email" => asc ? query.OrderBy(e => e.Email)
                               : query.OrderByDescending(e => e.Email),

                "jobtitle" => asc ? query.OrderBy(e => e.JobTitle)
                                  : query.OrderByDescending(e => e.JobTitle),

                "company" => asc ? query.OrderBy(e => e.Company.CompanyName)
                                 : query.OrderByDescending(e => e.Company.CompanyName),

                _ => asc ? query.OrderBy(e => e.Name)
                         : query.OrderByDescending(e => e.Name)
            };
        }
    }
}
