import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForgotpasswordService {
  private apiUrl = 'http://localhost:5241/api/User/forgot-password';
  constructor(private http: HttpClient) { }
  sendResetLink(email: string): Observable<any> {
    const body = { email };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.apiUrl, body, { headers });
  }
}

