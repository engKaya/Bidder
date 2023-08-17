import { Routes } from '@angular/router';

import { AppSideLoginComponent } from './module.pages/login.page/login.component';
import { AppSideRegisterComponent } from './module.pages/register.page/register.component';

export const AuthenticationRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'login',
        component: AppSideLoginComponent,
      },
      {
        path: 'register',
        component: AppSideRegisterComponent,
      },
    ],
  },
];
