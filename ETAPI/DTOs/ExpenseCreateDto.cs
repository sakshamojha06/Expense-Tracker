namespace ETAPI.DTOs;

public class ExpenseCreateDto
{
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int CategoryId { get; set; } 
    public DateTime ExpenseDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}