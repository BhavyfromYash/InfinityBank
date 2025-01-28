// export interface TransactionModel {
//     accountId: number;
//     amount: number;
//     transactionType: string;
//     transactionDate: Date;
//     description?: string; 
//     balance?: number
// }

export interface TransactionModel {
    accountId: number;
    amount: number;
    transactionType: string;
    transactionDate: Date;
    description: string;
    balance: number
    credit: number;
    debit: number;
}

