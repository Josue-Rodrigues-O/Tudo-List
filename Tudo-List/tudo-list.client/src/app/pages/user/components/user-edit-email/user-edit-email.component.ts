import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../../../services/auth/auth.service';
import { UserService } from '../../../../services/user/user.service';
import { LoadingState } from '../../../../states/loading-state';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-user-edit-email',
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './user-edit-email.component.html',
  styleUrl: './user-edit-email.component.scss',
})
export class UserEditEmailComponent implements OnInit {
  private user = this.authService.getCurrentUser();

  protected userEmail: FormControl = new FormControl(this.user()?.email, [
    Validators.required,
    Validators.email
  ]);

  protected userPasswordUpdateEmail: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(256),
  ]);

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private loadingState: LoadingState) { }

  ngOnInit(): void {
    this.userEmail.disable();
  }

  onClickSaveEmail() {
    this.loadingState.show();
    if (this.userEmail.valid) {
      const currentUser = this.authService.getCurrentUser();
      const userUpdateEmail = {
        userId: currentUser()!.id,
        newEmail: this.userEmail.value,
        currentPassword: this.userPasswordUpdateEmail.value
      };
      this.userService
        .updateEmail(userUpdateEmail)
        .pipe(finalize(() => this.loadingState.hide()))
        .subscribe({
          next: () => {
            this.authService.setCurrentUser({ ...currentUser()!, email: this.userEmail.value });
          },
          error: (err) => {
            console.error(err.error);
            alert(err.error.title);
          }
        });
    }
    else {
      this.loadingState.hide();
    }
  }

  onClickEditing(formControl: FormControl) {
    formControl.enable();
  }

  togglePassword(input: HTMLInputElement) {
    input.type = input.type === 'password' ? 'text' : 'password';
  }
}
