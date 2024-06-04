import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Product } from '../../Models/Product';
import { User } from '../../Models/User';
import { CartService } from '../../Services/Cart/cart.service';
import { ProductService } from '../../Services/Product/product.service';
import { UserService } from '../../Services/User/user.service';
import Swal from 'sweetalert2'
import { Token } from '@angular/compiler';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  public ProductId: number
  public Product: Product | null
  public UserId: number | null
  public Loaded: boolean
  constructor(private _router: Router,
    private _route: ActivatedRoute,
    private _ProductService: ProductService,
    private _CartService: CartService,
    private _UserService: UserService
  ) {
    this.ProductId = 0;
    this.Product = null;
    this.UserId = null;
    this.Loaded = false;
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


    this._route.params.subscribe((params: Params) => {
      this.ProductId = params["id"]


      this._ProductService.GetProduct(this.ProductId).subscribe(
        Response => {
          this.Product = Response
          this.Loaded = true;
          console.log(this.Product)
        },
        (Error) => {
          this.Loaded = true;
          console.log(Error)
        }
      )

    });
  }

  AddToCart() {

    const jwt = localStorage.getItem("token");

    if (jwt) {


      if (this.UserId != null && this.ProductId != null) {
        this._CartService.AddToCart(this.UserId, this.ProductId).subscribe(
          Response => {
            Swal.fire({
              text: "product added to cart successfully",
              icon: "success"
            })

          },
          Error => {

          })

      }


    } else {
      Swal.fire({
        text: "you need to be logged to add products to the cart",
        icon: "error"
      })
    }

    }

   

  }



