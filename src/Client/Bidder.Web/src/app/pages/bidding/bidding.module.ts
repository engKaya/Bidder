import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BiddingPupComponent } from './module.components/bidding-pup/bidding-pup.component'; 
import { SharedModule } from 'src/app/bidder.common/common.modules/shared.module';   
import { NumericDirective } from 'src/app/bidder.common/common.services/directives/NumericDirective.directive';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
    BiddingPupComponent
  ],
})
export class BiddingModule { }
