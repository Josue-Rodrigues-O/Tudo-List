import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl, Validators, FormsModule, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';
import { LoginRequest } from '../../core/models/login/login-request';
import { LoadingState } from '../../states/loading-state';
import { catchError, finalize, map, of, switchMap, tap, throwError } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';
import { UserService } from '../../services/user/user.service';
import { UserImageService } from '../../services/user-image/user-image.service';
import { MessageToastService } from '../../services/message-toast/message-toast.service';
import { User } from '../../core/models/user/user';
import { Token } from '../../core/models/login/token';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  protected readonly formLogin = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(255),
    ])
  });

  constructor(
    private router: Router,
    private loginService: LoginService,
    private loadingState: LoadingState,
    private authService: AuthService,
    private userService: UserService,
    private messageToastService: MessageToastService,
    private translate: TranslateService,
    private userImgService: UserImageService) { }

  togglePassword(input: HTMLInputElement) {
    input.type = input.type === 'password' ? 'text' : 'password';
  }

  onClickLogin() {
    this.formLogin.markAsTouched();
    if (this.formLogin.valid) {
      let user = this.formLogin.value as LoginRequest;
      this.login(user);
    }
  }

  onClickRegister() {
    this.router.navigate(['/auth/register']);
  }

  private login(user: LoginRequest) {
    this.loadingState.show();

    this.loginService.login(user).pipe(
      tap((token: Token) => this.authService.setToken(token)),

      switchMap(() => {
        const decodedToken = this.authService.getDecodedToken();
        debugger
        return this.userService.getById(decodedToken.nameid);
      }),
      tap((user: User) => this.authService.setCurrentUser(user)),

      switchMap((user: User) => {
        return this.userImgService
          .GetByUserId(user.id)
          .pipe(
            tap((img: Blob) => this.userImgService.saveImgLocally(img)),
            catchError(err => {
              if (err.status === 404) {
                this.userImgService.clearImgLocally();
                return of(null);
              }

              if (err.headers?.get('content-type')?.includes('image')) {
                return of(null);
              }

              return throwError(() => err);
            }),
            map(() => user)
          );
      }),
      finalize(() => this.loadingState.hide())
    ).subscribe({
      next: () => this.router.navigate(['']),
      error: (err) => {
        this.messageToastService.show(this.translate.instant('login.messages.loginError'), 'error');
        console.error(err.error);
      }
    });
  }
}
