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
import { TranslateModule } from '@ngx-translate/core';
import { TodoListItemService } from '../../../../services/todo-list-item/todo-list-item.service';
import { AddItem } from '../../../../core/models/todo-list-item/add-item';
import { PriorityEnum } from '../../../../core/enums/priority-enum';
import { StatusEnum } from '../../../../core/enums/status-enum';
import { MatSelectModule } from '@angular/material/select';
import { LoadingState } from '../../../../states/loading-state';
import { finalize } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { TodoListItem } from '../../../../core/models/todo-list-item/todo-list-item';
import { UpdateItem } from '../../../../core/models/todo-list-item/update-item';

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
    private route: ActivatedRoute) {
    this.form.disable();
  }

  onClickEdit() {
    this.isEditMode = true;
    this.form.enable();
  }

  onClickSave() {
    this.loadingState.show();
    if (this.form.valid) {
      let task: UpdateItem = this.form.value;
      task.id = this.data.id;
      this.todoListService.Update(task)
        .pipe(finalize(() => this.dialogRef.close()))
        .subscribe({
          next: () => this.loadItemsWithFilters(),
          error: (err) => {
            this.loadingState.hide();
            alert('Error adding task');
            console.log(err)
          },
        });
    } else {
      this.form.markAsTouched();
      this.loadingState.hide();
    }
  }

  private loadItemsWithFilters() {
    this.route.queryParams.subscribe(params => {
      const filter = {
        title: params['title'],
        priority: params['priority'],
        status: params['status']
      };
      this.todoListService.loadItens(filter, () => this.loadingState.hide());
    });
  }
}
