import { Component, OnInit } from '@angular/core';
import { ShowAccountBalance } from '../Models/ShowBalance';
import { ViewbalanceService } from '../services/viewbalance.service';

@Component({
  selector: 'app-view-balance',
  templateUrl: './view-balance.component.html',
  styleUrls: ['./view-balance.component.css']
})
export class ViewBalanceComponent implements OnInit {
  accountId!: number;
  balance!: ShowAccountBalance;

  constructor(private viewBalanceService: ViewbalanceService) { }

  ngOnInit(): void {
    this.loadAccountIdFromSession();
    if (this.accountId) {
      this.showBalance();
    } else {
      console.error('Account ID not found in session storage');
    }
  }

  loadAccountIdFromSession(): void {
    const storedAccountId = sessionStorage.getItem('accountId');
    console.log('Retrieving accountId from session storage:', storedAccountId);
    if (storedAccountId) {
      this.accountId = +storedAccountId;
      console.log('Loaded accountId from session:', this.accountId);
    } else {
      console.error('No accountId found in session storage');
    }
  }

  showBalance(): void {
    this.viewBalanceService.getAccountBalance(this.accountId).subscribe(data => {
      this.balance = data;
    },
    error => {
      console.error('Error fetching account balance', error);
      alert('Account Balance not found');
    });
  }
}
