import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RequestService {
  constructor() {}

  getHeader() {
    let headers!: HttpHeaders;

    headers = new HttpHeaders();
    headers = headers.append('Authorization', `Bearer ${this.getToken()}`);

    return { headers: headers };
  }

  setToken(obj: any) {
    localStorage.setItem('token', obj.token);
  }

  getToken() {
    return localStorage.getItem('token');
  }
}
