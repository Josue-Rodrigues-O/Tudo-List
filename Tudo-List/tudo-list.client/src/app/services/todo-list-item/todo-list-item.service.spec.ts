import { TestBed } from '@angular/core/testing';

import { TodoListItemService } from './todo-list-item.service';

describe('TodoListItemService', () => {
  let service: TodoListItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TodoListItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
