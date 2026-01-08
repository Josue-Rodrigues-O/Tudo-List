import { Injectable, signal, WritableSignal } from '@angular/core';
import { Token } from '../../core/models/login/token';
import { User } from '../../core/models/user/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private user: WritableSignal<User | null> = signal(null);

  public setToken(token: Token) {
    localStorage.setItem('token', token.token);
  }

  public getToken(): Token {
    return {
      token: localStorage.getItem("token") || ''
    }
  }

  public getCurrentUser() {
    if (!this.user()) {
      const user = localStorage.getItem('user') || '';
      this.user.set(JSON.parse(user));
    }

    return this.user;
  }

  public setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.user.set(user);
  }

  public isAuthenticated(): boolean {
    return !!localStorage.getItem("token");
  }

  public logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    indexedDB.deleteDatabase('imgProfileDb');
  }

  public getDecodedToken(): DecodedToken {
    const token = this.getToken();
    const payloadBase64 = token.token.split('.')[1];
    const payloadBase64Decoded = payloadBase64
      .replace(/-/g, '+')
      .replace(/_/g, '/');

    const payloadDecoded = atob(payloadBase64Decoded);
    return JSON.parse(payloadDecoded);
  }
}

interface DecodedToken {
  nameid: string,
  unique_name: string,
  email: string,
}