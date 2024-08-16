import { BaseValidatorService } from '../baseValidator/base-validator.service';
import { User } from '../../../models/user/user';

export class UserValidatorService extends BaseValidatorService {
  constructor(user: User) {
    super();
    const regexEmail = /^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$/i;

    const ruleForConfirmPassword = {
      rule: () => user.password == user.confirmPassword,
      message:
        "The values ​​of the 'Password' and 'Password Confirmation' fields must be the same!",
    };

    this.ruleFor(user.email, 'Email').notEmpty().match(regexEmail);

    this.ruleFor(user.password, 'Password').notEmpty().minLength(8);

    this.ruleFor(user.confirmPassword, 'Confirm Password')
      .notEmpty()
      .must(ruleForConfirmPassword.rule, ruleForConfirmPassword.message);
  }
}
