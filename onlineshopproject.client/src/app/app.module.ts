import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { NavComponent } from './Components/nav/nav.component';
import { FooterComponent } from './Components/footer/footer.component';
import { HomeComponent } from './Components/home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { provideRouter } from '@angular/router';
import { routes } from './app-routing.module';
import { ProductsComponent } from './Components/products/products.component';
import { ContactComponent } from './Components/contact/contact.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './Components/login/login.component';
import { SignupComponent } from './Components/signup/signup.component';
import { ErrorComponent } from './Components/error/error.component';
import { ReactiveFormsModule } from '@angular/forms'
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './Services/AuthInterceptor/auth-interceptor.service';
import { UserComponent } from './Components/user/user.component';
import { ChangePasswordComponent } from './Components/change-password/change-password.component';
import { UsereditComponent } from './Components/useredit/useredit.component';
import { SearchComponent } from './Components/search/search.component';
import { ProductComponent } from './Components/product/product.component';
import { CartComponent } from './Components/cart/cart.component';
import { OrderComponent } from './Components/order/order.component';
import { OrdersComponent } from './Components/orders/orders.component';


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


