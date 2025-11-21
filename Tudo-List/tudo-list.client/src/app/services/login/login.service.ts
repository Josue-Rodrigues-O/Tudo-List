import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../../core/models/login/login-request';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private readonly baseUrl: string = 'api/Login';

  constructor(private readonly httpClient: HttpClient) { }

  public Login(user: LoginRequest) {
    const url = `${this.baseUrl}/login-async`;
    return this.httpClient.post<{ token: string }>(url, user);
  }
}
