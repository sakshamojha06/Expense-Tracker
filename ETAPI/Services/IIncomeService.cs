using ETAPI.Models;

namespace ETAPI.Services
{
    public interface IIncomeService
    {
        Task<List<Income>> GetAllAsync();
        Task<Income?> GetByIdAsync(int id);
        Task<Income> CreateAsync(Income income);
        Task<bool> UpdateAsync(int id, Income income);
        Task<bool> DeleteAsync(int id);
    }
}