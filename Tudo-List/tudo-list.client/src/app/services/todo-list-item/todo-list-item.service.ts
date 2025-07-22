import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TodoListItemFilter } from '../../core/models/todo-list-item-filter';
import { TodoListItem } from '../../core/models/todo-list-item';

@Injectable({
  providedIn: 'root',
})
export class TodoListItemService {
  private uri = 'api/TodoListItems';
  constructor(private http: HttpClient) {}

  getAll(filter: TodoListItemFilter) {
    const url = `${this.uri}/get-all`;
    return this.http.get<TodoListItem[]>(url);
  }

  getById(id: string) {
    const url = `${this.uri}/get-by-id/${id}`;
    return this.http.get<TodoListItem>(url);
  }

  add(todoListItem: TodoListItem) {
    const url = `${this.uri}/add`;
    return this.http.post<void>(url, todoListItem);
  }

  update(todoListItem: TodoListItem) {
    const url = `${this.uri}/update`;
    return this.http.patch(url, todoListItem);
  }

  delete(id: string) {
    const url = `${this.uri}/delete/${id}`;
    return this.http.delete<void>(url);
  }
}
