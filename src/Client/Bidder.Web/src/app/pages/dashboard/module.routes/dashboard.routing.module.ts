import { Routes } from '@angular/router'; 
import { DashboardComponent } from '../module.pages/dashboard.component';

export const DashboardRoutes: Routes = [
    {
        path: '',  
        component: DashboardComponent
    }, 
    {
        path:'**',
        redirectTo: ''
    }

];