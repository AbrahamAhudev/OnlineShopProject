import { Component, OnInit } from '@angular/core';
import { CartService } from '../../Services/Cart/cart.service';
import { CartItemDTO } from '../../Models/CartItemDTO';
import { User } from '../../Models/User';
import { UserService } from '../../Services/User/user.service';
import { jwtDecode } from 'jwt-decode';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  public User: User | null;
  public Products: CartItemDTO[] | null;
  public Loaded: boolean
  public CartId: number | undefined;
  public TotalPrice: number;


  constructor(private _CartService: CartService,
    private _UserService: UserService) {
    this.Products = null;
    this.User = null;
    this.Loaded = false;
    this.CartId = undefined;
    this.TotalPrice = 0;
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

          if (this.CartId != null) {
            this._CartService.getProductsOfACart(this.CartId).subscribe(
              Response => {
                
                this.Products = Response;
                this.calculateTotalPrice();
                this.Loaded = true;      
                console.log(this.Products)

              },
              error => {
                console.log(error)
              })
          } else {
            this.Loaded = true;
          }
        },
        error => {
          console.log(error)
        }
      )

    }


  

  }

  calculateTotalPrice() {
    if (this.Products) {
      this.TotalPrice = this.Products.reduce((total, item) => total + (item.productPrice * item.quantity), 0);
    }
  }


  DeleteCartItem(CartItemId: number) {

    this._CartService.DeleteCartItem(CartItemId).subscribe(
      Response => {
        Swal.fire({
          text: "Cart item deleted successfully",
          icon: "success"
        }).then(() => {
          window.location.reload();
        })
      },
      Error => {

      }
    )
  }
  
}


            
