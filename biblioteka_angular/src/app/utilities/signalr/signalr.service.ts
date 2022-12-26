import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { UserService } from 'src/app/user/user.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(private userService:UserService) { }

  private hubConnection!: signalR.HubConnection;
  private refreshDataHub: string = "refreshdata";
  private apiUrl = environment.apiUrl;

  private connectionId!:string;
  private connectionGroup!:string;

 private $recieved: Subject<string> = new Subject<string>();

  public startConnection(){
    return new Promise((resolve, reject)=>{

      this.hubConnection=new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/` + this.refreshDataHub)
      .build();

      this.hubConnection
      .start()
      .then(()=>{
        console.log('Signalr - connection started');
        return resolve(true);
      })
      .then(()=> this.getConnectionId())
      .then(()=>this.getConnectionGroup())
      .catch(err=> console.log('Signalr - error while starting connection: ' + err)
      )
    })   
  }

   public listenToAllChanges = () => {
    this.hubConnection.on('RefreshDataMethod', (data:string) => {
      this.$recieved.next(data);
    });
  }

  public get allChanges():Observable<string>{
    return this.$recieved.asObservable();
  }

  private getConnectionId=()=>{
    let connGroup = this.userService.getFieldFromToken('email');
    this.hubConnection.invoke('getconnectionid', connGroup)
    .then((data)=>{
      this.connectionId=data;
    });
  }

  private getConnectionGroup=()=>{
    let connGroup = this.userService.getRole();
    this.hubConnection.invoke('JoinGroup', connGroup)
    .then((data)=>{
      this.connectionGroup=data;
    });
  }
}