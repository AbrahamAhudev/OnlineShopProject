import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../../Services/Authentication/auth.service';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private _Authservice: AuthService,
    private _router: Router,) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree
  {
    if (this._Authservice.isAuthenticated()) {

      Swal.fire({
        text: "you are already logged",
        icon: "error"
      }).then(() => {
        this._router.navigate(['/home']);
        return false
        
      })

    }
    return true
  }
  
}
