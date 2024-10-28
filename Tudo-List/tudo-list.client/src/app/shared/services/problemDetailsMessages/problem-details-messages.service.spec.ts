import { TestBed } from '@angular/core/testing';

import { ProblemDetailsMessagesService } from './problem-details-messages.service';

describe('ProblemDetailsMessagesService', () => {
  let service: ProblemDetailsMessagesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProblemDetailsMessagesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
