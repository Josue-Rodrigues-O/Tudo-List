import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RequestService {
  constructor() {}

  setToken(obj: any) {
    localStorage.setItem('token', obj.token);
  }

  getToken(): string {
    return localStorage.getItem('token') || '';
  }
}
