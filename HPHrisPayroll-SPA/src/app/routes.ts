import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MaintCompanyComponent } from './maint-company/maint-company.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        children: [
            { path: 'maint-company', component: MaintCompanyComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
