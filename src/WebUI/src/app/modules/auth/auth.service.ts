import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenKey = 'authToken';
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post(`User/login`, { userName: username, password: password, email:username  }).pipe(
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
  public getUserRoles(): string[] {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(this.getAuthToken() as string);
    if (decodedToken) {
        if (decodedToken.role) {
            return decodedToken.role.split(',').map((a: any) => a.trim());
        } else {
            return [];
        }
    } else {
        return [];
    }
}
  public isInRole(rolestr: string) {
    if (rolestr != undefined) {
        try {
            const roles = rolestr.split(',');
            let isInRole = false;
            const userRoles = this.getUserRoles().map(a => a?.toLowerCase());

            roles.forEach(role => {
                if (userRoles.indexOf(role.toLowerCase().trim()) > -1) {
                    isInRole = true;
                }
            });

            return isInRole;
        }
        catch(err) {
            return false;
        }

    } else {
        return false;
    }

}
  private storeAuthToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  private removeAuthToken(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
