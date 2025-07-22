import { Injectable } from '@angular/core';
import { User } from '../../core/models/user';
import { HttpClient } from '@angular/common/http';
import { Token } from '../../core/models/token';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private uriUser = 'api/User';
  private uriLogin = 'api/Login';

  constructor(private http: HttpClient) {}

  registe(user: User) {
    const url = `${this.uriUser}/register`;
    return this.http.post<void>(url, user);
  }

  login(user: User) {
    const url = this.uriLogin;
    return this.http.post<Token>(url, user);
  }

  getById(id: string) {
    const url = `${this.uriUser}/get-by-id/${id}`;
    return this.http.get<User>(url);
  }
}
