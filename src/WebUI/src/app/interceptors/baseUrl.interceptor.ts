import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { finalize, tap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
    constructor(private snackBar: MatSnackBar
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.url === 'token') {
            const baseUrl = environment.tokenUrl;
            const apiReq = req.clone({ url: `${baseUrl}/${req.url}` });
            return next.handle(apiReq).pipe(finalize(() => setTimeout(() => this, 0)));
        } else if (req.url.includes('googleapis')) {
            return next.handle(req).pipe(finalize(() => setTimeout(() => this, 0)));
        } else if (!req.url.includes('assets')) {
            const baseUrl = environment.apiUrl;
            const apiReq = req.clone({ url: `${baseUrl}/${req.url}` });

            return next.handle(apiReq).pipe(
                tap(
                    (event: HttpEvent<any>) => {
                        if (event instanceof HttpResponse) {
                            // do stuff with response if you want
                        }
                    },
                    (err: any) => {
                        if (err instanceof HttpErrorResponse) {
                            if (err.status === 401) {
                               // this._router.navigate(['account/login']);
                            }
                            else if (err.status === 500) {
                                this.showSnackbar('Something went wrong.');
                            }
                            else if (err.status === 400) {
                                this.showSnackbar(err.error);
                            }
                        }
                    }
                ),
                finalize(() => setTimeout(() => this, 0))
            );
        }
        else {
            return next.handle(req);
        }
    }

    private showSnackbar(message: string, duration: number = 3000): void {
        this.snackBar.open(message, 'dismiss', {
            duration: duration,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
        });
    }
}
