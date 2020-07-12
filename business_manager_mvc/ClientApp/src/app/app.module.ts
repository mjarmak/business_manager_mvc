import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserAccountCreateComponent } from './user-account-create/user-account-create.component';
import { BusinessCreateComponent } from './business-create/business-create.component';
import { BusinessManagerService } from './services/business-manager-svc';
import { ToastrModule } from 'ngx-toastr';  
import { AlertService } from './services/alert-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        UserAccountCreateComponent,
        BusinessCreateComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'counter', component: CounterComponent },
        { path: 'fetch-data', component: FetchDataComponent },
        { path: 'user-account-create', component: UserAccountCreateComponent },
        { path: 'business-create', component: BusinessCreateComponent },
    ]),
    ToastrModule.forRoot({
        positionClass: 'bottom-right',
        closeButton: true
    }),
    BrowserAnimationsModule
  ],
    providers: [
        BusinessManagerService,
        AlertService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
