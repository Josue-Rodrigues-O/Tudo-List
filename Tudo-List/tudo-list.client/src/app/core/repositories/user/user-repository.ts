import { Injectable } from '@angular/core';
import { User } from '../../models/user/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserRepository {
  apiUrlLogin: string = 'api/Login';
  apiUrlUser: string = 'api/Users';

  constructor(private http: HttpClient) {}

  register(user: User) {
    const url: string = `${this.apiUrlUser}/register`;
    return this.http.post(url, user);
  }

  login(user: User) {
    const url: string = `${this.apiUrlLogin}`;
    return this.http.post<string>(url, user);
  }

  getById(id: string) {
    const url: string = `${this.apiUrlUser}/get-by-id/${id}`;
    return this.http.get<User>(url, { headers: this._getHeader() });
  }

  _getHeader() {
    let headers!: HttpHeaders;

    headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );

    return headers;
  }
}
