using ETAPI.Models;
using ETAPI.Repositories;

namespace ETAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            return await _expenseRepository.AddAsync(expense);
        }

        public async Task UpdateAsync(Expense expense)
        {
            await _expenseRepository.UpdateAsync(expense);
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense is null)
            {
                throw new KeyNotFoundException($"Expense with ID {id} not found.");
            }
                await _expenseRepository.DeleteAsync(expense);
        }
    }
}