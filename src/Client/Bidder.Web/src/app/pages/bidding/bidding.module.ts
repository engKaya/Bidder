import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BiddingPupComponent } from './bidding-pup/bidding-pup.component'; 
import { SharedModule } from 'src/app/bidder.common/common.modules/shared.module';

@NgModule({
  declarations: [
    BiddingPupComponent 
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class BiddingModule { }
