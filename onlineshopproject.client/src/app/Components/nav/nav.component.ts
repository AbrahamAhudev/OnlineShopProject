import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthService } from 'src/app/Services/Authentication/auth.service';
import { RoleService } from '../../Services/Role/role.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  public isAuthenticated: boolean = false;
  public isAdmin: boolean = false;


  constructor(
    private _AuthService: AuthService,
    private _RoleService: RoleService) {
  }

  ngOnInit(): void {
    this.isAuthenticated = this._AuthService.isAuthenticated();

    this._RoleService.isAdmin().subscribe(isAdmin => {
      this.isAdmin = isAdmin;
    });

   
  }

  public logoff(): void {
    localStorage.removeItem("token");
    window.location.reload();
  }

}
