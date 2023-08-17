import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FullComponent } from './layouts/full/full.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./pages/authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      ),
  },
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: '',
        redirectTo: '/dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./pages/pages.module').then((m) => m.PagesModule),
      },
    ],
  },
  // {
  //   path: '',
  //   component: FullComponent,
  //   children: [
  //     {
  //       path: '',
  //       redirectTo: '/dashboard',
  //       pathMatch: 'full',
  //     },
  //     {
  //       path: 'dashboard',
  //       loadChildren: () =>
  //         import('./pages/pages.module').then((m) => m.PagesModule),
  //     },
  //     {
  //       path: 'ui-components',
  //       loadChildren: () =>
  //         import('./pages/ui-components/ui-components.module').then(
  //           (m) => m.UicomponentsModule
  //         ),
  //     },
  //     {
  //       path: 'extra',
  //       loadChildren: () =>
  //         import('./pages/extra/extra.module').then((m) => m.ExtraModule),
  //     },
  //   ],
  // },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
