import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BiddingPupComponent } from './bidding-pup/bidding-pup.component';
import { MaterialModule } from 'src/app/material.module';  
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    BiddingPupComponent 
  ],
  imports: [
    CommonModule,
    MaterialModule, 
    FormsModule, 
  ]
})
export class BiddingModule { }
