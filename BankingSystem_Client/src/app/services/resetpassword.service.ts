import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResetpasswordService {

  private apiUrl = 'http://localhost:5241/api/User/reset-password';

  constructor(private http: HttpClient) { } 
  resetPassword(email: string, password: string): Observable<any> { 
    const body = { email, password, confirmPassword: password }; 
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' }); 
    return this.http.post(this.apiUrl, body, { headers }); 
  }

}
