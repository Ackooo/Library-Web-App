import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parseWebAPIErrors } from 'src/app/utilities/utils';
import { userLogin } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private userService:UserService, private router:Router, private formBuilder:FormBuilder) { }

  form!:FormGroup;

  errors:string[]=[];
  buttonName="Login";

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      email:['', {validators:[Validators.required, Validators.email]}],
      password:['', {validators:[Validators.required]}]
    });
  }

  login(userLogin:userLogin){
    this.userService.login(userLogin).subscribe(authResponse=>{
      this.userService.saveTokenToLS(authResponse);
      this.router.navigate(['home']);
    }, err=>this.errors=parseWebAPIErrors(err));
   
  }

  getEmailErrorMessage(){
    var f = this.form.get('email');
    if(f?.hasError('required')){
      return "The email is required";
    }
    if(f?.hasError('email')){
      return "The email is invalid";
    }
    return ;
  }

}
