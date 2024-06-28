import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Product } from '../../Models/Product';
import { ProductService } from '../../Services/Product/product.service';
import { UserService } from '../../Services/User/user.service';

@Component({
  selector: 'app-seller',
  templateUrl: './seller.component.html',
  styleUrls: ['./seller.component.css']
})
export class SellerComponent implements OnInit {


  public products: Product[]
  public loaded: boolean
  public UserId: number

  constructor(private _UserService: UserService,
    private _ProductService: ProductService,
    private _router: Router) {
    this.products = []
    this.loaded = false
    this.UserId = 0
  }

  ngOnInit() {
    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {

          this.UserId = Response.id
         


         

          this._ProductService.GetProductsOfUser(this.UserId).subscribe(
            response => {

              if (response.length > 0) {
                this.products = response
              } 
             
              this.loaded = true
            },
            error => {
              console.log(error)
              this.loaded = true
            }
          )
        
        },
        error => {
          console.log(error)
          this.loaded = true
        }
      )

    }
  }

  productclick(productId: Number) {

    this._router.navigate(['products/' + productId])
  }


  CreateProduct() {
    this._router.navigateByUrl('seller/product')
  }
}
