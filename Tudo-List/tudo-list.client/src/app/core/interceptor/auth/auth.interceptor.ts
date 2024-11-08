import {
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RequestService } from '../../../shared/services/requestService/request.service';

@Injectable()
export class authInterceptor implements HttpInterceptor {
  constructor(private auth: RequestService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const authToken = this.auth.getToken();

    const authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authToken}`),
    });

    return next.handle(authReq);
  }
}
