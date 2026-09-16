import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Income } from '../models/income';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {
    private apiUrl = 'http://localhost:5111/api/Income';

  constructor(private http: HttpClient) {}

    getAll(): Observable<Income[]> {
        return this.http.get<Income[]>(this.apiUrl);
    }

    getById(id: number): Observable<Income> {
        return this.http.get<Income>(`${this.apiUrl}/${id}`);
    }

    create(income: any): Observable<Income> {
        return this.http.post<Income>(this.apiUrl, income);
    }

    update(id: number, income: Income): Observable<Income> {
        return this.http.put<Income>(`${this.apiUrl}/${id}`, income);
    }
    
    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}