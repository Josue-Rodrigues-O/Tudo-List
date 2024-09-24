import { ElementRef } from '@angular/core';
import { InputComponent } from '../../../../features/components/input/input.component';

export class FieldsForUserValidation {
  public email!: InputComponent;
  public password!: InputComponent;
  public confirmPassword!: InputComponent;
}
