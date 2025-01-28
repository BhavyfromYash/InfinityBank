import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerCreationModel } from '../Models/CustomerCreationModel';

@Injectable({
    providedIn: 'root'
})
export class CustomerService {
    private apiUrl = 'http://localhost:5241/api/Customer'; // Replace with your actual API URL

    constructor(private http: HttpClient) { }

    createCustomer(userId: number,customer: CustomerCreationModel): Observable<CustomerCreationModel> {
        return this.http.post<CustomerCreationModel>(`${this.apiUrl}/create-customer?userId=${userId}`, customer);
    }
    // getCustomerDetails(): Observable<any> {
    //     return this.http.get(`${this.apiUrl}/details`);
    // }
    isUserExists(userId: number): Observable<boolean> {
        return this.http.get<boolean>(`${this.apiUrl}/is-user-exists?userId=${userId}`);
    }
}