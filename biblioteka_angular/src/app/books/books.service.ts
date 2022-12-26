import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { booksCreateDTO, booksDTO } from './books.model';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  constructor(private httpClient:HttpClient) { }

  private apiUrl = environment.apiUrl + '/books';

  getAll(page:number, UnitsPerPage:number):Observable<any>{
    let params = new HttpParams();
   params=params.append('page', page.toString());
    params=params.append('unitsPerPage', UnitsPerPage.toString());
    return this.httpClient.get<booksDTO[]>(this.apiUrl, {observe:'response', params});
  }

  getById(id:number):Observable<booksDTO>{
    return this.httpClient.get<booksDTO>(`${this.apiUrl}/${id}`);
  }

  insert(book: booksCreateDTO){
    return this.httpClient.post(this.apiUrl, book);
  }

  add(id:number, num:number){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
  }); 
    let body = JSON.stringify(num);
    return this.httpClient.put(`${this.apiUrl}/${id}`, body, {headers: headers});
  }
}