using ETAPI.Models;
using ETAPI.Repositories;
using ETAPI.DTOs;

namespace ETAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<ExpenseResponseDto>> GetAllAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return expenses.Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                CategoryId = e.CategoryId,
                CategoryName = e.Category?.Name ?? string.Empty,
                ExpenseDate = e.ExpenseDate,
                PaymentMethods = string.IsNullOrWhiteSpace(e.PaymentMethods)
                    ? []
                    : e.PaymentMethods.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList(),
                Description = e.Description
            }).ToList();
        }

        public async Task<ExpenseResponseDto?> GetByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense is null)
            {
                return null;
            }

            return new ExpenseResponseDto
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category?.Name ?? string.Empty,
                ExpenseDate = expense.ExpenseDate,
                PaymentMethods = string.IsNullOrWhiteSpace(expense.PaymentMethods)
                    ? []
                    : expense.PaymentMethods.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList(),
                Description = expense.Description
            };
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            return await _expenseRepository.AddAsync(expense);
        }

        public async Task<bool> UpdateAsync(int id, Expense expense)
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(id);
            if (existingExpense is null)
            {
                throw new KeyNotFoundException($"Expense with ID {id} not found.");
            }

            existingExpense.Title = expense.Title;
            existingExpense.Amount = expense.Amount;
            existingExpense.CategoryId = expense.CategoryId;
            existingExpense.ExpenseDate = expense.ExpenseDate;
            existingExpense.PaymentMethods = expense.PaymentMethods;
            existingExpense.Description = expense.Description;

            await _expenseRepository.UpdateAsync(existingExpense);

            return true;
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