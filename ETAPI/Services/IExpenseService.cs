using ETAPI.Models;
using ETAPI.DTOs;

namespace ETAPI.Services;
    public interface IExpenseService
{
        Task<List<ExpenseResponseDto>> GetAllAsync();
        Task<ExpenseResponseDto?> GetByIdAsync(int id);
        Task<Expense> CreateAsync(Expense expense);
        Task<bool> UpdateAsync(int id, Expense expense);
        Task DeleteAsync(int id);
    }