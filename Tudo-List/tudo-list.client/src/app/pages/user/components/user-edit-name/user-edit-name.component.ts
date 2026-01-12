import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import { AuthService } from '../../../../services/auth/auth.service';
import { UserService } from '../../../../services/user/user.service';
import { LoadingState } from '../../../../states/loading-state';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MessageToastService } from '../../../../services/message-toast/message-toast.service';

@Component({
  selector: 'app-user-edit-name',
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './user-edit-name.component.html',
  styleUrl: './user-edit-name.component.scss',
})
export class UserEditNameComponent implements OnInit {
  private user = this.authService.getCurrentUser();
  protected userName: FormControl = new FormControl(this.user()?.name, [
    Validators.required,
    Validators.maxLength(256)
  ]);

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private messageToastService: MessageToastService,
    private translate: TranslateService,
    private loadingState: LoadingState) { }

  ngOnInit(): void {
    this.userName.disable();
  }

  onClickSaveName() {
    this.loadingState.show();
    if (this.userName.valid)
      this.userService
        .updateName(this.userName.value)
        .pipe(finalize(() => this.loadingState.hide()))
        .subscribe({
          next: () => {
            const currentUser = this.authService.getCurrentUser();
            this.authService.setCurrentUser({ ...currentUser()!, name: this.userName.value });
            this.messageToastService.show(this.translate.instant('userEdit.messages.nameUpdatedSuccess'), 'success');
          },
          error: (err) => {
            this.messageToastService.show(err.error.title, 'error');
            console.error(err.error);
          }
        });
    else
      this.loadingState.hide();
  }

  onClickEditing(formControl: FormControl) {
    formControl.enable();
  }
}
