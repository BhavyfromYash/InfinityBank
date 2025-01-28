// account-details.component.ts
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { AccountDetails } from '../Models/AccountDetails';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {
  accountDetails: AccountDetails | null = null;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    const userId = sessionStorage.getItem('UserId');
    console.log('UserId retrieved from session:', userId); // Debug log

    if (userId) {
      this.accountService.getAccountDetails(+userId).subscribe(
        (data) => {
          console.log('Fetched account details:', data); // Debug log
          this.accountDetails = data;
        },
        (error) => {
          console.error('Error fetching account details', error);
        }
      );
    } else {
      console.error('UserId not found in session');
    }
  }
}
