import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/user/user.service';
import { parseWebAPIErrors } from 'src/app/utilities/utils';
import { booksCreateDTO } from '../books.model';
import { BooksService } from '../books.service';

@Component({
  selector: 'app-insert-book',
  templateUrl: './insert-book.component.html',
  styleUrls: ['./insert-book.component.css']
})
export class InsertBookComponent implements OnInit {

  constructor(private router: Router, private formBuilder: FormBuilder, private bookService:BooksService, private userService:UserService) { }

  form!: FormGroup;
  errors:string[]=[];
  

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: ['', {
        validators:[Validators.required]
      }],
      author:['',{
        validators:[Validators.required]
      }],
      available: [0,{
        validators:[Validators.pattern("^[0-9]*$")]
      }]
    });
  }

  saveChanges(){
    if(this.userService.isAuthenticated()){
      this.bookService.insert(this.form.value).subscribe({
        next: () => {
          alert("inserted");
          this.form.reset();
        },
        error: error => this.errors=parseWebAPIErrors(error)
      });
    }else{
      alert("You need to log in first");
    }
  }

  errorMessageTitle(){
    const field=this.form.get('title');
    if(field?.hasError('required')){
      return 'The title field is required';
    }
    return '';
  }
  errorMessageAuthor(){
    const field=this.form.get('author');
    if(field?.hasError('required')){
      return 'The author field is required';
    }
    return '';
  }
  errorMessageAvailable(){
    const field=this.form.get('available');
    if(field?.hasError('pattern')){
      return 'Insert number of available books';
    }
    return '';
  }
}
