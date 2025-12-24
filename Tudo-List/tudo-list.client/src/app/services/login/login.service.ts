import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../../core/models/login/login-request';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private readonly baseUrl: string = 'api/Login';

  constructor(private readonly httpClient: HttpClient) { }

  public login(user: LoginRequest) {
    const url = `${this.baseUrl}/login-async`;
    return this.httpClient.post<Token>(url, user);
  }

  public setToken(token: Token) {
    localStorage.setItem('token', token.token);
  }

  public getToken(): Token {
    return {
      token: localStorage.getItem("token") || ''
    }
  }

  public isAuthenticated(): boolean {
    return !!localStorage.getItem("token");
  }
}

interface Token {
  token: string;
}
