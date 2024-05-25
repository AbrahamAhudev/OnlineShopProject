import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ProductService } from '../../Services/Product/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {

  public SearchString: string
  constructor(private _router: Router) {
    this.SearchString = '';
  }

  ngOnInit() {

  }

  Search() {
    this._router.navigate(['/products/search/' + this.SearchString])
  }
}
