// // // import { HttpClient, HttpHeaders } from '@angular/common/http';
// // // import { Injectable } from '@angular/core';
// // // import { NewAccount } from '../Models/NewAccount';
// // // import { Observable, throwError } from 'rxjs';
// // // import { catchError } from 'rxjs/operators';
// // // import { ViewAccountDetails } from '../Models/AccountDetails';

// // // @Injectable({
// // //   providedIn: 'root'
// // // })
// // // export class AccountService {
// // //   private apiUrl = 'http://localhost:5241/api/Account';

// // //   constructor(private http: HttpClient) { }

// // // createAccount(accountData: NewAccount): Observable<any> {
// // //   return this.http.post(`${this.apiUrl}/create`, accountData).pipe(
// // //     catchError(this.handleError)
// // //   );
// // // }

// // //   getAccountDetails(): Observable<ViewAccountDetails[]> {
// // //     const token = sessionStorage.getItem('loginToken');
// // //     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
// // //     console.log(`Token being used in getAccountDetails: ${token}`); // Debugging log

// // //     return this.http.get<ViewAccountDetails[]>(`${this.apiUrl}/account-details`, { headers }).pipe(
// // //       catchError(this.handleError)
// // //     );
// // //   }

// // //   private handleError(error: any): Observable<never> {
// // //     console.error('An error occurred:', error); // Logging the error
// // //     return throwError(error);
// // //   }
// // // }



// // import { Injectable } from '@angular/core';
// // import { HttpClient } from '@angular/common/http';
// // import { Observable } from 'rxjs';
// // import { ViewAccountDetails } from '../Models/AccountDetails';
// // import { NewAccount } from '../Models/NewAccount';
// // @Injectable({
// //   providedIn: 'root'
// // })
// // export class AccountDetailsService {
// //   private apiUrl = 'http://localhost:5241/api/Account'; // Replace with your API URL

// //   constructor(private http: HttpClient) { }
// //   createAccount(accountData: NewAccount): Observable<any> {
// //     return this.http.post(`${this.apiUrl}/create`, accountData)
// //   }

// //   getAccountDetails(): Observable<ViewAccountDetails> {
// //     return this.http.get<ViewAccountDetails>(`${this.apiUrl}/account-details` , { withCredentials: true});
// //   }
// // }


// import { Injectable } from '@angular/core';
// import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { Observable, throwError } from 'rxjs';
// import { catchError } from 'rxjs/operators';
// import { ViewAccountDetails } from '../Models/AccountDetails';
// import { NewAccount } from '../Models/NewAccount';

// @Injectable({
//   providedIn: 'root'
// })
// export class AccountDetailsService {
//   private apiUrl = 'http://localhost:5241/api/Account'; // Replace with your API URL

//   constructor(private http: HttpClient) { }

//   createAccount(accountData: NewAccount): Observable<any> {
//     return this.http.post(`${this.apiUrl}/create`, accountData).pipe(
//       catchError(this.handleError)
//     );
//   }

//   getAccountDetails(): Observable<ViewAccountDetails> {
//     const token = sessionStorage.getItem('loginToken');
//     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
//     console.log(`Token being used in getAccountDetails: ${token}`); // Debugging log

//     return this.http.get<ViewAccountDetails>(`${this.apiUrl}/account-details`, { headers, withCredentials: true }).pipe(
//       catchError(this.handleError)
//     );

//   }

//   private handleError(error: any): Observable<never> {
//     console.error('An error occurred:', error); // Logging the error
//     return throwError(error);
//   }
// }


// account.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NewAccount } from '../Models/NewAccount';
import { AccountDetails } from '../Models/AccountDetails';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'http://localhost:5241/api/Account'; // Change to your actual API URL

  constructor(private http: HttpClient) { }

  createAccount(accountData: NewAccount): Observable<any> {
    return this.http.post(`${this.apiUrl}/create`, accountData).pipe(
      catchError(this.handleError)
    );
  }

  getAccountDetails(userId: number): Observable<AccountDetails> {
    return this.http.get<AccountDetails>(`${this.apiUrl}/account-details/${userId}`).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
    return throwError('Something went wrong; please try again later.');
  }
}

