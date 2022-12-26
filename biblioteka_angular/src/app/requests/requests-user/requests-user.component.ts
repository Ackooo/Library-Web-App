import { Component, Input, OnInit } from '@angular/core';
import { UserService } from 'src/app/user/user.service';
import { SignalrService } from 'src/app/utilities/signalr/signalr.service';
import { requestsDTO } from '../requests.model';
import { RequestsService } from '../requests.service';

@Component({
  selector: 'app-requests-user',
  templateUrl: './requests-user.component.html',
  styleUrls: ['./requests-user.component.css']
})
export class RequestsUserComponent implements OnInit {

  constructor(private requestService:RequestsService, private signalrService:SignalrService ) { }

  ngOnInit(): void {
    this.fetchDataByUser();
    this.fetchDataByUserOver();

    //signalr
    this.signalrService.startConnection().then(()=>{
    this.signalrService.listenToAllChanges();
      this.signalrService.allChanges.subscribe((data:string)=>{
        if(data==="requestsAll"){
            this.fetchData();
        }else{
        }
      })
    })
  }

  requests!: requestsDTO[];
  requestsOver!:requestsDTO[];

  fetchData(){
    this.fetchDataByUser();
    this.fetchDataByUserOver();
  }

  fetchDataByUser(){
    this.requestService.getByUser().subscribe(requests=>{
      this.requests = requests;
    });
  }

  fetchDataByUserOver(){
    this.requestService.getByUserOver().subscribe(requests=>{
      this.requestsOver = requests;       
     });
  }

  resolveState(num:number){
    switch (num) {
      case 0:
          return 'pending';
      case 1:
        return 'reading now';
      case 2:
        return 'returned';
      case 3:
        return 'denied';
      default:
          return;
    }
  }
}
