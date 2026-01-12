import { ValidatorFn, AbstractControl, ValidationErrors } from "@angular/forms";

export const passwordMatchValidator: ValidatorFn =
    (control: AbstractControl): ValidationErrors | null => {

        const passwordCtrl = control.get('password');
        const confirmCtrl = control.get('confirmPassword');

        if (!passwordCtrl || !confirmCtrl) {
            return null;
        }

        if (passwordCtrl.value !== confirmCtrl.value) {
            confirmCtrl.setErrors({ passwordMismatch: true });
            return { passwordMismatch: true };
        }

        if (confirmCtrl.hasError('passwordMismatch')) {
            confirmCtrl.setErrors(null);
        }

        return null;
    };
