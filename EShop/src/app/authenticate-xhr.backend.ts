import { Request, XHRBackend, BrowserXhr, ResponseOptions, XSRFStrategy, Response } from '@angular/http';
import { HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import {catchError} from 'rxjs/operators';

// sweet global way to handle 401s - works in tandem with existing AuthGuard route checks
// http://stackoverflow.com/questions/34934009/handling-401s-globally-with-angular-2

@Injectable()
export class AuthenticateXHRBackend extends XHRBackend {

  constructor(_browserXhr: BrowserXhr, _baseResponseOptions: ResponseOptions, _xsrfStrategy: XSRFStrategy) {
    super(_browserXhr, _baseResponseOptions, _xsrfStrategy);
  }

  createConnection(request: any) {
    const xhrConnection = super.createConnection(request.body);
    xhrConnection.response = xhrConnection.response.pipe(catchError((error: HttpResponse<any> ) => {
      if ((error.status === 401 || error.status === 403) && (window.location.href.match(/\?/g) || []).length < 2) {

        console.log('The authentication session expired or the user is not authorized. Force refresh of the current page.');
        localStorage.removeItem('auth_token');
        window.location.href = window.location.href + '?' + new Date().getMilliseconds();
      }
      return throwError(error);
    }));
    return xhrConnection;
  }
}
