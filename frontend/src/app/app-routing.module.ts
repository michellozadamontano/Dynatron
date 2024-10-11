import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'admin/customers',
    pathMatch: 'full'
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      {
        path: 'customers',
        loadComponent: () => import('./customers/containers/customer-list/customer-list.component').then(m => m.CustomerListComponent)
      }

    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
