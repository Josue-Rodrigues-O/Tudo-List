import { Component, forwardRef, input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrl: './input.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true,
    },
  ],
})
export class InputComponent implements ControlValueAccessor {
  iconIsClickable = input(false);
  onClickIcon = input<() => void>(() => {});
  icon = input('');
  placeholder = input('');
  value = '';

  private onChange: any = () => {};
  private onTouched: any = () => {};

  writeValue(string: any): void {
    this.value = string || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {}

  onInput(event: Event): void {
    const val = (event.target as HTMLInputElement).value;
    this.value = val;
    this.onChange(val);
    this.onTouched();
  }

  onClick() {
    if (this.iconIsClickable()) {
      let funcao = this.onClickIcon();
      funcao();
    }
  }
}
