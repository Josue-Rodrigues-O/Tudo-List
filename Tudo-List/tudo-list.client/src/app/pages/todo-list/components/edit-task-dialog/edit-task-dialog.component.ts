import { Component, inject } from '@angular/core';
import {
  ReactiveFormsModule,
  FormControl,
  FormsModule,
  Validators,
  FormGroup,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogConfig,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { TodoListItemService } from '../../../../services/todo-list-item/todo-list-item.service';
import { AddItem } from '../../../../core/models/todo-list-item/add-item';
import { PriorityEnum } from '../../../../core/enums/priority-enum';
import { StatusEnum } from '../../../../core/enums/status-enum';
import { MatSelectModule } from '@angular/material/select';
import { LoadingState } from '../../../../states/loading-state';
import { finalize, switchMap, take } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { TodoListItem } from '../../../../core/models/todo-list-item/todo-list-item';
import { UpdateItem } from '../../../../core/models/todo-list-item/update-item';
import { MessageToastService } from '../../../../services/message-toast/message-toast.service';

@Component({
  selector: 'app-edit-task-dialog',
  standalone: true,
  templateUrl: './edit-task-dialog.component.html',
  styleUrl: './edit-task-dialog.component.scss',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    TranslateModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
})
export class EditTaskDialogComponent {
  readonly dialogRef = inject(MatDialogRef<EditTaskDialogComponent>);
  readonly data = inject<TodoListItem>(MAT_DIALOG_DATA);
  form: FormGroup = new FormGroup({
    title: new FormControl(this.data?.title, [Validators.required, Validators.maxLength(150)]),
    priority: new FormControl(this.data?.priority, [Validators.required]),
    status: new FormControl(this.data?.status, [Validators.required]),
    description: new FormControl(this.data?.description),
  });

  priorityEnum = PriorityEnum;
  statusEnum = StatusEnum;
  isEditMode: boolean = false;

  constructor(
    private todoListService: TodoListItemService,
    private loadingState: LoadingState,
    private messageToastService: MessageToastService,
    private translate: TranslateService,
    private route: ActivatedRoute) {
    this.form.disable();
  }

  onClickEdit() {
    this.isEditMode = true;
    this.form.enable();
  }

  onClickSave() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loadingState.show();

    const task: UpdateItem = {
      ...this.form.value,
      id: this.data.id
    };

    this.todoListService.Update(task).pipe(
      switchMap(() =>
        this.route.queryParams.pipe(take(1))
      ),
      switchMap(params => {
        const filter = {
          title: params['title'],
          priority: params['priority'],
          status: params['status']
        };

        return this.todoListService.loadItens$(filter);
      }),
      finalize(() => {
        this.loadingState.hide();
        this.dialogRef.close();
      })
    ).subscribe({
      next: () => this.messageToastService.show(this.translate.instant('todoList.messages.taskUpdatedSuccess'), 'success'),
      error: (err) => {
        this.messageToastService.show(err.error.title, 'error');
        console.error(err.error);
      }
    });
  }
}
