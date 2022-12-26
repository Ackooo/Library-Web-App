import { Component, Input,OnDestroy,OnInit } from '@angular/core';
import { BooksService } from '../books.service';
import * as signalR from "@microsoft/signalr"
import { environment } from 'src/environments/environment';
import { SignalrService } from 'src/app/utilities/signalr/signalr.service';
import { HttpResponse } from '@angular/common/http';
import { booksDTO } from '../books.model';
import { RequestsService } from 'src/app/requests/requests.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { requestsCreateDTO } from 'src/app/requests/requests.model';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-books-all',
  templateUrl: './books-all.component.html',
  styleUrls: ['./books-all.component.css']
})
export class BooksAllComponent implements OnInit, OnDestroy {

  private apiUrl = environment.apiUrl;

  form!: FormGroup;

  constructor(private booksService: BooksService, private signalrService:SignalrService, private requestService:RequestsService, private formBuilder: FormBuilder) { }
  ngOnDestroy(): void {

  }

  ngOnInit(): void {
    this.fetchData();

    this.form = this.formBuilder.group({
      num: [,{
        validators:[Validators.pattern("^[0-9]*$")]
      }]
    });

    this.signalrService.startConnection().then(()=>{
    this.signalrService.listenToAllChanges();
      this.signalrService.allChanges.subscribe((data:string)=>{
        if(data==="booksAll"){
            this.fetchData();
        }else{
        }
      })
    })
  }

  books: any;
  maxNumberOfBooks:any;
  currentPage = 1;
  pageSize=10;

  fetchData() {
     this.booksService.getAll(this.currentPage, this.pageSize).subscribe((response: HttpResponse<booksDTO[]>)=>{
      this.books=response.body;
      this.maxNumberOfBooks=response.headers.get("maxNumberOfUnits");
     });
  }

  request(bookId: number){
    const req : requestsCreateDTO={
      bookId:bookId,
    }
    this.requestService.insert(req).subscribe(()=>{
      alert("requested");
    });
  }

  add(bookId:number){
    const num=this.form.value.num;
    this.booksService.add(bookId, num).subscribe(()=>{
      alert("added");
      this.form.reset();
    })
  }

  fetchPage(event:PageEvent){
    this.currentPage=event.pageIndex+1;
    this.pageSize=event.pageSize;
    this.fetchData();
  }
}
