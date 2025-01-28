import { Injectable } from '@angular/core';
import { ShowAccountBalance } from '../Models/ShowBalance';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ViewbalanceService {

  private baseUrl = "http://localhost:5241/api/Account";
  constructor(private http: HttpClient) { }

  getAccountBalance(accountId: number): Observable<ShowAccountBalance> { 
    return this.http.get<ShowAccountBalance>(`${this.baseUrl}/ShowBalance/${accountId}`); 
  }
}
