import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { User } from '../../Models/User';
import { UserService } from '../../Services/User/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import Swal from 'sweetalert2'
import { Router } from '@angular/router';
import { NotFoundError } from 'rxjs';


@Component({
  selector: 'app-useredit',
  templateUrl: './useredit.component.html',
  styleUrls: ['./useredit.component.css']
})
export class UsereditComponent implements OnInit {

  public editForm: FormGroup;
  public User: User | null
  public Userid: number
  constructor(private _UserService: UserService,
    private fb: FormBuilder,
    private _router: Router,) {
    this.User = null;

    this.Userid = 0;

    this.editForm = this.fb.group({

      firstname: ['', Validators.required],
      lastname: [''],
      address: ['', Validators.required],
      phone_number: [null]

    })

  }


  ngOnInit() {

    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {
          this.User = Response
          this.Userid = Response.id
        
          this.editForm = this.fb.group({

            firstname: [this.User?.firstName, Validators.required],
            lastname: [this.User?.lastName],
            address: [this.User?.address, Validators.required],
            phone_number: [this.User?.phone_number]

          })
        },
        error => {
          console.log(error)
        }
      )

    }

  }


  SaveChanges() {

    if (this.editForm.invalid) {
      Swal.fire({
        text: "Please fill out the form correctly.",
        icon: "error"
      });
      return;
    }


    if (this.User != null) {

      this.User.firstName = this.editForm.get('firstname')?.value
      this.User.lastName = this.editForm.get('lastname')?.value
      this.User.address = this.editForm.get('address')?.value
      this.User.phone_number = this.editForm.get('phone_number')?.value

      
      if (this.User.phone_number != null && this.User.phone_number.toString().length != 9 ||
        this.User.firstName == undefined || this.User.address == null) {

        Swal.fire({
          text: "fill the form correctly",
          icon: "error"
        })
        return;
      }
   
        this._UserService.UpdateUser(this.User, this.Userid).subscribe(
          Response => {
            Swal.fire({
              text: "user updated successfully",
              icon: "success"
            }).then(() => {
              this._router.navigate(['/user']);
       
            },
              Error => {
                Swal.fire({
                  text: "error updating the user",
                  icon: "error"
                })
              }

            )
          }
        )
      }

      
    }
   
  }

