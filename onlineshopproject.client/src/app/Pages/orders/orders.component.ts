import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2'
import { OrderService } from '../../Services/Order/order.service';
import { jwtDecode } from 'jwt-decode';
import { UserService } from '../../Services/User/user.service';
import { User } from '../../Models/User';
import { Order } from '../../Models/Order';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {


  public User: User | null
  public Loaded: boolean
  public Orders: Order[] | null
  public TotalPrices: { [key: number]: number };
  public Quantities: { [key: number]: number };

  constructor(private _OrderService: OrderService,
    private _UserService: UserService)
  {
    this.User = null;
    this.Orders = null;
    this.Loaded = false
    this.TotalPrices = {};
    this.Quantities = {};
  }

  ngOnInit() {

    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {

          this.User = Response

          if (this.User != null) {
            this._OrderService.GetOrdersOfAnUser(Response.id).subscribe(
              response => {
                this.Orders = response
                this.Loaded = true

                console.log(response)

                this.Orders?.forEach(order => {
                  this._OrderService.GetTotalPriceOfOrder(order.id).subscribe(
                    totalprice => {
                      this.TotalPrices[order.id] = totalprice;
                    },
                    error => {
                      console.log(error);
                    }
                  )

                  this._OrderService.GetQuantityOfItemsInOrder(order.id).subscribe(
                    totalquantity => {
                      this.Quantities[order.id] = totalquantity;
                    }
                  )
                })
              },
              error => {
                console.log(error)
                this.Loaded = true
              }
            )
          }
        },
        error => {
          console.log(error)
          this.Loaded = true
        }
      )

    }

  }



  CancelOrder(id: any) {
    Swal.fire({

      title: 'Are you sure you want to cancel the order?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, cancel it!',
      cancelButtonText: 'No, keep it'

    })
    .then((result) => {

      if (result.isConfirmed) {
        this._OrderService.DeleteOrder(id).subscribe(
          response => {

            Swal.fire({
              text: "Order successfully cancelled",
              icon: "success"
            }).then(() => {
              window.location.reload();
            });

          },
          error => {

            console.log(error);
            Swal.fire({
              text: "An error occurred while cancelling the order",
              icon: "error"
            });

          }
        );
      }
    });
  }


}
