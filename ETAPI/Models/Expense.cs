namespace ETAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string PaymentMethods { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ReceiptImagePath { get; set; }
    }
}