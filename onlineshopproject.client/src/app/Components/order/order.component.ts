import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/User';
import { UserService } from '../../Services/User/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2'


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
  providers: [UserService]
})
export class OrderComponent implements OnInit {

  public orderForm: FormGroup;

  constructor(private fb: FormBuilder,
    private _UserService: UserService,
    private _router: Router) {

    this.orderForm = this.fb.group({
      postalcode: ['', Validators.required],
      address: ['', Validators.required],
    

    })
  }

  ngOnInit() {

  }
}
