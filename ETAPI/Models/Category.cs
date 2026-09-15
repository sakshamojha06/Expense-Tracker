namespace ETAPI.Models
{
    public class Category
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string? ImagePath {get; set;}
        public ICollection<Expense> Expenses {get; set;} = new List<Expense>();
    }
}