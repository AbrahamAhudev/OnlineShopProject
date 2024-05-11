import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Global } from 'src/app/Services/global';
import { User } from "../../Models/User";

@Injectable(
  {
    providedIn: 'root'
  }
)
export class UserService {

  public url: string

  constructor(private _http: HttpClient) {
    this.url = Global.url;
  }

  create(user: User): Observable<any> {
    let params = JSON.stringify(user)
    let headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this._http.post(this.url + 'api/User', params, { headers: headers });
  }

  getUsers(): Observable<any> {

    let user = 'api/User'

    return this._http.get(this.url + user)

  }

}
