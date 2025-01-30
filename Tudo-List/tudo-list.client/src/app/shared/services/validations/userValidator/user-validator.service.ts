import { BaseValidatorService } from '../baseValidator/base-validator.service';
import { User } from '../../../../core/models/user/user';
import { FieldsForUserValidation } from '../../../../core/models/fields-for-user-validation/fields-for-user-validation';

export class UserValidatorService extends BaseValidatorService {
  private regexEmail = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$/i;

  constructor(private fieldsForUserValidation: FieldsForUserValidation) {
    super();
  }

  validationToRegister(user: User) {
    const ruleForConfirmPassword = {
      rule: () => user.password == user.confirmPassword,
      message: this.translate.instant('validationPasswordAndConfirmPassword'),
    };

    this.ruleFor(user.email, 'Email', this.fieldsForUserValidation.email)
      .notEmpty()
      .match(this.regexEmail);

    this.ruleFor(
      user.password,
      'Password',
      this.fieldsForUserValidation.password
    )
      .notEmpty()
      .minLength(8);

    this.ruleFor(
      user.confirmPassword,
      'Confirm Password',
      this.fieldsForUserValidation.confirmPassword
    )
      .notEmpty()
      .must(ruleForConfirmPassword.rule, ruleForConfirmPassword.message);
  }

  validationToConnect(user: User) {
    this.ruleFor(user.email, 'Email', this.fieldsForUserValidation.email)
      .notEmpty()
      .match(this.regexEmail);
    this.ruleFor(
      user.password,
      'Password',
      this.fieldsForUserValidation.password
    )
      .notEmpty()
      .minLength(8);
  }
}
