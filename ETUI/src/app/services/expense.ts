import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Expense } from '../models/expense';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
    private apiUrl = 'http://localhost:5111/api/Expense';

  constructor(private http: HttpClient) {}

    getAll(): Observable<Expense[]> {
        return this.http.get<Expense[]>(this.apiUrl);
    }
    
    getById(id: number): Observable<Expense> {
        return this.http.get<Expense>(`${this.apiUrl}/${id}`);
    }

    create(expense: Omit<Expense, 'id' | 'categoryName'>): Observable<Expense> {
        return this.http.post<Expense>(this.apiUrl, expense);
    }

    update(id: number, expense: Expense): Observable<Expense> {
        return this.http.put<Expense>(`${this.apiUrl}/${id}`, expense);
    }
    
    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
