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

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                        .Include(e => e.Company)
                        .Where(e => e.IsActive)
                        .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                        .Include(e => e.Company)
                        .Where(e => e.IsActive)
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

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.ID == id && e.IsActive);

            if (employee == null)
                return false;

            employee.IsActive = false;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
