import { Routes } from '@angular/router';
import { Dashboard } from './component/dashboard/dashboard';
import { Income } from './component/income/income';
import { Categories } from './component/categories/categories';
import { Expenses } from './component/expenses/expenses';

export const routes: Routes = [
    {
        path: '',
        redirectTo: '/dashboard',
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        component: Dashboard
    },
    {
        path: 'income',
        component: Income
    },
    {
        path: 'categories',
        component: Categories
    },
    {
        path: 'expenses',
        component: Expenses
    },
    {
        path: '**',
        redirectTo: '/dashboard'
    }
];
