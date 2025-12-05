import { Component, inject } from '@angular/core';
import {
  ReactiveFormsModule,
  FormControl,
  FormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogActions,
  MatDialogClose,
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

@Component({
  selector: 'app-add-task-dialog',
  standalone: true,
  templateUrl: './add-task-dialog.component.html',
  styleUrl: './add-task-dialog.component.scss',
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
export class AddTaskDialogComponent {
  readonly dialogRef = inject(MatDialogRef<AddTaskDialogComponent>);
  title = new FormControl('', [Validators.required, Validators.maxLength(150)]);
  priority = new FormControl(PriorityEnum.Low, [Validators.required]);
  status = new FormControl(StatusEnum.NotStarted, [Validators.required]);
  description = new FormControl('');

  priorityEnum = PriorityEnum;
  statusEnum = StatusEnum;

  constructor(private todoListService: TodoListItemService, private loadingState: LoadingState) { }

  onClickSave() {
    this.loadingState.loading();
    if (this.title.valid && this.priority.valid && this.status.valid) {
      let task: AddItem = {
        title: this.title.value || '',
        priority: this.priority.value || PriorityEnum.Low,
        status: this.status.value || StatusEnum.NotStarted,
        description: this.description.value || '',
      };
      this.todoListService.Add(task)
        .pipe(finalize(() => {
          this.dialogRef.close();
          this.loadingState.hide()
        }))
        .subscribe({
          next: () => this.todoListService.loadItens(),
          error: (err) => {
            alert('Error adding task');
            console.log(err)
          },
        });
    } else {
      this.title.markAsTouched();
      this.priority.markAsTouched();
      this.status.markAsTouched();
      this.loadingState.hide();
    }
  }
}
