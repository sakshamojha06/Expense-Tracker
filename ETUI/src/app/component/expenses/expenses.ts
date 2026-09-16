import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Expense } from '../../models/expense';
import { ExpenseService } from '../../services/expense';
import { CategoryService } from '../../services/category';
import { Category } from '../../models/category';



@Component({
  imports: [FormsModule],
  selector: 'app-expenses',
  styleUrl: './expenses.css',
  templateUrl: './expenses.html',
})
export class Expenses implements OnInit {
  expenses: Expense[] = [];

  categories: Category[] = [];

  expense = {
    id: 0,
    title: '',
    amount: 0,
    categoryId: 0,
    categoryName: '',
    expenseDate: '',
    paymentMethods: [] as string[],
    description: ''
  };

  editingExpenseId: number | null = null;

  constructor(
    private expenseService: ExpenseService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadExpenses();
    this.loadCategories();
  }

  loadExpenses(): void {

    this.expenseService
      .getAll()
      .subscribe(data => {

        this.expenses = data;

      });
  }

  loadCategories(): void {

    this.categoryService
      .getAll()
      .subscribe(data => {

        this.categories = data;

      });
  }

  addExpense(): void {

    const expenseToSave = {
      id: this.expense.id,
      title: this.expense.title,
      amount: this.expense.amount,
      categoryId: this.expense.categoryId,
      categoryName: this.expense.categoryName,
      expenseDate: this.expense.expenseDate,
      paymentMethods: this.expense.paymentMethods,
      description: this.expense.description
    };

    const request = this.editingExpenseId === null
      ? this.expenseService.create(expenseToSave)
      : this.expenseService.update(this.editingExpenseId, expenseToSave);

    request
      .subscribe(() => {
        this.resetForm();
        this.loadExpenses();
      });
  }

  editExpense(expense: Expense): void {
    this.editingExpenseId = expense.id;
    this.expense = {
      ...expense,
      expenseDate: expense.expenseDate.substring(0, 10),
      paymentMethods: [...expense.paymentMethods]
    };
  }

  resetForm(): void {
    this.editingExpenseId = null;
    this.expense = {
      id: 0,
      title: '',
      amount: 0,
      categoryId: 0,
      categoryName: '',
      expenseDate: '',
      paymentMethods: [],
      description: ''
    };
  }

  deleteExpense(id: number): void {

    this.expenseService
      .delete(id)
      .subscribe(() => {
        if (this.editingExpenseId === id) {
          this.resetForm();
        }
        this.loadExpenses();
      });
  }
}
