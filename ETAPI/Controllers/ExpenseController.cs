using ETAPI.DTOs;
using ETAPI.Models;
using ETAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense is null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateDto expenseDto)
        {
            var expense = new Expense
            {
                Title = expenseDto.Title,
                Amount = expenseDto.Amount,
                CategoryId = expenseDto.CategoryId,
                ExpenseDate = expenseDto.ExpenseDate,
                PaymentMethods = string.Join(",", expenseDto.PaymentMethods),
                Description = expenseDto.Description
            };
            var createdExpense = await _expenseService.CreateAsync(expense);
            return CreatedAtAction(nameof(GetById), new { id = createdExpense.Id }, createdExpense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            await _expenseService.UpdateAsync(id, expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _expenseService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}