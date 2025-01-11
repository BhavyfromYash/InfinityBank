// import { Injectable } from '@angular/core';
// import { HttpClient, HttpErrorResponse } from '@angular/common/http';
// import { catchError, Observable, throwError } from 'rxjs';
// import { User } from '../Models/User';

// @Injectable({
//   providedIn: 'root'
// })
// export class RegisterService {

//   private apiUrl = 'http://localhost:5241/api/User/register';

//   constructor(private http: HttpClient) { }

// register(user: User): Observable<any> {
//   return this.http.post(`${this.apiUrl}`, user);
// }
//   checkUserExists(email: string): Observable<boolean> { 
//     return this.http.get<boolean>(`${this.apiUrl}/check-user/${email}`); 
//   }
// }


import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../Models/User';

interface UserExistsResponse {
  exists: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private apiUrl = 'http://localhost:5241/api/User';

  constructor(private http: HttpClient) { }

  register(user: User): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  checkUserExists(email: string): Observable<UserExistsResponse> { 
    return this.http.get<UserExistsResponse>(`${this.apiUrl}/exists/${email}`); 
  }

}