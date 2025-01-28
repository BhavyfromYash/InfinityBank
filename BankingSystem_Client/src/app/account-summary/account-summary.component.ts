import { Component, OnInit } from '@angular/core';
import { AccountSummaryModel } from '../Models/AccountSummaryModel';
import { AccountSummaryService } from '../services/accountsummary.service';

@Component({
  selector: 'app-account-summary',
  templateUrl: './account-summary.component.html',
  styleUrls: ['./account-summary.component.css']
})
export class AccountSummaryComponent implements OnInit {
  accountSummary: AccountSummaryModel | null = null;
  error: any = null

  constructor(private accountSummaryService: AccountSummaryService) { }

  ngOnInit(): void {
    this.fetchAccountSummary();
  }

  fetchAccountSummary(): void {
    const userId = sessionStorage.getItem('UserId');
    console.log('UserId retrieved from session:', userId); 

    if (userId) {
      this.accountSummaryService.getAccountSummary(+userId).subscribe(
        (data) => {
          console.log('Fetched account summary:', data); 
          this.accountSummary = data;
        },
        (error) => {
          console.error('Error fetching account summary:', error);
        }
      );
    } else {
      console.error('UserId not found in session');
    }
  }
}
