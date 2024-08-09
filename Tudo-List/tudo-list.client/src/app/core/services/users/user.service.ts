import { Injectable } from '@angular/core';
import { UserRepository } from '../../repositories/user/user-repository';
import { User } from '../../models/user/user';
import { ToastService } from '../../../features/services/toast/toast.service';
import { ValidationService } from '../validations/validation.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private regexEmail = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$/i;
  constructor(
    private repository: UserRepository,
    private toastService: ToastService,
    private validation: ValidationService
  ) {}

  register(
    user: User,
    next: Function = () => {},
    error: Function = () => {},
    complete: Function = () => {}
  ) {
    //#region validations
    if (!this.validation.notEmpty(user.email)) {
      this.toastService.show('O email não pode ser vazio', 'text-bg-danger');
      return;
    }
    if (!this.validation.match(user.email, this.regexEmail)) {
      this.toastService.show('O email é inválido', 'text-bg-danger');
      return;
    }
    if (!this.validation.notEmpty(user.password)) {
      this.toastService.show('A senha não pode ser vazia', 'text-bg-danger');
      return;
    }
    if (!this.validation.minLength(user.password, 8)) {
      this.toastService.show(
        'A senha de ter no mínimo 8 caracteres',
        'text-bg-danger'
      );
      return;
    }
    if (!this.validation.notEmpty(user.confirmPassword)) {
      this.toastService.show(
        'A confirmação de senha não pode ser vazia',
        'text-bg-danger'
      );
      return;
    }
    if (!this.validation.equal(user.password, user.confirmPassword)) {
      this.toastService.show(
        'A senha e a confirmação de senha devem ser iguais',
        'text-bg-danger'
      );
      return;
    }
    //#endregion
    user.name = user.email.split('@')[0];
    this.repository.register(user).subscribe({
      next: () => {
        this.login(user, next);
        this.toastService.show('Sucesso', 'text-bg-success');
      },
      error: (err) => {
        this.toastService.show('Erro', 'text-bg-danger');
        error();
      },
      complete: () => {
        complete();
      },
    });
  }

  login(
    user: User,
    next: Function = () => {},
    error: Function = () => {},
    complete: Function = () => {}
  ) {
    this.repository.login(user).subscribe({
      next: (res) => {
        this._setToken(res);
        this.toastService.show('Sucesso', 'text-bg-success');
        next();
      },
      error: (err) => {
        console.log(err)
        this.toastService.show('Erro', 'text-bg-danger');
        error();
      },
      complete: () => {
        complete();
      },
    });
  }

  getById(id: string) {
    return this.repository.getById(id);
  }

  _setToken(obj: any) {
    localStorage.setItem('token', obj.token);
  }
}
