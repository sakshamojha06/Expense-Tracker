using System.ComponentModel.DataAnnotations;

namespace ETAPI.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}