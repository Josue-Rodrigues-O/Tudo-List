import { Injectable } from '@angular/core';
import { TodoListItem } from '../../../core/models/todo-list-item/todo-list-item';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RequestService } from '../requestService/request.service';

@Injectable({
  providedIn: 'root',
})
export class TodoListItemService {
  private apiUrl: string = 'api/TodoListItems';

  constructor(
    private http: HttpClient,
    private requestService: RequestService
  ) {}

  getAll(): Observable<Array<TodoListItem>> {
    const url: string = `${this.apiUrl}/get-all`;
    return this.http.get<Array<TodoListItem>>(
      url,
      this.requestService.getHeader()
    );
  }

  getById(id: string): Observable<TodoListItem> {
    const url: string = `${this.apiUrl}/get-by-id/${id}`;
    return this.http.get<TodoListItem>(url, this.requestService.getHeader());
  }

  add(todoListItem: TodoListItem) {
    const url: string = `${this.apiUrl}/add`;

    return this.http.post(url, todoListItem, this.requestService.getHeader());
  }

  update(todoListItem: TodoListItem) {
    const url: string = `${this.apiUrl}/update`;

    todoListItem.itemId = todoListItem.id;
    return this.http.patch(url, todoListItem, this.requestService.getHeader());
  }

  delete(id: string) {
    const url: string = `${this.apiUrl}/delete/${id}`;

    return this.http.delete(url, this.requestService.getHeader());
  }
}
