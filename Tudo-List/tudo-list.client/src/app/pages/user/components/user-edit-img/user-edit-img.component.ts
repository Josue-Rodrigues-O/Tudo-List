import { Component, OnDestroy, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { finalize } from 'rxjs';
import { UserImageService } from '../../../../services/user-image/user-image.service';
import { LoadingState } from '../../../../states/loading-state';
import { MessageToastService } from '../../../../services/message-toast/message-toast.service';

@Component({
  selector: 'app-user-edit-img',
  imports: [TranslateModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule, MatButtonModule],
  templateUrl: './user-edit-img.component.html',
  styleUrl: './user-edit-img.component.scss',
})
export class UserEditImgComponent implements OnInit, OnDestroy {
  previewUrl: string | null = null;
  imgFile: File | null = null;

  constructor(
    private userImgService: UserImageService,
    private messageToastService: MessageToastService,
    private translate: TranslateService,
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
          this.messageToastService.show(this.translate.instant('userEdit.messages.avatarUpdatedSuccess'), 'success');
        },
        error: (err) => {
          this.messageToastService.show(err.error.title, 'error');
          console.error(err.error);
        }
      });
  }
}
