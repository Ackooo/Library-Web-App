import { Component, OnInit } from '@angular/core';
import { SignalrService } from 'src/app/utilities/signalr/signalr.service';
import { requestsDTO } from '../requests.model';
import { RequestsService } from '../requests.service';

@Component({
  selector: 'app-requests-librarian-issued',
  templateUrl: './requests-librarian-issued.component.html',
  styleUrls: ['./requests-librarian-issued.component.css']
})
export class RequestsLibrarianIssuedComponent implements OnInit {

  constructor(private requestService:RequestsService, private signalrService:SignalrService) { }

  ngOnInit(): void {
    this.fetchData();

    //signalr
    this.signalrService.startConnection().then(()=>{
    this.signalrService.listenToAllChanges();
      this.signalrService.allChanges.subscribe((data:string)=>{
        if(data==="requestsAllReturned"){
            this.fetchData();
        }else{
        }
      })
    })
  }

  requests!: requestsDTO[];

  fetchData() {
    this.requestService.getIssued().subscribe(requests=>{
      this.requests = requests;
     });
  }

  requestReturned(id:number){
    this.requestService.returned(id).subscribe(()=>{
     alert("returned");
    });
   }
}
