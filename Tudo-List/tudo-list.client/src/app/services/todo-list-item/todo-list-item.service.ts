import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, signal, Signal } from '@angular/core';
import { TodoListItem } from '../../core/models/todo-list-item/todo-list-item';
import { TodoListItemQueryFilter } from '../../core/models/todo-list-item/todo-list-item-query-filter';
import { AddItem } from '../../core/models/todo-list-item/add-item';
import { UpdateItem } from '../../core/models/todo-list-item/update-item';

@Injectable({
  providedIn: 'root',
})
export class TodoListItemService {
  private readonly baseUrl: string = 'api/TodoListItems';
  private readonly itemsSignal = signal<TodoListItem[]>([]);
  public readonly items: Signal<TodoListItem[]> = this.itemsSignal.asReadonly();

  constructor(private readonly httpClient: HttpClient) { }

  public loadItens(filter?: TodoListItemQueryFilter) {
    const url = `${this.baseUrl}/get-all-async`;
    const params = new HttpParams({
      fromObject: {
        title: filter?.title ?? '',
        status: filter?.status ?? '',
        priority: filter?.priority ?? '',
        creationDate: filter?.creationDate?.toISOString().substring(0, 10) ?? '',
        initialDate: filter?.initialDate?.toISOString().substring(0, 10) ?? '',
        finalDate: filter?.finalDate?.toISOString().substring(0, 10) ?? '',
      }
    });

    this.httpClient
      .get<TodoListItem[]>(url, { params: params })
      .subscribe(result => this.itemsSignal.update(() => result));
  }

  public GetById(id: string) {
    const url = `${this.baseUrl}/get-by-id-async/${id}`;
    return this.httpClient.get<TodoListItem>(url);
  }

  public Add(item: AddItem) {
    const url = `${this.baseUrl}/add-async`;
    return this.httpClient.post<void>(url, item);
  }

  public Update(item: UpdateItem) {
    const url = `${this.baseUrl}/update-async`;
    return this.httpClient.patch<void>(url, item);
  }

  public Delete(id: string) {
    const url = `${this.baseUrl}/delete-async/${id}`;
    return this.httpClient.delete<void>(url);
  }
}
