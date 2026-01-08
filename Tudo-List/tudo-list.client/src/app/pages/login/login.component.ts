import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl, Validators, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';
import { LoginRequest } from '../../core/models/login/login-request';
import { LoadingState } from '../../states/loading-state';
import { finalize, switchMap, tap } from 'rxjs';
import { AuthService } from '../../services/auth/auth.service';
import { UserService } from '../../services/user/user.service';
import { UserImageService } from '../../services/user-image/user-image.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  readonly email = new FormControl('', [
    Validators.required,
    Validators.email
  ]);
  readonly password = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(255),
  ]);

  constructor(
    private router: Router,
    private loginService: LoginService,
    private loadingState: LoadingState,
    private authService: AuthService,
    private userService: UserService,
    private userImgService: UserImageService) { }

  togglePassword(input: HTMLInputElement) {
    input.type = input.type === 'password' ? 'text' : 'password';
  }

  onClickLogin() {
    if (this.email.valid && this.password.valid) {
      let user: LoginRequest = {
        email: this.email.value || '',
        password: this.password.value || ''
      };
      this.login(user);
    }
  }

  onClickRegister() {
    this.router.navigate(['/register']);
  }

  private login(user: LoginRequest) {
    this.loadingState.show();

    this.loginService.login(user).pipe(
      tap(token => this.authService.setToken(token)),

      switchMap(() => {
        const decodedToken = this.authService.getDecodedToken();
        return this.userService.getById(decodedToken.nameid);
      }),
      tap(user => this.authService.setCurrentUser(user)),

      switchMap(user => this.userImgService.GetByUserId(user.id)),
      tap(img => this.userImgService.saveImgLocally(img)),
      finalize(() => this.loadingState.hide())
    ).subscribe({
      next: () => this.router.navigate(['']),
      error: (err) => {
        console.error(err.error);
        alert(err.error.title);
      }
    });
  }
}
