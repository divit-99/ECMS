using ECMS.API.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;

namespace ECMS.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(int pageNumber, int pageSize, string? search, string sortBy, string sortDir);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<int> GetCountAsync(string? search);
    }
}
