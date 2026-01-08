import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../core/models/user/user';
import { RegisterUser } from '../../core/models/user/register-user';
import { UpdateUser } from '../../core/models/user/update-user';
import { UpdateEmail } from '../../core/models/user/update-email';
import { UpdatePassword } from '../../core/models/user/update-password';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly baseUrl: string = 'api/Users';

  constructor(private readonly httpClient: HttpClient, private readonly authService: AuthService) { }

  public getAll() {
    const url = `${this.baseUrl}/get-all-async`;
    return this.httpClient.get<User[]>(url);
  }

  public getById(id: string) {
    const url = `${this.baseUrl}/get-by-id-async/${id}`;
    return this.httpClient.get<User>(url);
  }

  public register(user: RegisterUser) {
    const url = `${this.baseUrl}/register-async`;
    return this.httpClient.post<void>(url, user);
  }

  public updateName(userName: string) {
    const decodedToken = this.authService.getDecodedToken();
    const user: UpdateUser = {
      userId: parseInt(decodedToken.nameid),
      newName: userName
    };
    const url = `${this.baseUrl}/update-async`;
    return this.httpClient.patch<void>(url, user);
  }

  public updateEmail(user: UpdateEmail) {
    const url = `${this.baseUrl}/update-email-async`;
    return this.httpClient.patch<void>(url, user);
  }

  public updatePassword(user: UpdatePassword) {
    const url = `${this.baseUrl}/update-password-async`;
    return this.httpClient.patch<void>(url, user);
  }

  public delete(id: string) {
    const url = `${this.baseUrl}/delete-async/${id}`;
    return this.httpClient.delete<void>(url);
  }
}
