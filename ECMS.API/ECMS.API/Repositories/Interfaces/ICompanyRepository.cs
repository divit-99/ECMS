using ECMS.API.Models;

namespace ECMS.API.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int? id);
        Task<Company> AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
    }

}
