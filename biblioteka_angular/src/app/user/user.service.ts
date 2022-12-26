import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { authenticationDTO, userLogin } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient:HttpClient) { }

  private apiURL = environment.apiUrl + "/user";

  register(user:userLogin): Observable<authenticationDTO>{
  return this.httpClient.post<authenticationDTO>( `${this.apiURL}/register`, user);
  }

  login(user:userLogin): Observable<authenticationDTO>{
    return this.httpClient.post<authenticationDTO>( `${this.apiURL}/login`, user);
  }

  logout(){
    localStorage.removeItem(this.token);
  }

  private readonly token:string='token';
  private readonly role:string ="role"; 

  isAuthenticated():boolean{
    const t = localStorage.getItem(this.token);
    if(!t){
      return false;
    }else{
      return true;
    }
  }
  getRole():string{
    return this.getFieldFromToken(this.role);
    
  }
  saveTokenToLS(auth:authenticationDTO){
    localStorage.setItem(this.token, auth.token);
  }
  getFieldFromToken(field:string):string{
    const t = localStorage.getItem(this.token);
    if(!t){
      return '';
    }else{
      const f = JSON.parse(atob(t.split('.')[1]));
      return f[field];
    }
  }

  getToken(){
    return localStorage.getItem(this.token);
  }
}
