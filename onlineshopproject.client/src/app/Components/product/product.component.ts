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
import { RoleService } from '../../Services/Role/role.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  public ProductId: number
  public Product: Product | null
  public UserId: number
  public Loaded: boolean
  public isAdmin: boolean
  public isSeller: boolean
  constructor(private _router: Router,
    private _route: ActivatedRoute,
    private _ProductService: ProductService,
    private _CartService: CartService,
    private _UserService: UserService,
    private _RoleService: RoleService
  ) {
    this.ProductId = 0;
    this.Product = null;
    this.UserId = 0;
    this.Loaded = false;
    this.isAdmin = false;
    this.isSeller = false;
   
  }

  ngOnInit() {


    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {

          this.UserId = Response.id

          if (this.UserId != null) {

            this._RoleService.getUserRole(this.UserId).subscribe(
              response => {
                console.log(response)

                if (response.roleId == 2) {
                  this.isAdmin = true
                }
              },
              error => {
                console.log(error)
              }
            )

            this._ProductService.GetProduct(this.ProductId).subscribe(
              response => {

              

                if (this.UserId == response.userId)
                  this.isSeller = true
                  


              },
              error => {
                console.log(error);

              }
            )
          
          }


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

  deleteproduct(productId: number) {

    Swal.fire({
      title: '¿are you sure?',
      text: "¡you can't revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'delete it!',
      cancelButtonText: 'cancel'
    }).then((result) => {
      if (result.isConfirmed) {

        this._ProductService.DeleteProduct(productId).subscribe(
          response => {
            Swal.fire({
              icon: "success",
              text: "product deleted successfully"
            }).then(() => {
              this._router.navigateByUrl('/products')
            })

          },
          error => {
            console.log(error)
          }
        );
      }
    });
  }

  IsSeller(UserId: number, ProductId: number): boolean{


    this._ProductService.GetProduct(ProductId).subscribe(
      response => {

        if (this.UserId == UserId) {
          return true;
        } else {
          return false;
        }

      },
      error => {
        console.log(error);
        return false;
      }
    )

    return false
  }


}
