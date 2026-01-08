import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from '../../../../services/user/user.service';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import { AuthService } from '../../../../services/auth/auth.service';
import { LoadingState } from '../../../../states/loading-state';
import { finalize } from 'rxjs';
import { UserImageService } from '../../../../services/user-image/user-image.service';

@Component({
  selector: 'app-user-editing-form',
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './user-editing-form.component.html',
  styleUrl: './user-editing-form.component.scss',
})
export class UserEditingFormComponent implements OnInit, OnDestroy {
  previewUrl: string | null = null;
  imgFile: File | null = null;
  private user = this.authService.getCurrentUser();

  protected userName: FormControl = new FormControl(this.user()?.name, [
    Validators.required,
    Validators.maxLength(256)
  ]);

  protected userEmail: FormControl = new FormControl(this.user()?.email, [
    Validators.required,
    Validators.email
  ]);

  protected userPassword: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(256),
  ]);

  constructor(
    private userService: UserService,
    private userImgService: UserImageService,
    private authService: AuthService,
    private loadingState: LoadingState) { }

  async ngOnInit(): Promise<void> {
    this.previewUrl = await this.userImgService.getLocalImg();
  }

  ngOnDestroy(): void {
    if (this.previewUrl) {
      URL.revokeObjectURL(this.previewUrl);
    }
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;

    if (!input.files || input.files.length === 0)
      return;

    const file = input.files[0];

    if (!file.type.startsWith('image/')) {
      alert('Selecione uma imagem válida');
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      this.previewUrl = reader.result as string;
    };

    reader.readAsDataURL(file);
    this.imgFile = file;
  }

  onClickClearImg(input: HTMLInputElement) {
    this.previewUrl = null;
    input.value = '';
    this.imgFile = null;
  }

  onClickSaveImg() {
    this.loadingState.show();
    if (!this.imgFile) {
      alert('Selecione uma imagem antes de salvar!');
      this.loadingState.hide();
      return;
    }

    this.userImgService
      .Upload(this.imgFile)
      .pipe(finalize(() => this.loadingState.hide()))
      .subscribe({
        next: async (result) => {
          console.log(result instanceof Blob);
          await this.userImgService.saveImgLocally(result);
        },
        error: (err) => {
          console.error(err.error ?? err);
          alert(err.error.title);
        }
      });
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
          },
          error: (err) => {
            console.error(err.error);
            alert(err.error.title);
          }
        });
    else
      this.loadingState.hide();
  }

  onClickSaveEmail() {
    this.loadingState.show();
    if (this.userEmail.valid)
      this.userService
        .updateEmail(this.userEmail.value)
        .pipe(finalize(() => this.loadingState.hide()))
        .subscribe({
          next: () => {
            const currentUser = this.authService.getCurrentUser();
            this.authService.setCurrentUser({ ...currentUser()!, email: this.userEmail.value });
          },
          error: (err) => {
            console.error(err.error);
            alert(err.error.title);
          }
        });
    else
      this.loadingState.hide();
  }

  onClickSavePassword() {
    this.loadingState.show();
    if (this.userPassword.valid)
      this.userService
        .updatePassword(this.userPassword.value)
        .pipe(finalize(() => this.loadingState.hide()))
        .subscribe({
          error: (err) => {
            console.error(err.error);
            alert(err.error.title);
          }
        });
    else
      this.loadingState.hide();
  }

  togglePassword(input: HTMLInputElement) {
    input.type = input.type === 'password' ? 'text' : 'password';
  }

}
