import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Product } from '../../Models/Product';
import { ProductService } from '../../Services/Product/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  public ProductId: number
  public Product: Product | null
  constructor(private _router: Router,
    private _route: ActivatedRoute,
    private _ProductService: ProductService
  ) {
    this.ProductId = 0;
    this.Product = null;
  }

  ngOnInit() {

    this._route.params.subscribe((params: Params) => {
      this.ProductId = params["id"]


      this._ProductService.GetProduct(this.ProductId).subscribe(
        Response => {
          this.Product = Response
          console.log(this.Product)
        },
        (Error) => {
          console.log(Error)
        }
      )

    });
  }


}
