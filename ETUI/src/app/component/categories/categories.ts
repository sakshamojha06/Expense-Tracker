import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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

  category = {
    id: 0,
    name: '',
    description: ''
  };

  editingCategoryId: number | null = null;

  constructor(
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {

    this.categoryService
      .getAll()
      .subscribe(data => {

        this.categories = data;

      });
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
