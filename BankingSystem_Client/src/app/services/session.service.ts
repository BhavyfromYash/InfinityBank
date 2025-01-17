// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { Observable } from 'rxjs';

// @Injectable({
//   providedIn: 'root'
// })
// export class SessionService {

//   private sessionExpiredCheckUrl = 'http://localhost:5241/api/User/sessionexpired';
//   constructor(private http: HttpClient) { }

//   checkSessionExpired(): Observable<any> {
//     return this.http.get(this.sessionExpiredCheckUrl);
//   }
// }
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private sessionExpiredCheckUrl = 'http://localhost:5241/api/User/sessionexpired';

  constructor(private http: HttpClient) { }

  checkSessionExpired(): Observable<any> {
    return this.http.get<any>(this.sessionExpiredCheckUrl);
  }
}

