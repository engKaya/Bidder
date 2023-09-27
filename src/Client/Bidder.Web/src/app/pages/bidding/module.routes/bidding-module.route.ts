import { Routes } from '@angular/router'; 
import { BidComponent } from '../module.pages/bid/bid.component';

export const PagesRoutes: Routes = [
  {
    path: 'bid/:id',
    component: BidComponent
  },
];
