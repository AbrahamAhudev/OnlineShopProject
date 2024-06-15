import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Global } from 'src/app/Services/global';


@Injectable({
  providedIn: 'root'
})
export class OrderService {

  public url: string

  constructor(private _http: HttpClient)
  {
    this.url = Global.url;
  }

  Makeorder(cartid: number, address: string): Observable<any>
  {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    const param = JSON.stringify(address);

    return this._http.post<any>(`${this.url}api/Order/${cartid}`, param, { headers: headers })
  }

  GetOrdersOfAnUser(userid: number): Observable<any>
  {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this._http.get<any>(`${this.url}api/Order/user/${userid}`, { headers: headers })
  }


  GetTotalPriceOfOrder(Orderid: number): Observable<any>{

    return this._http.get<any>(`${this.url}api/Order/${Orderid}/price`);
  }

  GetQuantityOfItemsInOrder(Orderid: number): Observable<any> {

    return this._http.get<any>(`${this.url}api/Order/${Orderid}/quantity`);
  }


  DeleteOrder(Orderid: number): Observable<any> {

    return this._http.delete<any>(`${this.url}api/Order/${Orderid}`);
  }
}
