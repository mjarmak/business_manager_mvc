import { HttpErrorResponse, HttpResponse, HttpHandler, HttpInterceptor, HttpRequest, HttpEvent } from '@angular/common/http';
import { AuthService } from './auth-service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AlertService } from './alert-service';
import { BusinessManagerService } from './business-manager-svc';
import { RouterService } from './router-service';

export class TokenInterceptor implements HttpInterceptor {

    constructor(public auth: AuthService, private alertSerice: AlertService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!request.headers.has("Authorization") && !request.url.includes("googleapis")) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.auth.getToken()}`
                }
            });
        }

        return next.handle(request).pipe(tap(
            (event: HttpEvent<any>) => event instanceof HttpResponse,
            (error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.alertSerice.warning("Login expired", "Please login again");
                    RouterService.openLoginPage();
                } else if (error.status === 500) {
                    this.alertSerice.error(error.statusText, error.message);
                }
            }
        ));
    }
}
