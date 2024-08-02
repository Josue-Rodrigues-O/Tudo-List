import { Injectable } from '@angular/core';
import { User } from '../../models/user/user';
import { HttpClient } from '@angular/common/http';
import { AuthenticationResult } from '../../models/authentication-result/authentication-result';

@Injectable({
  providedIn: 'root',
})
export class UserRepository {
  apiUrlLogin: string = 'api/Login';
  apiUrlUser: string = 'api/Users';

  constructor(private http: HttpClient) {}

  register(user: User) {
    return this.http.post(`${this.apiUrlUser}/register`, user);
  }

  login(user: User) {
    return this.http.post<AuthenticationResult>(`${this.apiUrlLogin}`, user);
  }
}
