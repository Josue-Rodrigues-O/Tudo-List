import { Component, Input } from '@angular/core';
import { ValueStateEnum } from '../../../core/enums/value-state/valueState-enum';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss',
})
export class InputComponent {
  @Input() fieldId: string = '';
  @Input() label: string = '';
  text: string = '';

  private error: string = 'is-invalid';
  private success: string = 'is-valid';

  private setValueStateNone() {
    document.getElementById(this.fieldId)?.classList.toggle(this.error, false);
    document.getElementById(this.fieldId)?.classList.toggle(this.success, false);
  }

  private setValueStateError() {
    document.getElementById(this.fieldId)?.classList.toggle(this.error);
  }

  private setValueStateSuccess() {
    document.getElementById(this.fieldId)?.classList.toggle(this.success);
  }

  setValueText(value: string) {
    this.text = value;
  }

  setValueState(valueState: ValueStateEnum) {
    switch (valueState) {
      case ValueStateEnum.none:
        this.setValueStateNone();
        break;

      case ValueStateEnum.error:
        this.setValueStateError();
        break;

      case ValueStateEnum.success:
        this.setValueStateSuccess();
        break;

      default:
        this.setValueStateNone();
        break;
    }
  }
}
