import { Component, signal } from '@angular/core';
import { ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';
import { LoginRequest } from '../../core/models/login/login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  hide = signal(true);
  readonly email = new FormControl('', [
    Validators.required,
    Validators.email
  ]);
  readonly password = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(256),
  ]);

  constructor(private router: Router, private loginService: LoginService) {

  }

  togglePassword() {
    this.hide.set(!this.hide());
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
    this.loginService.Login(user)
      .subscribe({
        next: (result) => {
          localStorage.setItem('token', result.token);
          this.router.navigate(['']);
        },
        error: (err) => {
          console.error(err.error);
          alert(err.error.title);
        }
      });
  }
}
