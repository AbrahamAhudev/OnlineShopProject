import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './Components/contact/contact.component';
import { ErrorComponent } from './Components/error/error.component';
import { UserComponent } from './Components/user/user.component';
import { HomeComponent } from './Components/home/home.component';
import { LoginComponent } from './Components/login/login.component';
import { ProductsComponent } from './Components/products/products.component';
import { SignupComponent } from './Components/signup/signup.component';
import { AuthGuard } from './Guards/Auth/auth.guard';
import { GuestGuard } from './Guards/Guest/guest.guard';
import { ChangePasswordComponent } from './Components/change-password/change-password.component';
import { UsereditComponent } from './Components/useredit/useredit.component';
import { SearchComponent } from './Components/search/search.component';
import { ProductComponent } from './Components/product/product.component';
import { CartComponent } from './Components/cart/cart.component';
import { OrderComponent } from './Components/order/order.component';

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
  { path: 'user/changepwd', component: ChangePasswordComponent, canActivate: [GuestGuard]},
  { path: '**', component: ErrorComponent }
];

@NgModule({
  imports: [
   RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

