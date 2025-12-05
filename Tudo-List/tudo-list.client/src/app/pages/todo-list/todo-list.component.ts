import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { TodoListItemService } from '../../services/todo-list-item/todo-list-item.service';
import { TodoListItem } from '../../core/models/todo-list-item/todo-list-item';
import { PriorityEnum } from '../../core/enums/priority-enum';
import { StatusEnum } from '../../core/enums/status-enum';
import { TranslateModule } from '@ngx-translate/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AddTaskDialogComponent } from './components/add-task-dialog/add-task-dialog.component';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.scss',
  imports: [
    MatToolbarModule,
    MatIconModule,
    TranslateModule,
    MatExpansionModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
  ],
})
export class TodoListComponent {
  readonly dialog = inject(MatDialog);
  tasks: TodoListItem[] = [
    {
      id: 'asdasdasd',
      creationDate: new Date(),
      priority: PriorityEnum.High,
      status: StatusEnum.InProgress,
      title: 'Testessss',
      description: 'Descrição para testessssss',
    },
    {
      id: 'asdasdasd',
      creationDate: new Date(),
      priority: PriorityEnum.High,
      status: StatusEnum.InProgress,
      title: 'Testessss',
      description: 'Descrição para testessssss',
    },
  ];
  constructor(private todoListService: TodoListItemService) {
    // this.UpdateList();
  }

  onClickAddTask(): void {
    const dialogRef = this.dialog.open(AddTaskDialogComponent, {});

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }

  private UpdateList() {
    this.todoListService.GetAll().subscribe({
      next: (result) => {
        this.tasks = result;
        console.log(this.tasks.length);
      },
    });
  }
}
