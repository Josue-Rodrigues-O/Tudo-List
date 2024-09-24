import { Component, ViewChild } from '@angular/core';
import { User } from '../../../core/models/user/user';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { UserService } from '../../../core/services/users/user.service';
import { ToastService } from '../../services/toast/toast.service';
import { RequestService } from '../../../core/services/requestService/request.service';
import { InputComponent } from '../../components/input/input.component';
import { MessageBoxService } from '../../services/message-box/message-box.service';
import { ProblemDetailsMessagesService } from '../../../core/services/problemDetailsMessages/problem-details-messages.service';
import { ValueStateEnum } from '../../../core/enums/value-state/valueState-enum';

@Component({
  selector: 'app-sign-in-sign-up',
  templateUrl: './sign-in-sign-up.component.html',
  styleUrl: './sign-in-sign-up.component.scss',
})
export class SignInSignUpComponent {
  @ViewChild('inpEmail') inpEmail!: InputComponent;
  @ViewChild('inpPassword') inpPassword!: InputComponent;
  @ViewChild('inpConrfirmPassword') inpConrfirmPassword!: InputComponent;
  user: User = new User();
  isSignIn: boolean = true;
  canChangeControls: boolean = false;

  constructor(
    private router: Router,
    private userService: UserService,
    private toastService: ToastService,
    private messageBox: MessageBoxService,
    private translate: TranslateService,
    private requestService: RequestService,
    private problemDetailsMessagesService: ProblemDetailsMessagesService
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
    this.user.email = '';
    this.user.password = '';
    this.user.confirmPassword = '';
    this.inpEmail.setValueState(ValueStateEnum.none);
    this.inpPassword.setValueState(ValueStateEnum.none);
    this.inpConrfirmPassword?.setValueState(ValueStateEnum.none);
    setTimeout(() => {
      this.isSignIn = !this.isSignIn;
    }, 500);
  }

  estilizarLabel(evento: any, value: any) {
    if (!value) evento.classList.toggle('estilo-label');
  }

  onClickLogin() {
    this.bindFieldsForUserValidation();
    this.login();
  }

  onClickRegister() {
    this.bindFieldsForUserValidation();
    this.register();
  }

  private bindFieldsForUserValidation() {
    this.userService.bindFieldsForUserValidation({
      email: this.inpEmail,
      password: this.inpPassword,
      confirmPassword: this.inpConrfirmPassword,
    });
  }

  private register() {
    this.userService.register(this.user).subscribe({
      next: () => {
        this.login();
        this.toastService.show('Sucesso', ValueStateEnum.success);
      },
      error: (err) => {
        let errors = this.problemDetailsMessagesService.getMessages(err.error);
        this.messageBox.open(errors.title, errors.messages);
      },
    });
  }

  private login() {
    this.userService.login(this.user).subscribe({
      next: (res) => {
        this.requestService.setToken(res);
        this.toastService.show('Sucesso', ValueStateEnum.success);
        this.router.navigate(['/tudo-list']);
      },
      error: (err) => {
        let errors = this.problemDetailsMessagesService.getMessages(err.error);
        this.messageBox.open(errors.title, errors.messages);
      },
    });
  }
}
