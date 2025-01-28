// session.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionsService {

  setSessionData(key: string, value: string): void {
    sessionStorage.setItem(key, value);
  }

  getSessionData(key: string): string | null {
    return sessionStorage.getItem(key);
  }

  removeSessionData(key: string): void {
    sessionStorage.removeItem(key);
  }

  clearSession(): void {
    sessionStorage.clear();
  }
}
