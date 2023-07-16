import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
    constructor() { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const headers = req.headers
        .set('Authorization', 'Bearer ' +  localStorage.getItem('authToken'))
        .set('Lang','ENG');
        const authReq = req.clone({ headers});
        return next.handle(authReq);
    }
}
