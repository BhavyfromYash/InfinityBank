import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserLoginModel } from '../Models/UserLoginModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl: string = 'http://localhost:5241/api/User/login';
  constructor(private http: HttpClient) { }
  Login(User: UserLoginModel):any {
    return this.http.post<any>(this.apiUrl, User, { observe: 'response' });
  }
}
