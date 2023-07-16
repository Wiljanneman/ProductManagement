import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenKey = 'authToken';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post(`User/authenticate`, { userName: username, password: password, email:username  }).pipe(
      tap((response => {
        const authToken = (response as any).token;
        this.storeAuthToken(authToken);
        this.isAuthenticatedSubject.next(true);
      })
    ));
  }

  logout(): void {
    this.removeAuthToken();
    this.isAuthenticatedSubject.next(false);
  }

  isLoggedIn(): boolean {
    const authToken = this.getAuthToken();
    return !!authToken;
  }

  getAuthToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private storeAuthToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  private removeAuthToken(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
