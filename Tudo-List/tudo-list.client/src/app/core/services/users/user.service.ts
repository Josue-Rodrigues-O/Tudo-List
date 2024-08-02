import { Injectable } from '@angular/core';
import { UserRepository } from '../../repositories/user/user-repository';
import { User } from '../../models/user/user';
import { AuthenticationResult } from '../../models/authentication-result/authentication-result';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private repository: UserRepository) {}

  register(user: User) {
    this.repository.register(user);
  }

  login(user: User) {
    this.repository.login(user).subscribe({
      next: (res) => this._setToken(res),
      error: (err) => console.log(err),
    });
  }

  _setToken(authenticationResult: AuthenticationResult) {
    localStorage.setItem('token', authenticationResult.token);
  }
}
