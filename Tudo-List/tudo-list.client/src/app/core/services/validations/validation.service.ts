import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ValidationService {
  constructor() {}

  notEmpty(value: string): boolean {
    return value.length > 0;
  }

  maxLength(value: string, maxLength: number): boolean {
    return value.length <= maxLength;
  }

  minLength(value: string, minLength: number): boolean {
    return value.length >= minLength;
  }

  equal(value_1: string, value_2: string): boolean {
    return value_1 == value_2;
  }

  match(value: string, regex: RegExp) {
    return value.match(regex);
  }
}
