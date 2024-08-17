export class Validation {
  private fieldName!: string;
  private propertyValue: any;
  private rules: Array<RuleWithMessage>;

  constructor(propertyValue: any, fieldName: string) {
    this.fieldName = fieldName;
    this.propertyValue = propertyValue;
    this.rules = new Array<RuleWithMessage>();
  }

  private addValidation(rule: () => boolean, message: string) {
    this.rules.push({ rule, message });
  }

  validate(): Array<string> {
    let errorsMessages: Array<string> = new Array<string>();
    let failedValidations = this.rules.filter((x) => !x.rule());
    if (failedValidations.length > 0) {
      failedValidations.forEach((rule) => {
        errorsMessages.push(rule.message);
      });
    }
    return errorsMessages;
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
