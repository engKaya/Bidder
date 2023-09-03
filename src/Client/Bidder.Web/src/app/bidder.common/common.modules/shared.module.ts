import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialModule,
    TranslateModule,
    FormsModule
  ],
  exports: [
    MaterialModule,
    TranslateModule,
    FormsModule
  ]
})
export class SharedModule { }
