import { Component, OnInit, signal } from '@angular/core';
import { forkJoin } from 'rxjs';
import { IncomeService } from '../../services/income';
import { ExpenseService } from '../../services/expense';

@Component({
  imports: [],
  selector: 'app-dashboard',
  styleUrl: './dashboard.css',
  templateUrl: './dashboard.html',
})
export class Dashboard implements OnInit {

  totalIncome = signal(0);
  totalExpense = signal(0);
  balance = signal(0);

  constructor(
    private incomeService: IncomeService,
    private expenseService: ExpenseService
  ) {}

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {

    forkJoin({
      incomes: this.incomeService.getAll(),
      expenses: this.expenseService.getAll()
    }).subscribe(({ incomes, expenses }) => {
      this.totalIncome.set(incomes.reduce(
        (total, income) => total + Number(income.amount),
        0
      ));

      this.totalExpense.set(expenses.reduce(
        (total, expense) => total + Number(expense.amount),
        0
      ));

      this.calculateBalance();
    });
  }

  calculateBalance(): void {

    this.balance.set(this.totalIncome() - this.totalExpense());

  }
}
