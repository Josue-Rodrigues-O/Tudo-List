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

  add(
    todoListItem: TodoListItem,
    next: Function = () => {},
    error: Function = () => {},
    complete: Function = () => {}
  ) {
    this.repository.add(todoListItem).subscribe({
      next: () => {
        next();
      },
      error: () => {
        error();
      },
      complete: () => {
        complete();
      },
    });
  }

  update(
    todoListItem: TodoListItem,
    next: Function = () => {},
    error: Function = () => {},
    complete: Function = () => {}
  ) {
    this.repository.update(todoListItem).subscribe({
      next: () => {
        next();
      },
      error: () => {
        error();
      },
      complete: () => {
        complete();
      },
    });
  }

  delete(
    id: string,
    next: Function = () => {},
    error: Function = () => {},
    complete: Function = () => {}
  ) {
    this.repository.delete(id).subscribe({
      next: () => {
        next();
      },
      error: () => {
        error();
      },
      complete: () => {
        complete();
      },
    });
  }
}
