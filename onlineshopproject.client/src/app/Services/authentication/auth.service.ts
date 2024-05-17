import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../Models/User';
import { Global } from '../global';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this._http.post(Global.url + 'api/Auth/login', {username, password})
  }

  isAuthenticated(): boolean {
    return localStorage.getItem('token') !== null;
  }
}
