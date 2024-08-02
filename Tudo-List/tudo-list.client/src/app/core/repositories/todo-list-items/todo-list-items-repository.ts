import { StatusEnum } from '../../enums/status-enum/status-enum';
import { TodoListItem } from '../../models/todo-list-item/todo-list-item';

export class TodoListItemsRepository {
  static staticTodoListItems: Array<TodoListItem> = [
    {
      id: '1',
      title:
        'Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio urna a a a a sdsa as  ',
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
      status: StatusEnum.inProgress,
      priority: 0,
      creationDate: new Date(),
      userId: 0,
    },
    {
      id: '3',
      title: 'task 3',
      description: 'description 3',
      status: StatusEnum.completed,
      priority: 0,
      creationDate: new Date(),
      userId: 0,
    },
  ];

  constructor() {}

  getAll(): Array<TodoListItem> {
    return TodoListItemsRepository.staticTodoListItems;
  }

  getById(id: string): TodoListItem {
    return (
      TodoListItemsRepository.staticTodoListItems.find((x) => x.id == id) ??
      new TodoListItem()
    );
  }

  add(todoListItem: TodoListItem) {
    TodoListItemsRepository.staticTodoListItems.push(todoListItem);
  }

  update(todoListItem: TodoListItem) {
    let index: number = TodoListItemsRepository.staticTodoListItems.findIndex(
      (x) => x.id == todoListItem.id
    );
    TodoListItemsRepository.staticTodoListItems[index] = todoListItem;
  }

  delete(id: string) {
    let index: number = TodoListItemsRepository.staticTodoListItems.findIndex(
      (x) => x.id == id
    );
    TodoListItemsRepository.staticTodoListItems.splice(index, 1);
  }
}
