import { Injectable } from '@angular/core';
import { Token } from '../../core/models/token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  setToken(token: Token) {
    localStorage.setItem('token', token.Token);
  }

  getToken() {
    return localStorage.getItem('token');
  }
}
