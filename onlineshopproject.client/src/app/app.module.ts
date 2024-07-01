import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { NavComponent } from './Components/nav/nav.component';
import { FooterComponent } from './Components/footer/footer.component';
import { HomeComponent } from './Pages/home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { provideRouter } from '@angular/router';
import { routes } from './app-routing.module';
import { ProductsComponent } from './Pages/products/products.component';
import { ContactComponent } from './Pages/contact/contact.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './Pages/login/login.component';
import { SignupComponent } from './Pages/signup/signup.component';
import { ErrorComponent } from './Pages/error/error.component';
import { ReactiveFormsModule } from '@angular/forms'
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './Services/AuthInterceptor/auth-interceptor.service';
import { UserComponent } from './Pages/user/user.component';
import { ChangePasswordComponent } from './Pages/change-password/change-password.component';
import { UsereditComponent } from './Pages/useredit/useredit.component';
import { SearchComponent } from './Pages/search/search.component';
import { ProductComponent } from './Pages/product/product.component';
import { CartComponent } from './Pages/cart/cart.component';
import { OrderComponent } from './Pages/order/order.component';
import { OrdersComponent } from './Pages/orders/orders.component';
import { AdminpageComponent } from './Pages/adminpage/adminpage.component';
import { SellerComponent } from './Pages/seller/seller.component';
import { CreateproductComponent } from './Pages/createproduct/createproduct.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NavComponent,
    FooterComponent,
    HomeComponent,
    ProductsComponent,
    ContactComponent,
    LoginComponent,
    SignupComponent,
    ErrorComponent,
    UserComponent,
    ChangePasswordComponent,
    UsereditComponent,
    SearchComponent,
    ProductComponent,
    CartComponent,
    OrderComponent,
    OrdersComponent,
    AdminpageComponent,
    SellerComponent,
    CreateproductComponent,


  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule, FormsModule,
    ReactiveFormsModule
  ],
  providers: [provideRouter(routes),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],

  bootstrap: [AppComponent]
})
export class AppModule { }


