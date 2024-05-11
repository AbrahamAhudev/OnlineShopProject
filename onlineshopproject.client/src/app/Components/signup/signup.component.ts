import { Component } from '@angular/core';
import { User } from 'src/app/Models/User';
import { UserService } from '../../Services/User/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  providers: [UserService]
})
export class SignupComponent {

  public userForm: FormGroup;
  public User:User

  constructor(
    private fb: FormBuilder,
    private _UserService: UserService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {

    this.User = new User('', '', null, '', '', '', null)

    this.userForm = this.fb.group({
      username: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: [''],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmpassword: ['', [Validators.required, Validators.minLength(6)]],
      phone_number: [null]

    }, {
      validators: this.passwordMatchValidator
    });
  }

  passwordMatchValidator(group: FormGroup) {

    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmpassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {

    const phoneNumberControl = this.userForm.get('phone_number');

    if (this.userForm.valid && (phoneNumberControl?.value === null || phoneNumberControl?.value.toString().length === 9)) {


      this.User.username = this.userForm.get('username')?.value

      this.User.firstName = this.userForm.get('firstname')?.value

      this.User.lastName = this.userForm.get('lastname')?.value

      this.User.address = this.userForm.get('address')?.value

      this.User.email = this.userForm.get('email')?.value

      this.User.phone_Number = this.userForm.get('phone_number')?.value

      this.User.password = this.userForm.get('password')?.value

      this._UserService.create(this.User).subscribe(
        (response) => {
          alert("User created successfully");
          console.log(response);
          this._router.navigate(['/login']);
        },
        (error) => {
          console.error("Error:", error);
          alert("Error when creating the user");

        }
      )


       // Aquí puedes enviar los datos del usuario al servicio
       // this._UserService.createUser(this.userForm.value).subscribe(/* Manejar la respuesta */);
      } else {
        alert("Please fill out the form correctly");
      }

  }
}
