import { Injectable, WritableSignal, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MessageBoxService {
  modalData: WritableSignal<{ title: string; message: Array<string> } | null> =
    signal(null);

  constructor() {}

  open(title: string, message: Array<string>) {
    this.modalData.set({ title, message });
  }

  close() {
    this.modalData.set(null);
  }

  confirm() {
    // Aqui você pode adicionar a lógica para o botão de confirmação, como emitir um evento.
    this.close();
  }
}
