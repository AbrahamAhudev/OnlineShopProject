import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../../Models/Product';
import { ProductService } from '../../Services/Product/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public searchstring: string
  public products: Product[]
  public loaded: boolean

  constructor(private _ProductService: ProductService, private _router: Router) {

    this.products = []
    this.searchstring = '';
    this.loaded = false
  }

  ngOnInit() {

    this._ProductService.GetProducts().subscribe(
      Response => {

        this.products = Response
        this.loaded = true
        
      },

      error => {
        console.log(error);
      }
    )
  }

  productclick(productId: Number) {
   
    this._router.navigate(['products/' + productId])
  }

  Search() {
    this._router.navigate(['products/search/' + this.searchstring])
  }

}
