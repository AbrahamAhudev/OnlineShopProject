import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/Authentication/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  isAuthenticated: boolean = false;

  constructor(
    private _AuthService: AuthService  ) {
  }

  ngOnInit(): void {
    this.isAuthenticated = this._AuthService.isAuthenticated();
  }

  public logoff(): void {
    localStorage.removeItem("token");
    window.location.reload();
  }

}
