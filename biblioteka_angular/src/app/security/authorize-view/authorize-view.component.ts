import { Component, Input, OnInit } from '@angular/core';
import { UserService } from 'src/app/user/user.service';

@Component({
  selector: 'app-authorize-view',
  templateUrl: './authorize-view.component.html',
  styleUrls: ['./authorize-view.component.css']
})
export class AuthorizeViewComponent implements OnInit {

  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }

  @Input()
  role!: string;

  public isAuthorized(){
    if(this.role){
      return this.userService.getRole()===this.role;
    }else{
      return this.userService.isAuthenticated();
    }
    
  }

}
