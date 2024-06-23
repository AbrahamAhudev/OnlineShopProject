import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import Swal from 'sweetalert2';
import { User } from '../../Models/User';
import { RoleService } from '../../Services/Role/role.service';
import { UserService } from '../../Services/User/user.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(
    private _router: Router,
    private _UserService: UserService,
    private _RoleService: RoleService
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {

    const result$ = new Subject<boolean>();
    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);
      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        user => {

         
          if (user.id != null) {
            this._RoleService.getUserRole(user.id).subscribe(
              role => {

               

                if (role.roleId == 2) {
                  result$.next(true);
                } else {
                  Swal.fire('Access denied', 'you dont have permission to access this page', 'error');
                  this._router.navigate(['/home']);
                  result$.next(false);
                }
                result$.complete();
              },
              error => {
                console.error(error);
                Swal.fire('Error', 'error verifying the role', 'error');
                this._router.navigate(['/home']);
                result$.next(false);
                result$.complete();
              }
            );
          } else {
            Swal.fire('Access denied', 'error obtaining user data', 'error');
            this._router.navigate(['/home']);
            result$.next(false);
            result$.complete();
          }
        },
        error => {
          console.error(error);
          Swal.fire('Error', 'error obtaining user data', 'error');
          this._router.navigate(['/home']);
          result$.next(false);
          result$.complete();
        }
      );
    } else {
      Swal.fire('Access denied', 'you dont have a valid token', 'error');
      this._router.navigate(['/home']);
      result$.next(false);
      result$.complete();
    }

    return result$.asObservable();
  }
}
