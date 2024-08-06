import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts: { message: string; class: string }[] = [];

  // Método para adicionar um toast
  show(message: string, toastClass: string = 'text-bg-primary') {
    this.toasts.push({ message, class: toastClass });
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
