import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { requestsCreateDTO, requestsDTO } from './requests.model';

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  constructor(private httpClient:HttpClient) { }

  private apiUrl = environment.apiUrl + '/requests';

 getAll(): Observable<requestsDTO[]>{ 
    return this.httpClient.get<requestsDTO[]>(this.apiUrl);
  }

  getPending():Observable<requestsDTO[]>{ 
    return this.httpClient.get<requestsDTO[]>(`${this.apiUrl}/pending`);
  }

  getIssued():Observable<requestsDTO[]>{ 
    return this.httpClient.get<requestsDTO[]>(`${this.apiUrl}/issued`);
  }

  getByUser():Observable<requestsDTO[]>{
    return this.httpClient.get<requestsDTO[]>(`${this.apiUrl}/user`);
  }

  getByUserOver():Observable<requestsDTO[]>{ 
    return this.httpClient.get<requestsDTO[]>(`${this.apiUrl}/user/over`);
  }

  insert(request: requestsCreateDTO){
    return this.httpClient.post(this.apiUrl, request);
  }

  accept(id:number, dateOfReturn:string){
    const headers = new HttpHeaders({
        'Content-Type': 'application/json'
    }); 
    let body = JSON.stringify(dateOfReturn);
    return this.httpClient.put(`${this.apiUrl}/accept/${id}`, body,  {headers: headers});
  }

  deny(id:number){
    return this.httpClient.put(`${this.apiUrl}/deny/${id}`, 0);
  }

  returned(id:number){
    return this.httpClient.put(`${this.apiUrl}/return/${id}`, 0);
  }

}
