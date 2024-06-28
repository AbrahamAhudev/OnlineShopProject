import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import Swal from 'sweetalert2'
import { jwtDecode } from 'jwt-decode';
import { ProductService } from '../../Services/Product/product.service';
import { Product } from '../../Models/Product';
import { UserService } from '../../Services/User/user.service';
import { ProductDTO } from '../../Models/ProductDTO';


@Component({
  selector: 'app-createproduct',
  templateUrl: './createproduct.component.html',
  styleUrls: ['./createproduct.component.css']
})
export class CreateproductComponent implements OnInit {


  public ProductForm: FormGroup
  public UserId: number
  private imageFile: File | null = null;

  constructor(private _router: Router,
    private fb: FormBuilder,
    private _ProductService: ProductService,
    private _UserService: UserService) {

    this.UserId = 0

    this.ProductForm = this.fb.group({

      name: ['', Validators.required],
      price: [null, Validators.required],
      description: [''],
      image: ['']


    })


  }

  ngOnInit() {
    const jwt = localStorage.getItem("token");

    if (jwt) {
      const decodedJWT: any = jwtDecode(jwt);

      const UserName = decodedJWT.sub;

      this._UserService.getUserByUsername(UserName).subscribe(
        Response => {

          this.UserId = Response.id


        },
        error => {
          console.log(error)
        }
      )

    }
  }


  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.imageFile = file;
    }
  }


  CreateProduct(){

    const formData: FormData = new FormData();

    formData.append('Name', this.ProductForm.get('name')?.value);

    formData.append('Price', this.ProductForm.get('price')?.value.toString());

    formData.append('Description', this.ProductForm.get('description')?.value || '');

    formData.append('UserId', this.UserId.toString());

    if (this.imageFile) {
      formData.append('Image', this.imageFile, this.imageFile.name);
    }

    

    this._ProductService.CreateProduct(formData).subscribe(
      response => {
        Swal.fire({
          icon: "success",
          text: "product created successfully"
        }).then(() => {
          this._router.navigateByUrl('products')
        })
      },
      error => {

        console.log(error);

        Swal.fire({
          icon: "error",
          text: "error creating the product"
        })
      })
  }

}
