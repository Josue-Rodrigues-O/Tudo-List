import { Injectable } from '@angular/core';
import { Validation } from '../validation/validation';
import { ValueStateEnum } from '../../../enums/value-state/valueState-enum';

@Injectable({
  providedIn: 'root',
})
export class BaseValidatorService {
  private validations: Array<Validation>;
  private isGenericMessage: boolean = false;

  private clearValueState(fieldId: string) {
    document.getElementById(fieldId)?.classList.toggle('is-valid', false);
    document.getElementById(fieldId)?.classList.toggle('is-invalid', false);
  }

  private setValueStateError(fieldId: string) {
    document.getElementById(fieldId)?.classList.toggle('is-valid');
  }

  private setValueStateSuccess(fieldId: string) {
    document.getElementById(fieldId)?.classList.toggle('is-valid');
  }

  constructor() {
    this.validations = new Array<Validation>();
  }

  ruleFor(propertyValue: any, fieldName: string, fieldId: string) {
    const validation = new Validation(propertyValue, fieldName, fieldId);
    this.validations.push(validation);
    return validation;
  }

  validate() {
    let errorMessages: Array<string> = new Array<string>();
    this.validations.forEach((validation) => {
      let errors = validation.validate();

      if (errors.errorsMessages.length > 0) {
        errorMessages.push(`- ${errors.errorsMessages[0]}`);
        this.setValueState(errors.fieldId, ValueStateEnum.error);
      } else {
        this.setValueState(errors.fieldId, ValueStateEnum.success);
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

  setValueState(fieldId: string, valueState: ValueStateEnum) {
    this.clearValueState(fieldId);
    switch (valueState) {
      case ValueStateEnum.none:
        this.clearValueState(fieldId);
        break;
      case ValueStateEnum.error:
        this.setValueStateError(fieldId);
        break;
      case ValueStateEnum.success:
        this.setValueStateSuccess(fieldId);
        break;
    }
  }
}
