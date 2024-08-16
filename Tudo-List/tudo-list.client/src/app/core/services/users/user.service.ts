import { Injectable } from '@angular/core';
import { User } from '../../models/user/user';
import { ToastService } from '../../../features/services/toast/toast.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RequestService } from '../requestService/request.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrlLogin: string = 'api/Login';
  private apiUrlUser: string = 'api/Users';

  constructor(
    private toastService: ToastService,
    private http: HttpClient,
    private requestService: RequestService
  ) {}

  register(user: User): Observable<any> {
    const url: string = `${this.apiUrlUser}/register`;

    user.name = user.email.split('@')[0];
    return this.http.post(url, user);
  }

  login(user: User) {
    const url: string = `${this.apiUrlLogin}`;
    return this.http.post<string>(url, user);
  }

  getById(id: string) {
    const url: string = `${this.apiUrlUser}/get-by-id/${id}`;
    return this.http.get<User>(url, this.requestService.getHeader());
  }
}
