import { InputComponent } from '../../../../features/fragments/input/input.component';

export class Validation {
  private field: InputComponent;
  private fieldName: string;
  private propertyValue: any;
  private rules: Array<RuleWithMessage>;

  constructor(propertyValue: any, fieldName: string, field: InputComponent) {
    this.field = field;
    this.fieldName = fieldName;
    this.propertyValue = propertyValue;
    this.rules = new Array<RuleWithMessage>();
  }

  private addValidation(rule: () => boolean, message: string) {
    this.rules.push({ rule, message });
  }

  validate() {
    let errorsMessages: Array<string> = new Array<string>();

    this.rules.forEach((ruleWithMessage) => {
      if (!ruleWithMessage.rule()) errorsMessages.push(ruleWithMessage.message);
    });

    return {
      field: this.field,
      errorsMessages: errorsMessages,
    };
  }

  must(rule: () => boolean, message: string) {
    this.addValidation(rule, message);
    return this;
  }

  notEmpty() {
    const message = `The value of the '${this.fieldName}' field cannot be empty!`;
    const rule = () => this.propertyValue.length > 0;

    this.addValidation(rule, message);
    return this;
  }

  maxLength(maxLength: number) {
    const message = `The value of the '${this.fieldName}' field must have a maximum of '${maxLength}' characters!`;
    const rule = () => this.propertyValue.length <= maxLength;

    this.addValidation(rule, message);
    return this;
  }

  minLength(minLength: number) {
    const message = `The value of the '${this.fieldName}' field must have at least '${minLength}' characters!`;
    const rule = () => this.propertyValue.length >= minLength;

    this.addValidation(rule, message);
    return this;
  }

  match(regex: RegExp) {
    const message = `The value of the '${this.fieldName}' field is invalid!`;
    const rule = () => this.propertyValue.match(regex);

    this.addValidation(rule, message);
    return this;
  }
}

class RuleWithMessage {
  rule!: () => boolean;
  message!: string;
}
