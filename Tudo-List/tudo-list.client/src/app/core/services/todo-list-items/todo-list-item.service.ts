import { Injectable } from '@angular/core';
import { TodoListItemsRepository } from '../../repositories/todo-list-items/todo-list-items-repository';
import { TodoListItem } from '../../models/todo-list-item/todo-list-item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TodoListItemService {
  constructor(private repository: TodoListItemsRepository) {}

  getAll(): Observable<Array<TodoListItem>> {
    return this.repository.getAll();
  }

  getById(id: string): Observable<TodoListItem> {
    return this.repository.getById(id);
  }

  add(todoListItem: TodoListItem) {
    this.repository.add(todoListItem);
  }

  update(todoListItem: TodoListItem) {
    this.repository.update(todoListItem);
  }

  delete(id: string) {
    this.repository.delete(id);
  }
}
