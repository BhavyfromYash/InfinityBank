import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../Models/Customer';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  private apiUrl = 'http://localhost:5241/api/Manager'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  getCustomers(): Observable<Customer[]> 
  { 
    return this.http.get<Customer[]>(`${this.apiUrl}/customers`); 
  }

  getPendingApprovals(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.apiUrl}/pending-approvals`);
  }

  approveCustomer(customerId: number): Observable<Customer> {
    return this.http.post<Customer>(`${this.apiUrl}/approve-request/${customerId}`, {});
  }

  rejectCustomer(customerId: number): Observable<Customer> {
    return this.http.post<Customer>(`${this.apiUrl}/reject-request/${customerId}`, {});
  }
}

