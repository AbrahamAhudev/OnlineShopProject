import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Product } from '../../Models/Product';
import { ProductService } from '../../Services/Product/product.service';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  public SearchString: string
  public ParamString: string
  public products: Product[]
  constructor(private _ProductService: ProductService,
    private _router: Router,
    private _route: ActivatedRoute) {
    this.products = []
    this.ParamString = '';
    this.SearchString = '';
  }

  ngOnInit() {
     

    this._route.params.subscribe((params: Params) => {
      this.ParamString = params["searchstring"]


      this._ProductService.SearchProduct(this.ParamString).subscribe(
        Response => {
          this.products = Response
        },
        (Error) => {
          console.log(Error)
        }
      )
  
    });

  }

  productclick() {
    console.log("click")
  }

  Search() {
    this._router.navigate(['/products/search/' + this.SearchString])
  }


}
