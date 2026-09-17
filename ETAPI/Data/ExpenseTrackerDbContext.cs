using ETAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Data
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(expense => expense.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Income>()
                .Property(income => income.Amount)
                .HasPrecision(18, 2);
        }
    }
}