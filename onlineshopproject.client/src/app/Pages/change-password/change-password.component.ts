import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import Swal from 'sweetalert2'
import { UserService } from '../../Services/User/user.service';
import { jwtDecode } from 'jwt-decode';
import { User } from '../../Models/User';
import { Router } from '@angular/router';


@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  public UserId: number | null
  public pwdForm: FormGroup

  constructor(private fb: FormBuilder,
    private _UserService: UserService,
    private _router: Router
  ) {
    this.pwdForm = this.fb.group({
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmpassword: ['', [Validators.required, Validators.minLength(6)]]
    }, {
      validators: this.passwordMatchValidator
    });

    this.UserId = null
  }

  passwordMatchValidator(group: FormGroup) {

    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmpassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }


  ngOnInit() {
    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {


          this.UserId = Response.id

          

        },
        error => {
          console.log(error)
        }
      )

    }
  }

    onSubmit(): void {

      const password: string = this.pwdForm.get('password')?.value

      if (password != null && this.UserId != null) {

        this._UserService.UpdatePassword(password, this.UserId).subscribe(
          response => {
            Swal.fire({
              text: "password changed successfully",
              icon: "success"
            }).then(() => {
              this._router.navigateByUrl('/user');
            }
           
            )
          },
          Error => {
            console.log(Error);
            Swal.fire({
              text: "error changing password",
              icon: "error"
            })
          }
        )

      }  
      
    }
  }

