import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class MessageToastService {
  private _snackBar = inject(MatSnackBar);

  show(message: string, type: 'success' | 'error' | 'info' = 'info') {
    this._snackBar.open(message, undefined, {
      duration: 3000,
      panelClass: [`toast-${type}`],
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }
}
