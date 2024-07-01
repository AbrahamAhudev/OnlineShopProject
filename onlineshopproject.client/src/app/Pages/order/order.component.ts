import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/User';
import { UserService } from '../../Services/User/user.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2'
import { jwtDecode } from 'jwt-decode';
import { CartItemDTO } from '../../Models/CartItemDTO';
import { CartService } from '../../Services/Cart/cart.service';
import { OrderService } from '../../Services/Order/order.service';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
  providers: [UserService]
})
export class OrderComponent implements OnInit {

  public orderForm: FormGroup;
  public User: User
  public Products: CartItemDTO[]
  public OrderPrice: number
  public CartId: number
  public TotalQuantity: number
  constructor(private fb: FormBuilder,
    private _UserService: UserService,
    private _router: Router,
    private _CartService: CartService,
    private _OrderService: OrderService) {

    this.User = new User('', '', '', '', '', '', null, undefined);

    this.orderForm = this.fb.group({
      address: [''],


    })

    

    this.Products = []

    this.OrderPrice = 0;

    this.CartId = 0;

    this.TotalQuantity = 0;
  }

  ngOnInit() {
    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {

          this.User = Response

          this.CartId = Response.cartId;

          this.orderForm.patchValue({
            address: this.User.address
          });

          if (this.CartId != null) {
            this._CartService.getProductsOfACart(this.CartId).subscribe(
              Response => {

                this.Products = Response;
                this.calculateTotalPrice();
                this.calculateTotalQuantity();
                

              },
              error => {
                console.log(error)
              })
          }

        },
        error => {
          console.log(error)
        }
      )

    }
  }

  onSubmit() {

    if (this.orderForm.invalid || this.orderForm.get('address')?.value.length === 0) {

      Swal.fire({
        text: "fill the form correctly",
        icon: "error"
      })

    } else {

      if (this.CartId != null) {

        let address = this.orderForm.get('address')?.value;

        this._OrderService.Makeorder(this.CartId, address).subscribe(
          Response => {

            Swal.fire({
              text: "order created successfully ",
              icon: "success"
            }).then(() => {
              this._router.navigate(['/products']);
            })


          },
          (Error) => {
            console.log(Error);
          }


        )
      } else {
        Swal.fire({
          text: "items in the cart not found",
          icon: "error"
        })
      }

      
    }

   
  }

  calculateTotalPrice() {
    if (this.Products) {
      this.OrderPrice = this.Products.reduce((total, item) => total + (item.productPrice * item.quantity), 0);
    }
  }

  calculateTotalQuantity() {
    if (this.Products) {
      this.TotalQuantity = this.Products.reduce((total, item) => total + item.quantity, 0);
    }
  }
}
