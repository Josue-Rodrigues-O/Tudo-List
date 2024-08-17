import { Injectable } from '@angular/core';
import { Validation } from '../validation/validation';

@Injectable({
  providedIn: 'root',
})
export class BaseValidatorService {
  private validations: Array<Validation>;
  private isGenericMessage: boolean = false;

  constructor() {
    this.validations = new Array<Validation>();
  }

  ruleFor(propertyValue: any, fieldName: string) {
    const validation = new Validation(propertyValue, fieldName);
    this.validations.push(validation);
    return validation;
  }

  validate() {
    let errorMessages: Array<string> = new Array<string>();
    this.validations.forEach((validation) => {
      let errorsList = validation.validate();

      if (errorsList.length > 0) {
        errorMessages.push(`- ${errorsList[0]}`);
      }
    });

    return {
      isValid: Boolean(errorMessages.length <= 0),
      title: 'Validation failed',
      messages: errorMessages,
      isGenericMessage: this.isGenericMessage,
    };
  }

  setIsGenericMessage(value: boolean) {
    this.isGenericMessage = value;
  }
}
