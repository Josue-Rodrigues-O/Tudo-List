import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AddTaskDialogComponent } from './components/add-task-dialog/add-task-dialog.component';
import { FilterPanelComponent } from './components/filter-panel/filter-panel.component';
import { TaskListComponent } from "./components/task-list/task-list.component";

@Component({
  selector: 'app-todo-list',
  standalone: true,
  templateUrl: './todo-list.component.html',
  styleUrl: './todo-list.component.scss',
  imports: [
    MatIconModule,
    TranslateModule,
    MatButtonModule,
    MatTooltipModule,
    FilterPanelComponent,
    TaskListComponent,
  ],
})
export class TodoListComponent {
  readonly dialog: MatDialog = inject(MatDialog);

  constructor() { }

  onClickAddTask(): void {
    const dialogRef = this.dialog.open(AddTaskDialogComponent, {
      height: '500px',
      width: '600px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
