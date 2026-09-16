using ETAPI.Models;
using ETAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public IncomeRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Income>> GetAllAsync()
        {
            return await _context.Incomes.AsNoTracking().OrderByDescending(i => i.IncomeDate).ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Income> AddAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
            return income;
        }

        public async Task UpdateAsync(Income income)
        {
            _context.Incomes.Update(income);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Income income)
        {
            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();
        }
    }
}