import { Component, Input,OnInit } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { SignalrService } from 'src/app/utilities/signalr/signalr.service';
import { formatDateFormData } from 'src/app/utilities/utils';
import { requestsDTO } from '../requests.model';
import { RequestsService } from '../requests.service';

@Component({
  selector: 'app-requests-librarian',
  templateUrl: './requests-librarian.component.html',
  styleUrls: ['./requests-librarian.component.css']
})
export class RequestsLibrarianComponent implements OnInit {

  constructor(private requestService:RequestsService, private signalrService:SignalrService) { }

  ngOnInit(): void {
    this.fetchData();

    //signalr
    this.signalrService.startConnection().then(()=>{
    this.signalrService.listenToAllChanges();
      this.signalrService.allChanges.subscribe((data:string)=>{
        if(data==="requestsAll"){
            this.fetchData();
            //alert("You have more requests");
        }else{
        }
      })
    })
  }

  requests: requestsDTO[]=[];
  todayDate:Date = new Date();

  fetchData() {
    this.requestService.getPending().subscribe(requests=>{
      this.requests = requests;
     });
  }
  
  requestAccept(id:number, dateOfReturn:string){  
    this.requestService.accept(id, dateOfReturn).subscribe(()=>{
     alert("accepted");    
    });
   }

   requestDeny(id:number){
    this.requestService.deny(id).subscribe(()=>{
     alert("denied");
    });
   }
 
   addev(id:number,  event: MatDatepickerInputEvent<Date>){
     let r = this.requests.find(x=>x.id===id);
     if(r!==undefined && event.value!==null){
       r.dateOfReturn= formatDateFormData(event.value);
     }
   }
}
