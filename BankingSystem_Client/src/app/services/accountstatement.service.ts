// // services/account-statement.service.ts
// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { AccountStatement } from '../Models/AccountStatement';

// @Injectable({
//   providedIn: 'root'
// })
// export class AccountStatementService {
//   private apiUrl = 'http://localhost:5241/api/Account/view-account-statement';  // Adjust to your API endpoint

//   constructor(private http: HttpClient) { }

//   getAccountStatement(userId: number): Observable<AccountStatement> {
//     return this.http.get<AccountStatement>(`${this.apiUrl}/${userId}`);
//   }
// }


import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TransactionModel } from '../Models/TransactionModel';

interface AccountDetails {
  holderName: string;
  accountNumber: string;
  balance: number;
}

interface TransactionsResponse {
  transactions: TransactionModel[];
  total: number;
}

@Injectable({
  providedIn: 'root'
})
export class AccountStatementService {
  private apiUrl = 'http://localhost:5241/api/Account';

  constructor(private http: HttpClient) { }

  getAccountDetails(userId: number): Observable<AccountDetails> {
    return this.http.get<AccountDetails>(`${this.apiUrl}/view-account-statement/${userId}`);
  }

  getTransactions(
    userId: number, 
    fromDate: string, 
    toDate: string, 
    page: number, 
    pageSize: number
  ): Observable<TransactionsResponse> {
    return this.http.get<any>(`${this.apiUrl}/view-account-statement/${userId}/by-date-range?fromDate=${fromDate}&toDate=${toDate}`).pipe(
      map(response => ({
        transactions: response.transactions.slice((page - 1) * pageSize, page * pageSize),
        total: response.transactions.length
      }))
    );
  }
}
