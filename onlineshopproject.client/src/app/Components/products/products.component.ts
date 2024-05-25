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

  constructor(private _ProductService: ProductService, private _router: Router) {

    this.products = []
    this.searchstring = '';
  }

  ngOnInit() {

    this._ProductService.GetProducts().subscribe(
      Response => {

        this.products = Response
        console.log(this.products)
      },

      error => {
        console.log(error);
      }
    )
  }

  productclick() {
    console.log("click");
  }

  Search() {
    this._router.navigate(['products/search/' + this.searchstring])
  }

}
