import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TodoListItem } from '../../models/todo-list-item/todo-list-item';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TodoListItemsRepository {
  apiUrl: string = 'api/TodoListItems';
  headers: HttpHeaders;

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders();
    this.headers = this.headers.append(
      'Authorization',
      `Bearer ${localStorage.getItem('token')}`
    );
  }

  getAll(): Observable<Array<TodoListItem>> {
    const url: string = `${this.apiUrl}/get-all`;

    return this.http.get<Array<TodoListItem>>(url, {
      headers: this.headers,
    });
  }

  getById(id: string): Observable<TodoListItem> {
    const url: string = `${this.apiUrl}/get-by-id/${id}`;

    return this.http.get<TodoListItem>(url, {
      headers: this.headers,
    });
  }

  add(todoListItem: TodoListItem) {
    const url: string = `${this.apiUrl}/add`;

    this.http.post(url, todoListItem, {
      headers: this.headers,
    });
  }

  update(todoListItem: TodoListItem) {
    const url: string = `${this.apiUrl}/update`;

    this.http.patch(url, todoListItem, {
      headers: this.headers,
    });
  }

  delete(id: string) {
    const url: string = `${this.apiUrl}/update/${id}`;

    this.http.delete(url, { headers: this.headers });
  }
}
