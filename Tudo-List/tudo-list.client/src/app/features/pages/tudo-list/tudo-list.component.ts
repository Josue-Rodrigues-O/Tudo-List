import { Component } from '@angular/core';
import { TodoListItem } from '../../../core/models/todo-list-item/todo-list-item';
import { StatusEnum } from '../../../core/enums/status-enum/status-enum';
import { TodoListItemService } from '../../../shared/services/todo-list-items/todo-list-item.service';
import { User } from '../../../core/models/user/user';
import { UserService } from '../../../shared/services/users/user.service';

@Component({
  selector: 'app-tudo-list',
  templateUrl: './tudo-list.component.html',
  styleUrl: './tudo-list.component.scss',
})
export class TudoListComponent {
  user: User = new User();
  tasks: Array<TodoListItem> = [];
  statusEnum = StatusEnum;
  selectedTask: TodoListItem = new TodoListItem();
  currentTask: TodoListItem = new TodoListItem();
  isEditing = false;

  constructor(
    private todoListItemService: TodoListItemService,
    userService: UserService
  ) {
    let token = localStorage.getItem('token')?.split('.')[1] || '';
    let id = JSON.parse(atob(token)).nameid;
    userService.getById(id).subscribe((res) => (this.user = res));
    this.updateTaskList();
  }

  onClickTaskStatusItem(
    value: StatusEnum,
    task: TodoListItem,
    statusSelect: any
  ) {
    task.status = value;
    switch (value) {
      case this.statusEnum.notStarted:
        statusSelect.value = 'Não iniciado';
        statusSelect.style.backgroundColor = '#F8D7DA';
        break;

      case this.statusEnum.inProgress:
        statusSelect.value = 'Em progresso';
        statusSelect.style.backgroundColor = '#CFE2FF';
        break;

      case this.statusEnum.completed:
        statusSelect.value = 'Completo';
        statusSelect.style.backgroundColor = '#D2F4EA';
        break;
    }
  }

  onSelectedTask(task: TodoListItem) {
    this.currentTask = { ...task };
    this.selectedTask = { ...task };
    this.isEditing = false;
  }

  onClickEditTask() {
    this.isEditing = true;
  }

  onClickRemoveTask() {
    this.remove();
  }

  onClickSave(id: string = '') {
    let index = id ? this.tasks.findIndex((x) => x.id == id) : -1;
    this.currentTask = id && index != -1 ? this.tasks[index] : this.currentTask;
    this.currentTask.id ? this.update() : this.add();

    this.isEditing = false;
  }

  onClickAddTask() {
    this.currentTask = new TodoListItem();
    this.currentTask.status = StatusEnum.notStarted;
    this.isEditing = true;
  }

  private updateTaskList() {
    this.todoListItemService
      .getAll()
      .subscribe((tasks) => (this.tasks = tasks));
  }

  private add() {
    this.todoListItemService.add(this.currentTask).subscribe({
      next: () => this.updateTaskList(),
      error: () => {},
      complete: () => {},
    });
  }

  private update() {
    this.todoListItemService.update(this.currentTask).subscribe({
      next: () => this.updateTaskList(),
      error: () => {},
      complete: () => {},
    });
  }

  private remove() {
    this.todoListItemService.delete(this.currentTask.id).subscribe({
      next: () => this.updateTaskList(),
      error: () => {},
      complete: () => {},
    });
  }
}
