import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ProductService } from '../../Services/Product/product.service';


@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

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
