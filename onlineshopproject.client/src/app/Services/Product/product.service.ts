import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Global } from 'src/app/Services/global';
import { Product } from "../../Models/Product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {


  public url: string
  constructor(private _http: HttpClient) {
    this.url = Global.url;
  }

  GetProducts(): Observable<any> {

    return this._http.get(this.url + 'api/Product')
  }

  GetProduct(id: number): Observable<any>  {

    return this._http.get(this.url + 'api/Product/' + id)
  }

  SearchProduct(SearchString: string): Observable<any> {
    return this._http.get(this.url + 'api/Product/search/' + SearchString)
  }

  CreateProduct(product: Product): Observable<any>  {
    let params = JSON.stringify(product);

    let headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this._http.post(this.url + 'api/Product', params, { headers: headers });
  }

}
