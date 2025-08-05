import { Component } from '@angular/core';
import { MessageBoxService } from '../../../shared/services/message-box/message-box.service';

@Component({
  selector: 'app-message-box',
  templateUrl: './message-box.component.html',
  styleUrl: './message-box.component.scss',
})
export class MessageBoxComponent {
  constructor(public messageBox: MessageBoxService) {}
}
