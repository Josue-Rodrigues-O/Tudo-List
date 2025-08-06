import { Component } from '@angular/core';
import { BtnTypeEnum } from '../../core/enums/ui/btn-type-enum';

@Component({
  selector: 'app-ui-kit',
  templateUrl: './ui-kit.component.html',
  styleUrl: './ui-kit.component.css',
})
export class UiKitComponent {
  protected btnType = BtnTypeEnum;
  constructor() {}
  testModel = {
    model: '',
  };

  aoClicar(txt = '') {
    console.log('Clicou', txt);
  }
}
