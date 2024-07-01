import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './Pages/contact/contact.component';
import { ErrorComponent } from './Pages/error/error.component';
import { UserComponent } from './Pages/user/user.component';
import { HomeComponent } from './Pages/home/home.component';
import { LoginComponent } from './Pages/login/login.component';
import { ProductsComponent } from './Pages/products/products.component';
import { SignupComponent } from './Pages/signup/signup.component';
import { AuthGuard } from './Guards/Auth/auth.guard';
import { GuestGuard } from './Guards/Guest/guest.guard';
import { ChangePasswordComponent } from './Pages/change-password/change-password.component';
import { UsereditComponent } from './Pages/useredit/useredit.component';
import { SearchComponent } from './Pages/search/search.component';
import { ProductComponent } from './Pages/product/product.component';
import { CartComponent } from './Pages/cart/cart.component';
import { OrderComponent } from './Pages/order/order.component';
import { OrdersComponent } from './Pages/orders/orders.component';
import { AdminGuard } from './Guards/Admin/admin.guard';
import { AdminpageComponent } from './Pages/adminpage/adminpage.component';
import { SellerComponent } from './Pages/seller/seller.component';
import { SellerGuard } from './Guards/Seller/seller.guard';
import { CreateproductComponent } from './Pages/createproduct/createproduct.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'products/:id', component: ProductComponent },
  { path: 'products/search/:searchstring', component: SearchComponent },
  { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
  { path: 'user', component: UserComponent, canActivate: [GuestGuard] },
  { path: 'user/edit', component: UsereditComponent, canActivate: [GuestGuard] },
  { path: 'cart', component: CartComponent, canActivate: [GuestGuard] },
  { path: 'cart/order', component: OrderComponent, canActivate: [GuestGuard] },
  { path: 'signup', component: SignupComponent },
  { path: 'user/changepwd', component: ChangePasswordComponent, canActivate: [GuestGuard] },
  { path: 'orders', component: OrdersComponent, canActivate: [GuestGuard] },
  { path: 'admin', component: AdminpageComponent, canActivate: [AdminGuard] },
  { path: 'seller', component: SellerComponent, canActivate: [SellerGuard] },
  { path: 'seller/product', component: CreateproductComponent, canActivate: [SellerGuard] },
  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [
   RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

