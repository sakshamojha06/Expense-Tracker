using ETAPI.Models;

namespace ETAPI.Repositories
{
    public interface IExpenseRepository
    {
       Task<List<Expense>> GetAllAsync();
       Task<Expense?> GetByIdAsync(int id);
       Task<Expense> AddAsync(Expense expense);
       Task UpdateAsync(Expense expense);
       Task DeleteAsync(Expense expense);


    }
}