import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Global } from 'src/app/Services/global';
import { Product } from "../../Models/Product";
import { ProductDTO } from '../../Models/ProductDTO';

@Injectable({
  providedIn: 'root'
})
export class ProductService {


  public url: string
  constructor(private _http: HttpClient) {
    this.url = Global.url;
  }

  GetProducts(): Observable<any> {

    return this._http.get(this.url + 'api/Product');
  }

  GetProduct(id: number): Observable<any>  {

    return this._http.get(this.url + 'api/Product/' + id);
  }

  GetRecentProducts(): Observable<any> { 
    return this._http.get(this.url + 'api/Product/latests')
  }

  GetProductsOfUser(UserId: number): Observable<any> {
    return this._http.get(this.url + 'api/Product/user/' + UserId);
  }

  SearchProduct(SearchString: string): Observable<any> {
    return this._http.get(this.url + 'api/Product/search/' + SearchString);
  }

  CreateProduct(formData: FormData): Observable<any> {
    return this._http.post(this.url + 'api/Product', formData, {
      headers: new HttpHeaders({
        'enctype': 'multipart/form-data'
      }),
      responseType: 'text' as 'json'
    });
  }

  DeleteProduct(ProductId: number): Observable<any> {

    return this._http.delete(this.url + 'api/Product/' + ProductId);
  }
}
