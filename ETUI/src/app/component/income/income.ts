import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { IncomeService } from '../../services/income';
import { Income as IncomeModel } from '../../models/income';

@Component({
  imports: [FormsModule],
  selector: 'app-income',
  styleUrl: './income.css',
  templateUrl: './income.html',
})
export class Income implements OnInit {
  incomes: IncomeModel[] = [];
  currentPage = 1;
  readonly pageSize = 25;

  income = {
    id: 0,
    source: '',
    amount: 0,
    incomeType: '',
    incomeDate: '',
    description: ''
  };

  editingIncomeId: number | null = null;

  constructor(
    private incomeService: IncomeService,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadIncome();
  }

  loadIncome(): void {

    this.incomeService
      .getAll()
      .subscribe(data => {

        this.incomes = data;
        this.currentPage = 1;
        this.changeDetector.markForCheck();

      });
  }

  get visibleIncomes(): IncomeModel[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.incomes.slice(start, start + this.pageSize);
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.incomes.length / this.pageSize));
  }

  previousPage(): void {
    this.currentPage = Math.max(1, this.currentPage - 1);
  }

  nextPage(): void {
    this.currentPage = Math.min(this.totalPages, this.currentPage + 1);
  }

  addIncome(): void {

    const request = this.editingIncomeId === null
      ? this.incomeService.create(this.income)
      : this.incomeService.update(this.editingIncomeId, this.income);

    request
      .subscribe(() => {
        this.resetForm();
        this.loadIncome();
      });
  }

  editIncome(income: IncomeModel): void {
    this.editingIncomeId = income.id;
    this.income = {
      ...income,
      incomeDate: income.incomeDate.substring(0, 10)
    };
  }

  resetForm(): void {
    this.editingIncomeId = null;
    this.income = {
      id: 0,
      source: '',
      amount: 0,
      incomeType: '',
      incomeDate: '',
      description: ''
    };
  }

  deleteIncome(id: number): void {

    this.incomeService
      .delete(id)
      .subscribe(() => {
        if (this.editingIncomeId === id) {
          this.resetForm();
        }
        this.loadIncome();
      });
  }
}
