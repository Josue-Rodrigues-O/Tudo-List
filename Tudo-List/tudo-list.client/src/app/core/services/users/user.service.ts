import { Injectable } from '@angular/core';
import { User } from '../../models/user/user';
import { ToastService } from '../../../features/services/toast/toast.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RequestService } from '../requestService/request.service';
import { UserValidatorService } from '../validations/userValidator/user-validator.service';
import { MessageBoxService } from '../../../features/services/message-box/message-box.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrlLogin: string = 'api/Login';
  private apiUrlUser: string = 'api/Users';

  constructor(
    private http: HttpClient,
    private requestService: RequestService,
    private toast: ToastService,
    private messageBox: MessageBoxService
  ) {}

  private validate(userValidator: UserValidatorService) {
    userValidator.setIsGenericMessage(false);
    let userValidation = userValidator.validate();

    if (!userValidation.isValid) {
      if (userValidation.isGenericMessage) {
        throw this.toast.show(userValidation.title, 'text-bg-danger');
      }
      throw this.messageBox.open(userValidation.title, userValidation.messages);
    }
  }

  private validationToRegister(user: User) {
    let userValidator = new UserValidatorService();
    userValidator.validationToRegister(user);
    this.validate(userValidator);
  }

  private validationToConnect(user: User) {
    let userValidator = new UserValidatorService();
    userValidator.validationToConnect(user);
    this.validate(userValidator);
  }

  register(user: User): Observable<any> {
    user.name = user.email.split('@')[0];
    this.validationToRegister(user);
    
    const url: string = `${this.apiUrlUser}/register`;
    return this.http.post(url, user);
  }

  login(user: User) {
    this.validationToConnect(user);

    const url: string = `${this.apiUrlLogin}`;
    return this.http.post<string>(url, user);
  }

  getById(id: string) {
    const url: string = `${this.apiUrlUser}/get-by-id/${id}`;
    return this.http.get<User>(url, this.requestService.getHeader());
  }
}
