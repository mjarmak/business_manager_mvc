import { HttpErrorResponse, HttpResponse, HttpHandler, HttpInterceptor, HttpRequest, HttpEvent } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AlertService } from './alert-service';
import { Injectable } from '@angular/core';
import { BusinessManagerService } from './business-manager-svc';

export class TokenInterceptor implements HttpInterceptor {

    constructor(public auth: AuthService, private alertSerice: AlertService, private businessManagerService: BusinessManagerService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.auth.getToken()}`
            }
        });

        return next.handle(request).pipe(tap(
            (event: HttpEvent<any>) => console.log(event instanceof HttpResponse ? 'dope' : 'not dope'),
            (error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.alertSerice.warning("Login expired", "Please login again");
                    this.businessManagerService.openLoginPage();
                    console.log("unauthorized");
                } else if (error.status === 500) {
                    this.alertSerice.error(error.statusText, error.message);
                    console.log("received error");
                }
            }
        ));
    }
}