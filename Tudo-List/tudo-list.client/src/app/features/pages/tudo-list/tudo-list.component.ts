import { Component } from '@angular/core';
import { TodoListItem } from '../../../core/models/todo-list-item/todo-list-item';
import { StatusEnum } from '../../../core/enums/status-enum/status-enum';
import { TodoListItemService } from '../../../core/services/todo-list-items/todo-list-item.service';

@Component({
  selector: 'app-tudo-list',
  templateUrl: './tudo-list.component.html',
  styleUrl: './tudo-list.component.scss',
})
export class TudoListComponent {
  tasks: Array<TodoListItem> = [];
  statusEnum = StatusEnum;
  selectedTask: TodoListItem = new TodoListItem();
  currentTask: TodoListItem = new TodoListItem();
  isEditing = false;

  constructor(private todoListItemService: TodoListItemService) {
    this.todoListItemService
      .getAll()
      .subscribe((tasks) => (this.tasks = tasks));
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
    this.todoListItemService.delete(this.currentTask.id);
  }

  onClickSave() {
    this.todoListItemService.add(this.currentTask);
    // this.currentTask.id
    //   ? this.todoListItemService.update(this.currentTask)
    //   : this.todoListItemService.add(this.currentTask);

    this.isEditing = false;
  }

  onClickCancelEdit() {
    this.isEditing = false;
  }

  onClickAddTask() {
    this.currentTask = new TodoListItem();
    this.currentTask.status = StatusEnum.notStarted;
    this.isEditing = true;
  }
}
