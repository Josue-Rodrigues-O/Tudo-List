import { Component, ElementRef, ViewChild } from '@angular/core';
import { InputComponent } from '../../features/fragments/input/input.component';

@Component({
  selector: 'app-teste',
  templateUrl: './teste.component.html',
  styleUrl: './teste.component.scss',
})
export class TesteComponent {
  @ViewChild('teste') abcasdsad!: ElementRef<InputComponent>;
  constructor() {}
}
