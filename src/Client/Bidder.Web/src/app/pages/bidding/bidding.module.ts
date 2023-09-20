import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BiddingPupComponent } from './module.components/bidding-pup/bidding-pup.component'; 
import { SharedModule } from 'src/app/bidder.common/common.modules/shared.module';    
import { BidComponent } from './module.pages/bid/bid.component';
import { RouterModule } from '@angular/router';
import { PagesRoutes } from './module.routes/bidding-module.route';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(PagesRoutes),

  ],
  declarations: [
    BiddingPupComponent,
    BidComponent
  ],
})
export class BiddingModule { }
