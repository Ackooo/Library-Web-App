import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { InsertBookComponent } from './books/insert-book/insert-book.component';
import { MenuComponent } from './menu/menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import{FormsModule, ReactiveFormsModule} from '@angular/forms'

import { MaterialModule } from './material/material.module';
import { GenericListComponent } from './utilities/generic-list/generic-list.component';
import { DisplayErrorsComponent } from './utilities/display-errors/display-errors.component';
import { BooksAllComponent } from './books/books-all/books-all.component';
import { RequestsUserComponent } from './requests/requests-user/requests-user.component';
import { RequestsLibrarianComponent } from './requests/requests-librarian/requests-librarian.component';
import { RequestsLibrarianIssuedComponent } from './requests/requests-librarian-issued/requests-librarian-issued.component';
import { AuthorizeViewComponent } from './security/authorize-view/authorize-view.component';
import { HttpInterceptorService } from './security/http-interceptor.service';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    InsertBookComponent,
    MenuComponent,
    GenericListComponent,
    DisplayErrorsComponent,
    BooksAllComponent,
    RequestsUserComponent,
    RequestsLibrarianComponent,
    RequestsLibrarianIssuedComponent,
    AuthorizeViewComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpInterceptorService,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
