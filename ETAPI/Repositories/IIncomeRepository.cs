using ETAPI.Models;

namespace ETAPI.Repositories
{
    public interface IIncomeRepository
    {
        Task<List<Income>> GetAllAsync();
        Task<Income?> GetByIdAsync(int id);
        Task<Income> AddAsync(Income income);
        Task UpdateAsync(Income income);
        Task DeleteAsync(Income income);
    }
}