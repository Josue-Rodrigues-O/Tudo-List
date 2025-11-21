import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../core/models/user/user';
import { RegisterUser } from '../../core/models/user/register-user';
import { UpdateUser } from '../../core/models/user/update-user';
import { UpdateEmail } from '../../core/models/user/update-email';
import { UpdatePassword } from '../../core/models/user/update-password';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly baseUrl: string = 'api/Users';

  constructor(private readonly httpClient: HttpClient) { }

  public GetAll() {
    const url = `${this.baseUrl}/get-all-async`;
    return this.httpClient.get<User[]>(url);
  }

  public GetById(id: string) {
    const url = `${this.baseUrl}/get-by-id-async/${id}`;
    return this.httpClient.get<User>(url);
  }

  public Register(user: RegisterUser) {
    const url = `${this.baseUrl}/register-async`;
    return this.httpClient.post<void>(url, user);
  }

  public Update(user: UpdateUser) {
    const url = `${this.baseUrl}/update-async`;
    return this.httpClient.patch<void>(url, user);
  }

  public UpdateEmail(user: UpdateEmail) {
    const url = `${this.baseUrl}/update-email-async`;
    return this.httpClient.patch<void>(url, user);
  }

  public UpdatePassword(user: UpdatePassword) {
    const url = `${this.baseUrl}/update-password-async`;
    return this.httpClient.patch<void>(url, user);
  }

  public Delete(id: string) {
    const url = `${this.baseUrl}/delete-async/${id}`;
    return this.httpClient.delete<void>(url);
  }
}
