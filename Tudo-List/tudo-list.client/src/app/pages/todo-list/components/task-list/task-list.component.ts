import { TodoListItemService } from '../../../../services/todo-list-item/todo-list-item.service';
import { TodoListItem } from '../../../../core/models/todo-list-item/todo-list-item';
import { TranslateModule } from '@ngx-translate/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { Component, inject, Signal } from '@angular/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { StatusPipe } from '../../../../pipes/status/status.pipe';
import { PriorityPipe } from '../../../../pipes/priority/priority.pipe';
import { ActivatedRoute, Params } from '@angular/router';
import { LoadingState } from '../../../../states/loading-state';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from "@angular/material/icon";
import { A11yModule } from "@angular/cdk/a11y";
import { MatDialog } from '@angular/material/dialog';
import { EditTaskDialogComponent } from '../edit-task-dialog/edit-task-dialog.component';

@Component({
  selector: 'app-task-list',
  standalone: true,
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss',
  imports: [
    TranslateModule,
    MatExpansionModule,
    MatButtonModule,
    MatTooltipModule,
    MatTableModule,
    StatusPipe,
    PriorityPipe,
    MatIconModule,
    A11yModule
  ],
})
export class TaskListComponent {
  readonly dialog: MatDialog = inject(MatDialog);
  readonly tasks: Signal<TodoListItem[]> = this.todoListService.items;
  displayedColumns: string[] = ['title', 'priority', 'status'];

  constructor(private todoListService: TodoListItemService, route: ActivatedRoute, private loadingState: LoadingState) {
    loadingState.show();
    route.queryParams.subscribe(params => this.loadItemsWithFilters(params));
  }

  private loadItemsWithFilters(params: Params) {
    const filter = {
      title: params['title'],
      priority: params['priority'],
      status: params['status']
    };
    this.todoListService.loadItens(filter, () => this.loadingState.hide());
  }

  onClickTask(task: TodoListItem): void {
    const dialogRef = this.dialog.open(EditTaskDialogComponent, {
      height: '500px',
      width: '600px',
      data: task
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
    });
  }
}
