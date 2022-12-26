import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parseWebAPIErrors } from 'src/app/utilities/utils';
import {  userRegister } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private userService:UserService,  private router:Router, private formBuilder:FormBuilder) { }

  form!:FormGroup;
  errors:string[]=[];
  buttonName="Register";

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      email:['', {validators:[Validators.required, Validators.email]}],
      password:['', {validators:[Validators.required]}],
      type:[false]
    });
  }

  register(user:userRegister){
    let t:number;
    if(user.type){
      t=0;
    }else{
      t=1;
    }
    let u:userRegister=
    {
      email: user.email,
      password:user.password,
      type: t
    }   
    this.errors=[];
    this.userService.register(u).subscribe(authenticatorResponse=>{
      this.userService.saveTokenToLS(authenticatorResponse);
      this.router.navigate(['home'])
    }, err=>this.errors = parseWebAPIErrors(err));
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
