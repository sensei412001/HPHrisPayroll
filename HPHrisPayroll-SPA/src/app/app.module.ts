import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ClarityModule } from '@clr/angular';

import { DxDataGridModule } from 'devextreme-angular';

import { AppComponent } from './app.component';
import { JwtModule } from '@auth0/angular-jwt';
import { SampleCrudComponent } from './sample-crud/sample-crud.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { HeaderNavComponent } from './header-nav/header-nav.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { HomeComponent } from './home/home.component';
import { MaintCompanyComponent } from './maint-company/maint-company.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      SampleCrudComponent,
      HeaderComponent,
      HeaderNavComponent,
      SideNavComponent,
      HomeComponent,
      MaintCompanyComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      BrowserAnimationsModule,
      ClarityModule,
      RouterModule.forRoot(appRoutes),
      DxDataGridModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
