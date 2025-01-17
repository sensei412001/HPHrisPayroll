import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MaintCompanyComponent } from './maint-company/maint-company.component';
import { AuthGuard } from './_guards/auth.guard';
import { UserGroupsComponent } from './user-groups/user-groups.component';
import { UsersComponent } from './users/users.component';
import { MaintDepartmentsComponent } from './maint-departments/maint-departments.component';
import { MainEmployeeNumberingComponent } from './main-employeeNumbering/main-employeeNumbering.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'maint-company', component: MaintCompanyComponent, data: { roles: ['Company'] } },
            { path: 'maint-departments', component: MaintDepartmentsComponent, data: { roles: ['Departments'] } },
            { path: 'maint-employeeNumbering', component: MainEmployeeNumberingComponent, data: { roles: ['EmployeeConfig'] } },
            { path: 'users', component: UsersComponent, data: { roles: ['Users'] } },
            { path: 'user-groups', component: UserGroupsComponent, data: { roles: ['UserGroups'] } },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
