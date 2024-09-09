import { ElementRef, Injectable } from '@angular/core';
import { User } from '../../models/user/user';
import { ToastService } from '../../../features/services/toast/toast.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RequestService } from '../requestService/request.service';
import { UserValidatorService } from '../validations/userValidator/user-validator.service';
import { MessageBoxService } from '../../../features/services/message-box/message-box.service';
import { FieldsForUserValidation } from '../validations/userValidator/fields-for-user-validation';
import { ValueStateEnum } from '../../enums/value-state/valueState-enum';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private fieldsForUserValidation!: FieldsForUserValidation;

  private apiUrlLogin: string = 'api/Login';
  private apiUrlUser: string = 'api/Users';

  constructor(
    private http: HttpClient,
    private requestService: RequestService,
    private toast: ToastService,
    private messageBox: MessageBoxService
  ) {}

  private validate(userValidator: UserValidatorService) {
    userValidator.setIsGenericMessage(true);
    let userValidation = userValidator.validate();

    if (!userValidation.isValid) {
      throw userValidation.isGenericMessage
        ? this.toast.show(userValidation.title, ValueStateEnum.error)
        : this.messageBox.open(userValidation.title, userValidation.messages);
    }
  }

  private validationToRegister(user: User) {
    let userValidator = new UserValidatorService(this.fieldsForUserValidation);
    userValidator.validationToRegister(user);
    this.validate(userValidator);
  }

  private validationToConnect(user: User) {
    let userValidator = new UserValidatorService(this.fieldsForUserValidation);
    userValidator.validationToConnect(user);
    this.validate(userValidator);
  }

  bindFieldsForUserValidation(
    fieldsForUserValidation: FieldsForUserValidation
  ) {
    this.fieldsForUserValidation = fieldsForUserValidation;
  }

  register(user: User): Observable<any> {
    user.name = `_${user.email.split('@')[0]}`;
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
