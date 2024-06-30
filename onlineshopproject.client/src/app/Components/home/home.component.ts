import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Product } from '../../Models/Product';
import { ProductService } from '../../Services/Product/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {

  public SearchString: string
  public Products: Product[]
  constructor(private _router: Router,
    private _ProductService: ProductService) {
    this.SearchString = '';
    this.Products = []
  }

  ngOnInit() {
    this._ProductService.GetRecentProducts().subscribe(
      response => {
        this.Products = response
      },
      error => {
        console.log(error)
      }
    )
  }
  productclick(productId: Number) {

    this._router.navigate(['products/' + productId])
  }

  Search() {
    this._router.navigate(['/products/search/' + this.SearchString])
  }
}
