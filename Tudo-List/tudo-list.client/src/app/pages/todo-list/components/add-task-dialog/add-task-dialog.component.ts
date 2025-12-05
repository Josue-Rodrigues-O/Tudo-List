import { Component } from '@angular/core';
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
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TranslateModule } from '@ngx-translate/core';
import { TodoListItemService } from '../../../../services/todo-list-item/todo-list-item.service';
import { AddItem } from '../../../../core/models/todo-list-item/add-item';
import { PriorityEnum } from '../../../../core/enums/priority-enum';
import { StatusEnum } from '../../../../core/enums/status-enum';

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
  ],
})
export class AddTaskDialogComponent {
  title = new FormControl('', [Validators.required]);
  priority = new FormControl(PriorityEnum.Low);
  status = new FormControl(StatusEnum.NotStarted)

  constructor(private todoListService: TodoListItemService) {}

  onClickSave() {
    if (this.title.valid) {
      let task: AddItem = {
        title: this.title.value || '',
        priority: this.priority.value || PriorityEnum.Low,
        status: this.status.
  
      };
      this.todoListService.Add(task);
    }
  }
}
