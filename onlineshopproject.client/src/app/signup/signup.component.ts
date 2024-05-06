import { Component } from '@angular/core';
import { User } from 'src/app/Models/User';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  public ConfirmedPassword: any

  public Password: any

  public User: User;

  constructor() {

    this.ConfirmedPassword = null;

    this.Password = null;

    this.User = new User('', '', '', '', '', '', null)
  }


  public onSubmit() {

  }
}
