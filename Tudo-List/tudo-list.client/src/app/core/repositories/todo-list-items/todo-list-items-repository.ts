import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TodoListItem } from '../../models/todo-list-item/todo-list-item';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TodoListItemsRepository {
  apiUrl: string = 'api/TodoListItems';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Array<TodoListItem>> {
    const url: string = `${this.apiUrl}/get-all`;

    return this.http.get<Array<TodoListItem>>(url, {
      headers: this._getHeader(),
    });
  }

  getById(id: string): Observable<TodoListItem> {
    const url: string = `${this.apiUrl}/get-by-id/${id}`;

    return this.http.get<TodoListItem>(url, { headers: this._getHeader() });
  }

  add(todoListItem: TodoListItem) {
    const url: string = `${this.apiUrl}/add`;

    return this.http.post(url, todoListItem, { headers: this._getHeader() });
  }

  update(todoListItem: TodoListItem) {
    const url: string = `${this.apiUrl}/update`;

    todoListItem.itemId = todoListItem.id;
    return this.http.patch(url, todoListItem, { headers: this._getHeader() });
  }

  delete(id: string) {
    const url: string = `${this.apiUrl}/delete/${id}`;

    return this.http.delete(url, { headers: this._getHeader() });
  }

  _getHeader() {
    let headers!: HttpHeaders;

    headers = new HttpHeaders();
    headers = headers.append(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );

    return headers;
  }
}
