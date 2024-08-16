import { Injectable } from '@angular/core';
import { Validation } from '../validation/validation';

@Injectable({
  providedIn: 'root',
})
export class BaseValidatorService {
  private validations: Array<Validation>;
  private stopAtTheFirstFailure: boolean = false;

  constructor() {
    this.validations = new Array<Validation>();
  }

  ruleFor(propertyValue: any, fieldName: string) {
    const validation = new Validation(propertyValue, fieldName);
    this.validations.push(validation);
    return validation;
  }

  validate() {
    this.validations.forEach((validation) => {
      validation.validate(this.stopAtTheFirstFailure);
    });
  }

  setValidationMode(value: boolean) {
    this.stopAtTheFirstFailure = value;
  }
}
