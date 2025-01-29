import { TranslateService } from '@ngx-translate/core';
import { InputComponent } from '../../../../features/components/input/input.component';

export class Validation {
  private field: InputComponent;
  private fieldName: string;
  private propertyValue: any;
  private rules: Array<RuleWithMessage>;
  private translate: TranslateService;

  constructor(
    propertyValue: any,
    fieldName: string,
    field: InputComponent,
    translate: TranslateService
  ) {
    this.field = field;
    this.fieldName = fieldName;
    this.propertyValue = propertyValue;
    this.rules = new Array<RuleWithMessage>();
    this.translate = translate;
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
    const message = this.translate.instant('validationNotEmpty', {
      fieldName: this.fieldName,
    });
    const rule = () => this.propertyValue.length > 0;

    this.addValidation(rule, message);
    return this;
  }

  maxLength(maxLength: number) {
    const message = this.translate.instant('validationMaxLength', {
      fieldName: this.fieldName,
      maxLength: maxLength,
    });
    const rule = () => this.propertyValue.length <= maxLength;

    this.addValidation(rule, message);
    return this;
  }

  minLength(minLength: number) {
    const message = this.translate.instant('validationMinLength', {
      fieldName: this.fieldName,
      minLength: minLength,
    });
    const rule = () => this.propertyValue.length >= minLength;

    this.addValidation(rule, message);
    return this;
  }

  match(regex: RegExp) {
    const message = this.translate.instant('validationMatch', {
      fieldName: this.fieldName,
    });
    const rule = () => this.propertyValue.match(regex);

    this.addValidation(rule, message);
    return this;
  }
}

class RuleWithMessage {
  rule!: () => boolean;
  message!: string;
}
