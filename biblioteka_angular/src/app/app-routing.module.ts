import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksAllComponent } from './books/books-all/books-all.component';
import { InsertBookComponent } from './books/insert-book/insert-book.component';
import { RequestsLibrarianIssuedComponent } from './requests/requests-librarian-issued/requests-librarian-issued.component';
import { RequestsLibrarianComponent } from './requests/requests-librarian/requests-librarian.component';
import { RequestsUserComponent } from './requests/requests-user/requests-user.component';
import { IsLibrarianGuard } from './security/is-librarian.guard';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';

const routes: Routes = [
  {path: '', component:BooksAllComponent},
  {path:'books/insert', component: InsertBookComponent, canActivate:[IsLibrarianGuard]},
  {path: 'requests/pending', component:RequestsLibrarianComponent,canActivate:[IsLibrarianGuard]},
  {path: 'requests/user', component:RequestsUserComponent},
  {path: 'requests/issued', component:RequestsLibrarianIssuedComponent,canActivate:[IsLibrarianGuard]},
  {path: 'login', component:LoginComponent},
  {path: 'register', component:RegisterComponent},
  {path: '**', redirectTo:''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
