import { Component } from '@angular/core';
import { User } from '../../../core/models/user/user';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { UserService } from '../../../core/services/users/user.service';
import { ToastService } from '../../services/toast/toast.service';
import { RequestService } from '../../../core/services/requestService/request.service';

@Component({
  selector: 'app-sign-in-sign-up',
  templateUrl: './sign-in-sign-up.component.html',
  styleUrl: './sign-in-sign-up.component.scss',
})
export class SignInSignUpComponent {
  user: User = new User();
  isSignIn: boolean = true;
  canChangeControls: boolean = false;

  constructor(
    private translate: TranslateService,
    private router: Router,
    private userService: UserService,
    private toastService: ToastService,
    private requestService: RequestService
  ) {
    this.translate.addLangs(['en', 'pt']);
    this.translate.setDefaultLang('en');

    const browserLang = this.translate.getBrowserLang();
    this.translate.use(
      browserLang && browserLang.match(/en|pt/) ? browserLang : 'en'
    );
  }

  onClickTextSignUpSignIn(form: HTMLElement, image: HTMLElement) {
    if (this.isSignIn) {
      form.className = 'form-sign-in animate-form-right';
      image.className = 'animate-img-left';
    } else {
      form.className = 'form-sign-in animate-form-left';
      image.className = 'animate-img-right';
    }
    setTimeout(() => {
      this.isSignIn = !this.isSignIn;
    }, 500);
  }

  estilizarLabel(evento: any, value: any) {
    if (!value) evento.classList.toggle('estilo-label');
  }

  onClickLogin() {
    this._login();
  }

  onClickRegister() {
    this._register();
  }

  _register() {
    this.userService.register(this.user).subscribe({
      next: () => {
        this._login();
        this.toastService.show('Sucesso', 'text-bg-success');
      },
      error: (err) => {
        this.toastService.show('Erro', 'text-bg-danger');
      },
    });
  }

  _login() {
    this.userService.login(this.user).subscribe({
      next: (res) => {
        this.requestService.setToken(res);
        this.toastService.show('Sucesso', 'text-bg-success');
        this.router.navigate(['/tudo-list']);
      },
      error: (err) => {
        console.log(err);
        this.toastService.show('Erro', 'text-bg-danger');
      },
    });
  }
}
