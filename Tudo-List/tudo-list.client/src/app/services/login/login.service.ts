import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../../core/models/login/login-request';
import { Token } from '../../core/models/login/token';

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
}
