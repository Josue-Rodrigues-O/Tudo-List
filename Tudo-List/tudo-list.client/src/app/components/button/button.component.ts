import { Component, Input, input } from '@angular/core';
import { BtnTypeEnum } from '../../core/enums/ui/btn-type-enum';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.css',
  host: {
    '[class.primary]': 'isPrimary()',
    '[class.secondary]': '!isPrimary()',
    '[attr.role]': "'button'",
  },
})
export class ButtonComponent {
  func = input<() => void>(() => {});
  type = input<BtnTypeEnum>(BtnTypeEnum.primary);
  isPrimary = () => this.type() === BtnTypeEnum.primary;

  constructor() {}

  protected onClickButton() {
    let funcao = this.func();
    funcao();
  }
}
