// // components/account-statement.component.ts
// import { Component, OnInit } from '@angular/core';
// import { AccountStatement } from '../Models/AccountStatement';
// import { AccountStatementService } from '../services/accountstatement.service';

// @Component({
//   selector: 'app-account-statement',
//   templateUrl: './account-statement.component.html',
//   styleUrls: ['./account-statement.component.css']
// })
// export class AccountStatementComponent implements OnInit {
//   accountStatement: AccountStatement | null = null;

//   constructor(private accountStatementService: AccountStatementService) { }

//   ngOnInit(): void {
//     this.fetchAccountStatement();
//   }

//   fetchAccountStatement(): void {
//     const userId = sessionStorage.getItem('UserId');
//     console.log('UserId retrieved from session:', userId); // Debug log

//     if (userId) {
//       this.accountStatementService.getAccountStatement(+userId).subscribe(
//         (data) => {
//           console.log('Fetched account statement:', data); // Debug log
//           this.accountStatement = data;
//         },
//         (error) => {
//           console.error('Error fetching account statement:', error);
//         }
//       );
//     } else {
//       console.error('UserId not found in session');
//       // Optionally, redirect to the login page
//     }
//   }
// }

import { Component, OnInit } from '@angular/core';
import { TransactionModel } from '../Models/TransactionModel';
import { AccountStatementService } from '../services/accountstatement.service';

@Component({
  selector: 'app-account-statement',
  templateUrl: './account-statement.component.html',
  styleUrls: ['./account-statement.component.css']
})
export class AccountStatementComponent implements OnInit {
  transactions: TransactionModel[] = [];
  loading = false;
  error: string | null = null;
  dateFrom: string = '';
  dateTo: string = '';

  accountHolder: string = '';
  accountNumber: string = '';
  currentBalance: number = 0;

  currentPage = 1;
  pageSize = 10;
  totalTransactions = 0;
  totalPages = 0;
  pages: number[] = [];
  Math = Math;

  constructor(private accountStatementService: AccountStatementService) { }

  ngOnInit(): void {
    this.loadAccountDetails();
  }

  loadAccountDetails(): void {
    const userId = sessionStorage.getItem('UserId');
    if (userId) {
      this.accountStatementService.getAccountDetails(+userId).subscribe({
        next: (data) => {
          this.accountHolder = data.holderName;
          this.accountNumber = data.accountNumber;
          this.currentBalance = data.balance;
        },
        error: (error) => {
          console.error('Error loading account details:', error);
          this.error = 'Failed to load account details';
        }
      });
    }
  }

  // fetchTransactions(): void {
  //   if (!this.dateFrom || !this.dateTo) {
  //     this.error = 'Please select both start and end dates';
  //     return;
  //   }
  
  //   const userId = sessionStorage.getItem('UserId');
  //   if (!userId) {
  //     this.error = 'User ID not found. Please log in again.';
  //     return;
  //   }
  
  //   this.loading = true;
  //   this.error = null;
  
  //   this.accountStatementService.getTransactions(
  //     +userId,
  //     this.dateFrom,
  //     this.dateTo,
  //     this.currentPage,
  //     this.pageSize
  //   ).subscribe({
  //     next: (response) => {
  //       let runningBalance = this.currentBalance;
  
  //       this.transactions = response.transactions.map((t) => {
  //         const transaction: TransactionModel = { ...t };
  
  //         transaction.description = this.getDescription(transaction.transactionType);
  //         transaction.transactionDate = transaction.transactionDate;
  
  //         // Determine debit or credit and adjust the running balance
  //         if (transaction.transactionType === 'Deposit') {
  //           transaction.credit = transaction.amount;
  //           transaction.debit = 0;  // Ensure debit is set to 0 for deposits
  //           runningBalance += transaction.amount;
  //         } else if (transaction.transactionType === 'Withdraw') {
  //           transaction.debit = transaction.amount;
  //           transaction.credit = 0; // Ensure credit is set to 0 for withdrawals
  //           runningBalance -= transaction.amount;
  //         }
  //         transaction.balance = runningBalance;
  
  //         return transaction;
  //       });
  
  //       this.totalTransactions = response.total;
  //       this.totalPages = Math.ceil(this.totalTransactions / this.pageSize);
  //       this.generatePageNumbers();
  //       this.loading = false;
  //     },
  //     error: (error) => {
  //       console.error('Error fetching transactions:', error);
  //       this.error = 'Failed to fetch transactions. Please try again.';
  //       this.loading = false;
  //     }
  //   });
  // }
  fetchTransactions(): void {
    if (!this.dateFrom || !this.dateTo) {
      this.error = 'Please select both start and end dates';
      return;
    }
  
    const userId = sessionStorage.getItem('UserId');
    if (!userId) {
      this.error = 'User ID not found. Please log in again.';
      return;
    }
  
    this.loading = true;
    this.error = null;
  
    this.accountStatementService.getTransactions(
      +userId,
      this.dateFrom,
      this.dateTo,
      this.currentPage,
      this.pageSize
    ).subscribe({
      next: (response) => {
        let runningBalance = this.currentBalance;
  
        this.transactions = response.transactions.map((t) => {
          const transaction = { ...t };
  
          transaction.description = this.getDescription(transaction.transactionType);
          transaction.transactionDate = new Date(transaction.transactionDate); // Ensure date is correctly parsed
  
          // Calculate debit or credit and update running balance
          if (transaction.transactionType === 'Deposit') {
            transaction.credit = transaction.amount;
            transaction.debit = 0; // Ensure debit is set to 0 for deposits
            runningBalance += transaction.amount;
          } else if (transaction.transactionType === 'Withdraw') {
            transaction.debit = transaction.amount;
            transaction.credit = 0; // Ensure credit is set to 0 for withdrawals
            runningBalance -= transaction.amount;
          }
          transaction.balance = runningBalance;
  
          return transaction;
        });
  
        this.totalTransactions = response.total;
        this.totalPages = Math.ceil(this.totalTransactions / this.pageSize);
        this.generatePageNumbers();
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching transactions:', error);
        this.error = 'Failed to fetch transactions. Please try again.';
        this.loading = false;
      }
    });
  }
  
  
  
  getDescription(transactionType: string): string {
    switch (transactionType) {
      case 'Deposit': 
        return 'Deposit made';
      case 'Withdraw': 
        return 'Withdrawal';
      default: 
        return 'Unknown transaction';
    }
  }  


  getBalance(transaction: TransactionModel): number {
    return transaction.amount;
  }

  generatePageNumbers(): void {
    this.pages = [];
    for (let i = 1; i <= this.totalPages; i++) {
      if (i === 1 || i === this.totalPages || (i >= this.currentPage - 1 && i <= this.currentPage + 1)) {
        this.pages.push(i);
      } else if (i === this.currentPage - 2 || i === this.currentPage + 2) {
        this.pages.push(-1);
      }
    }
    this.pages = [...new Set(this.pages)];
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.currentPage) {
      this.currentPage = page;
      this.fetchTransactions();
    }
  }

  onDateChange(): void {
    this.error = null;
    this.currentPage = 1;
  }
}
