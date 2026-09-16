export interface Expense {
    id: number;
    title: string;
    amount: number;
    categoryId: number;
    categoryName: string;
    expenseDate: string;
    paymentMethods: string[];
    description: string;
}