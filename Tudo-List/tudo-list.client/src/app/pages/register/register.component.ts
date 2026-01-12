import { Component, signal } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { UserService } from '../../services/user/user.service';
import { LoginRequest } from '../../core/models/login/login-request';
import { RegisterUser } from '../../core/models/user/register-user';
import { LoginService } from '../../services/login/login.service';
import { LoadingState } from '../../states/loading-state';
import { finalize, switchMap, tap } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';
import { Token } from '../../core/models/login/token';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
})
export class RegisterComponent {
  readonly email = new FormControl('', [
    Validators.required,
    Validators.email
  ]);
  readonly password = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(255),
  ]);
  readonly confirmPassword = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(255),
  ]);

  constructor(
    private router: Router,
    private userService: UserService,
    private loginService: LoginService,
    private loadingState: LoadingState,
    private authService: AuthService) { }

  togglePassword(input: HTMLInputElement) {
    input.type = input.type === 'password' ? 'text' : 'password';
  }

  onClickLogin() {
    this.router.navigate(['/login']);
  }

  onClickRegister() {
    if (this.email.valid && this.password.valid) {
      let user: RegisterUser = {
        email: this.email.value || '',
        password: this.password.value || '',
        confirmPassword: this.confirmPassword.value || '',
        name: (this.email.value || '').split('@')[0]
      };

      this.register(user);
    }
  }

  private register(user: RegisterUser) {
    this.loadingState.show();
    this.userService
      .register(user)
      .pipe(
        switchMap(() => this.loginService.login(user)),
        tap(token => this.authService.setToken(token)),
        switchMap(() => {
          const decodedToken = this.authService.getDecodedToken();
          return this.userService.getById(decodedToken.nameid);
        }),
        tap(user => this.authService.setCurrentUser(user)),
        finalize(() => this.loadingState.hide()))
      .subscribe({
        next: () => {
          this.router.navigate(['']);
        },
        error: (err) => {
          console.error(err.error);
          alert(err.error.title);
        }
      });
  }
}
