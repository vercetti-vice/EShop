import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from '@angular/common/http';

import { UserRegistration } from '../models/user.registration.interface';
import { Credentials } from '../models/credentials.interface';
import { ConfigService } from '../utils/config.service';

import {BaseService} from './base.service';

import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { map, filter, switchMap } from 'rxjs/operators';


@Injectable()

export class UserService extends BaseService {

  baseUrl = '';

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: HttpClient, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getApiURI();
  }

  register(email: string, password: string, firstName: string, lastName: string, location: string): Observable<any> {
    const body = JSON.stringify({ email, password, firstName, lastName, location });
    // const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    // const options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrl + '/accounts', body,
      {
        headers: new HttpHeaders({'Content-Type': 'application/json'}),
        observe: 'response',
        responseType: 'json'
      }).pipe(map(res => true));
  }

  login(userName, password): Observable<any> {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    return this.http.post(this.baseUrl + '/auth/login', JSON.stringify({ userName, password }),
      {headers, observe: 'response', responseType: 'json'})
      .pipe(map((res: HttpResponse<any>) => {
        localStorage.setItem('auth_token', res.body.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      }));
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }
}
