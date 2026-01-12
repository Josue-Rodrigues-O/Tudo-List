import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../../services/user/user.service';
import { LoadingState } from '../../../../states/loading-state';
import { finalize } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../../../services/auth/auth.service';
import { UpdatePassword } from '../../../../core/models/user/update-password';

@Component({
  selector: 'app-user-edit-password',
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './user-edit-password.component.html',
  styleUrl: './user-edit-password.component.scss',
})
export class UserEditPasswordComponent implements OnInit {
  private user = this.authService.getCurrentUser();
  protected formPassword = new FormGroup({
    currentPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(256),
    ]),
    newPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(256),
    ]),
    confirmNewPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(256),
    ]),
  });

  constructor(
    private userService: UserService,
    private loadingState: LoadingState,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.formPassword.disable();
  }

  onClickSavePassword() {
    this.loadingState.show();
    this.formPassword.markAllAsTouched();
    if (this.formPassword.valid) {
      const userPassword = {
        ...this.formPassword.value,
        userId: this.user()!.id
      } as UpdatePassword;
      this.userService
        .updatePassword(userPassword)
        .pipe(finalize(() => this.loadingState.hide()))
        .subscribe({
          error: (err) => {
            console.error(err.error);
            alert(err.error.title);
          }
        });
    } else {
      this.loadingState.hide();
    }
  }

  onClickEditing() {
    this.formPassword.enable();
  }

  togglePassword(input: HTMLInputElement) {
    input.type = input.type === 'password' ? 'text' : 'password';
  }
}
