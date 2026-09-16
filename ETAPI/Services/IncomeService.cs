using ETAPI.Models;
using ETAPI.Repositories;

namespace ETAPI.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<List<Income>> GetAllAsync()
        {
            return await _incomeRepository.GetAllAsync();
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _incomeRepository.GetByIdAsync(id);
        }

        public async Task<Income> CreateAsync(Income income)
        {
            return await _incomeRepository.AddAsync(income);
        }

        public async Task<bool> UpdateAsync(int id,Income income)
        {
            var existingIncome = await _incomeRepository.GetByIdAsync(id);
            if (existingIncome is null)
            {
                return false;
            }

            existingIncome.Amount = income.Amount;
            existingIncome.Source = income.Source;
            existingIncome.Description = income.Description;
            existingIncome.IncomeType = income.IncomeType;
            existingIncome.IncomeDate = income.IncomeDate;

            await _incomeRepository.UpdateAsync(existingIncome);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingincome = await _incomeRepository.GetByIdAsync(id);
            if (existingincome is null)
            {
                return false;
            }
            await _incomeRepository.DeleteAsync(existingincome);
            return true;
        }
        }
    }