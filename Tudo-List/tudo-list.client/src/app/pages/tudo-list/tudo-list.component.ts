import { Component } from '@angular/core';
import { TodoListItem } from '../../models/todo-list-item/todo-list-item';
import { StatusEnum } from '../../models/enums/status-enum/status-enum';

@Component({
  selector: 'app-tudo-list',
  templateUrl: './tudo-list.component.html',
  styleUrl: './tudo-list.component.scss',
})
export class TudoListComponent {
  statusEnum = StatusEnum;

  tasks: Array<TodoListItem> = [
    {
      id: '1',
      title: 'task 1',
      description: 'description 1',
      status: 0,
      priority: 0,
      creationDate: new Date(),
      userId: 0,
    },
    {
      id: '2',
      title: 'task 2',
      description: 'description 2',
      status: 0,
      priority: 0,
      creationDate: new Date(),
      userId: 0,
    },
    {
      id: '3',
      title: 'task 3',
      description: 'description 3',
      status: 0,
      priority: 0,
      creationDate: new Date(),
      userId: 0,
    },
  ];
  
  onClickTaskStatusItem(
    value: StatusEnum,
    task: TodoListItem,
    statusSelect: any
  ) {
    task.status = value;
    switch (value) {
      case this.statusEnum.notStarted:
        statusSelect.value = 'Não iniciado';
        statusSelect.style.backgroundColor = '#F1AEB5';
        break;

      case this.statusEnum.inProgress:
        statusSelect.value = 'Em progresso';
        statusSelect.style.backgroundColor = '#9EC5FE';
        break;

      case this.statusEnum.completed:
        statusSelect.value = 'Completo';
        statusSelect.style.backgroundColor = '#A6E9D5';
        break;
    }
  }
}
