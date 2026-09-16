using System.ComponentModel.DataAnnotations;

namespace ETAPI.DTOs
{
    public class IncomeUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Source { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "1000000000")]
        public decimal Amount { get; set; }

        [Required]
        public string IncomeType { get; set; } = string.Empty;

        public DateTime IncomeDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}