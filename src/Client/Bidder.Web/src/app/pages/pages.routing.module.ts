import { Routes } from '@angular/router';
import { AppDashboardComponent } from './dashboard/dashboard.component';

export const PagesRoutes: Routes = [
  {
    path: '',
    component: AppDashboardComponent
  },
  {
    path: 'bidding',
    loadChildren: () => import('./bidding/bidding.module').then(m => m.BiddingModule)

  }
];
