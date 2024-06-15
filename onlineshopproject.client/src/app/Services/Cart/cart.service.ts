import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Global } from 'src/app/Services/global';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  public url: string

  constructor(private _http: HttpClient)
  {
    this.url = Global.url;
  }


  AddToCart(UserId: number, ProductId: number): Observable<any> {

    var params = JSON.stringify(ProductId);

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this._http.post<any>(`${this.url}api/Cart/${UserId}/add`, params, { headers: headers })
  }

  DeleteCartItem(Id: number): Observable<any> {

    return this._http.delete<any>(`${this.url}api/CartItem/${Id}`)
  }

  getProductsOfACart(CartId: Number): Observable<any> {


    return this._http.get<any>(`${this.url}api/Cart/${CartId}/Products`)
  }

  }

