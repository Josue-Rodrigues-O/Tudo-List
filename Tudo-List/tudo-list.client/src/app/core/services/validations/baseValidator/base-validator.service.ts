import { Injectable } from '@angular/core';
import { Validation } from '../validation/validation';
import { ValueStateEnum } from '../../../enums/value-state/valueState-enum';
import { InputComponent } from '../../../../features/fragments/input/input.component';

@Injectable({
  providedIn: 'root',
})
export class BaseValidatorService {
  private validations: Array<Validation>;
  private isGenericMessage: boolean = false;

  constructor() {
    this.validations = new Array<Validation>();
  }

  ruleFor(propertyValue: any, fieldName: string, field: InputComponent) {
    const validation = new Validation(propertyValue, fieldName, field);
    this.validations.push(validation);
    return validation;
  }

  validate() {
    let errorMessages: Array<string> = new Array<string>();
    this.validations.forEach((validation) => {
      let errors = validation.validate();

      let valueState = ValueStateEnum.success;

      if (errors.errorsMessages.length > 0) {
        valueState = ValueStateEnum.error;
        errorMessages.push(`- ${errors.errorsMessages[0]}`);
        errors.field?.setValueText(errors.errorsMessages[0]);
      }

      errors.field?.setValueState(valueState);
    });

    return {
      isValid: errorMessages.length <= 0,
      title: 'Validation failed',
      messages: errorMessages,
      isGenericMessage: this.isGenericMessage,
    };
  }

  setIsGenericMessage(value: boolean) {
    this.isGenericMessage = value;
  }
}
