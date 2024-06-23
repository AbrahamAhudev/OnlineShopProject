import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Global } from 'src/app/Services/global';
import { catchError, map, switchMap } from 'rxjs/operators';
import { UserService } from '../User/user.service';
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  public url: string


  constructor(private _http: HttpClient,
    private _UserService: UserService)
  {
    this.url = Global.url;
  }

  getUserRole(UserId: number): Observable<any> {

    return this._http.get(this.url + 'api/Role/UserRole/' + UserId);
  }

  getDecodedToken(): any {
    const token = localStorage.getItem('token');
    if (!token) {
      return null;
    }
    return jwtDecode(token);
  }


  isAdmin(): Observable<boolean> {
    const decodedToken = this.getDecodedToken();
    if (!decodedToken) {
      return of(false);
    }
    const username = decodedToken.sub;

    return this._UserService.getUserByUsername(username).pipe(
      switchMap(user => this.getUserRole(user.id)),
      map(role => role.roleId === 2), 
      catchError(() => of(false))
    );
  }


  public CreateRole(UserId: number, roleId: number): Observable<any> {


    let headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this._http.post(this.url + 'api/Role/' + UserId, roleId, { headers: headers });
  }

  DeleteRole(UserId: number): Observable<any> {

    return this._http.delete(this.url + 'api/Role/' + UserId);
  }
}
