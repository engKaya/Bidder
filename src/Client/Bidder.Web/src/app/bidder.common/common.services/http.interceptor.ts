import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpEventType
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';  
import { AuthLoginService } from 'src/app/pages/authentication/module.services/auth.service'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthLoginService
    ) {

    }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next
      .handle(this.addAuthToken(request))
      .pipe(
        catchError((error: HttpErrorResponse) => { 
          if (error.status === 401) {
            this.authService.logout();
          }
          throw error;
        })
      )
    }

  addAuthToken(request: HttpRequest<any>) {  
    if  (!this.authService.isLoggedIn()) {
      return request;
    }

    const token = this.authService.getToken(); 
    return request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
    })
  }
}