import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { User } from '../../Models/User';
import { UserService } from '../../Services/User/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  public User: User | null
  public phone_number: string | undefined

  constructor(private _UserService: UserService,
    private _router: Router,

  ) {

    this.User = null;
    this.phone_number = '';
  }

  ngOnInit() {
    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;
     
      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {
          this.User = Response
          this.phone_number = this.User?.phone_number?.toString();
          
        },
        error => {
          console.log(error)
        }
      )

    }



  }

  pwdchange() {
    this._router.navigate(['/user/changepwd']);
  }

}
