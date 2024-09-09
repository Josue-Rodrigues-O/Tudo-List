import { Injectable } from '@angular/core';
import { ValueStateEnum } from '../../../core/enums/value-state/valueState-enum';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts: { message: string; class: string }[] = [];

  // Método para adicionar um toast
  show(message: string, toastClass: ValueStateEnum) {
    let styleClass = '';
    switch (toastClass) {
      case ValueStateEnum.none:
        styleClass = 'text-bg-primary';
        break;

      case ValueStateEnum.error:
        styleClass = 'text-bg-danger';
        break;

      case ValueStateEnum.success:
        styleClass = 'text-bg-success';
        break;

      default:
        styleClass = 'text-bg-primary';
        break;
    }
    this.toasts.push({ message, class: styleClass });
    setTimeout(() => this.removeToast(this.toasts[0]), 3000); // Remove após 3 segundos
  }

  // Método para remover um toast
  removeToast(toast: { message: string; class: string }) {
    const index = this.toasts.indexOf(toast);
    if (index !== -1) {
      this.toasts.splice(index, 1);
    }
  }
}
