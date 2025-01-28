// account-summary.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'; 
import { AccountSummaryModel } from '../Models/AccountSummaryModel';

@Injectable({
  providedIn: 'root'
})
export class AccountSummaryService {
  private apiUrl = 'http://localhost:5241/api/Account/account-summary';

  constructor(private http: HttpClient) {}

  getAccountSummary(userId:number): Observable<AccountSummaryModel> {
    return this.http.get<AccountSummaryModel>(`${this.apiUrl}/${userId}`);
  }
}
