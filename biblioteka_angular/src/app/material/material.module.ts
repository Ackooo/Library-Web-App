import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import{MatToolbarModule} from '@angular/material/toolbar'
import{MatButtonModule} from '@angular/material/button'
import{MatIconModule} from '@angular/material/icon'
import {MatFormFieldModule} from '@angular/material/form-field'
import{MatInputModule} from '@angular/material/input'
import{MatCheckboxModule} from '@angular/material/checkbox'
import{MatDatepickerModule} from'@angular/material/datepicker'
import{MatNativeDateModule} from '@angular/material/core'
import {MatTableModule} from '@angular/material/table'
import{MatPaginatorModule} from '@angular/material/paginator'


@NgModule({
  declarations: [],
  exports:[
MatToolbarModule,
MatButtonModule,
MatIconModule,
MatFormFieldModule,
MatInputModule,
MatCheckboxModule,
MatNativeDateModule,
MatDatepickerModule,
MatTableModule,
MatPaginatorModule
  ],
  imports: [
    CommonModule,

  ]
})
export class MaterialModule { }
