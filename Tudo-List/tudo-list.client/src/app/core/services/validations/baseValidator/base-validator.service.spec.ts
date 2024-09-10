import { TestBed } from '@angular/core/testing';

import { BaseValidatorService } from './base-validator.service';

describe('BaseValidatorService', () => {
  let service: BaseValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BaseValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
