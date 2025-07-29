import { Component } from '@angular/core';

@Component({
  selector: 'app-ui-kit',
  templateUrl: './ui-kit.component.html',
  styleUrl: './ui-kit.component.css',
})
export class UiKitComponent {
  testModel = {
    model: '',
  };

  aoClicar(txt = '') {
    console.log('Clicou', txt);
  }
}
