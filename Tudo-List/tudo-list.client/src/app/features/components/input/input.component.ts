import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { ValueStateEnum } from '../../../core/enums/value-state/valueState-enum';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss',
})
export class InputComponent {
  @ViewChild('input') private input!: ElementRef;
  @Input() placeholder: string = '';
  @Input() label: string = '';
  @Input() type: string = 'text';
  @Input() model: any;
  @Output() modelChange: EventEmitter<any> = new EventEmitter<any>();
  text: string = '';

  private error: string = 'is-invalid';
  private success: string = 'is-valid';

  private setValueStateNone() {
    this.input.nativeElement?.classList.toggle(this.error, false);
    this.input.nativeElement?.classList.toggle(this.success, false);
  }

  private setValueStateError() {
    this.input.nativeElement?.classList.toggle(this.error);
  }

  private setValueStateSuccess() {
    this.input.nativeElement?.classList.toggle(this.success);
  }

  onModelChange(value: any) {
    this.modelChange.emit(value);
  }

  setValueText(value: string) {
    this.text = value;
  }

  setValueState(valueState: ValueStateEnum) {
    this.setValueStateNone();
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
