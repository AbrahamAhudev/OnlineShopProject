import { Component } from '@angular/core';
import { AuthService } from '../../Services/Authentication/auth.service';
import { UserService } from '../../Services/User/user.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2'
import { Location } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [UserService]
})
export class LoginComponent {

  public LogForm: FormGroup


  constructor(
    private _UserService: UserService,
    private _AuthService: AuthService,
    private fb: FormBuilder,
    private _router: Router,
    private _Location: Location

  ) {
    this.LogForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
      })
  }

  onSubmit() {
    this._AuthService.login(this.LogForm.get('username')?.value, this.LogForm.get('password')?.value)
      .subscribe(
        (Response) => {

          localStorage.setItem("token", Response.token);


          Swal.fire({
            text: "logged successfully",
            icon: "success"
          }).then(() => {
            this._router.navigateByUrl('/home').then(() => {
              window.location.reload();
            });
          })
            
         
            
          
        },

        (Error) => {
         
          console.log(Error)
          Swal.fire({
            text: "username or password are wrong",
            icon: "error"
          })
        }
      )
  }
}
