import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';


export const authGuard: CanActivateFn = (route, state) => {
  const service = inject(LoginService);
  const router = inject(Router);
  if (!service.isAuthenticated()) {
    router.navigate(['/login']);
    return false;
  }

  return true;
};
