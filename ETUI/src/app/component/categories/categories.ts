import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../models/category';
import { CategoryService } from '../../services/category';

@Component({
  imports: [CommonModule, FormsModule],
  selector: 'app-categories',
  styleUrl: './categories.css',
  templateUrl: './categories.html',
})
export class Categories implements OnInit {

  categories: Category[] = [];
  currentPage = 1;
  readonly pageSize = 25;

  category = {
    id: 0,
    name: '',
    description: ''
  };

  editingCategoryId: number | null = null;

  constructor(
    private categoryService: CategoryService,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {

    this.categoryService
      .getAll()
      .subscribe(data => {

        this.categories = data;
        this.currentPage = 1;
        this.changeDetector.markForCheck();

      });
  }

  get visibleCategories(): Category[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.categories.slice(start, start + this.pageSize);
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.categories.length / this.pageSize));
  }

  previousPage(): void {
    this.currentPage = Math.max(1, this.currentPage - 1);
  }

  nextPage(): void {
    this.currentPage = Math.min(this.totalPages, this.currentPage + 1);
  }

  addCategory(): void {

    const categoryToSave: Category = {
      id: this.category.id,
      name: this.category.name,
      description: this.category.description
    };

    const request = this.editingCategoryId === null
      ? this.categoryService.create(categoryToSave)
      : this.categoryService.update(this.editingCategoryId, categoryToSave);

    request
      .subscribe(() => {
        this.resetForm();
        this.loadCategories();
      });
  }

  editCategory(category: Category): void {
    this.editingCategoryId = category.id;
    this.category = { ...category };
  }

  resetForm(): void {
    this.editingCategoryId = null;
    this.category = { id: 0, name: '', description: '' };
  }

  deleteCategory(id: number): void {

    this.categoryService
      .delete(id)
      .subscribe(() => {
        if (this.editingCategoryId === id) {
          this.resetForm();
        }
        this.loadCategories();
      });
  }

}
