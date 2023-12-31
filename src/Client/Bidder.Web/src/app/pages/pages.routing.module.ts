import { Routes } from '@angular/router'; 

export const PagesRoutes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full', 
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'bidding',
    loadChildren: () => import('./bidding/bidding.module').then(m => m.BiddingModule) 
  },
  {
    path:'**',
    redirectTo: 'dashboard'
  }
];
