
import { AuthGuard } from './_guards/auth.guard';
import { AuthService } from './_services/auth.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ClarityModule } from '@clr/angular';
import { AppComponent } from './app.component';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';

import {
   DxDataGridModule, DxFormModule, DxPopoverModule, DxPopupModule,
   DxLoadIndicatorModule, DxLoadPanelModule, DxScrollViewModule,
   DxTabPanelModule
} from 'devextreme-angular';

import { SampleCrudComponent } from './sample-crud/sample-crud.component';
import { HeaderComponent } from './header/header.component';
import { HeaderNavComponent } from './header-nav/header-nav.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { HomeComponent } from './home/home.component';
import { MaintCompanyComponent } from './maint-company/maint-company.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { UserGroupsComponent } from './user-groups/user-groups.component';
import { UsersComponent } from './users/users.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      HasRoleDirective,
      SampleCrudComponent,
      HeaderComponent,
      HeaderNavComponent,
      SideNavComponent,
      HomeComponent,
      MaintCompanyComponent,
      LoginComponent,
      UserGroupsComponent,
      UsersComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      BrowserAnimationsModule,
      ClarityModule,
      DxDataGridModule,
      FormsModule,
      DxFormModule,
      DxPopupModule,
      DxLoadIndicatorModule,
      DxLoadPanelModule,
      DxScrollViewModule,
      DxTabPanelModule,
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
     }),
   ],
   providers: [
      AuthService,
      AuthGuard,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
