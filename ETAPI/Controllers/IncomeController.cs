using ETAPI.DTOs;
using ETAPI.Models;
using ETAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var incomes = await _incomeService.GetAllAsync();
            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var income = await _incomeService.GetByIdAsync(id);
            if (income is null)
            {
                return NotFound(new {message = $"Income with ID {id} not found."});
            }
            return Ok(income);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IncomeCreateDto incomeDto)
        {
            var income = new Income
            {
                Amount = incomeDto.Amount,
                Source = incomeDto.Source,
                IncomeType = incomeDto.IncomeType,
                IncomeDate = incomeDto.IncomeDate,
                Description = incomeDto.Description
            };

            var createdIncome = await _incomeService.CreateAsync(income);
            return CreatedAtAction(nameof(GetById), new { id = createdIncome.Id }, createdIncome);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IncomeUpdateDto incomeDto)
        {
            var income = new Income
            {
                Amount = incomeDto.Amount,
                Source = incomeDto.Source,
                IncomeType = incomeDto.IncomeType,
                IncomeDate = incomeDto.IncomeDate,
                Description = incomeDto.Description
            };

            var update  = await _incomeService.UpdateAsync(id, income);
            if (!update)
            {
                return NotFound(new { message = $"Income with ID {id} not found." });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _incomeService.DeleteAsync(id);
            if (!delete)
            {
                return NotFound(new { message = $"Income with ID {id} not found." });
            }
            return NoContent();
        }
    }
}