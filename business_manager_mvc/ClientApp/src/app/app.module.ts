import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { UserAccountCreateComponent } from './user-account-create/user-account-create.component';
import { BusinessCreateComponent } from './business-create/business-create.component';
import { BusinessManagerService } from './services/business-manager-svc';
import { ToastrModule } from 'ngx-toastr';  
import { AlertService } from './services/alert-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BusinessListComponent } from './business-list/business-list.component';
import { BusinessOverviewComponent } from './business-overview/business-overview.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { TokenInterceptor } from './services/token-interceptor';
import { AuthService } from './services/auth-service';
import { LoginComponent } from './login/login.component';
import { BusinessDetailComponent } from './business-detail/business-detail.component';
import { MatFormFieldModule, MatInputModule, MatSelectModule, MatOptionModule, MAT_FORM_FIELD_DEFAULT_OPTIONS, MatSortModule } from '@angular/material';
import { UserListComponent } from './user-list/user-list.component';
import { UserOverviewComponent } from './user-overview/user-overview.component';

@NgModule({
  declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        UserAccountCreateComponent,
        BusinessCreateComponent,
        BusinessListComponent,
        BusinessDetailComponent,
        BusinessOverviewComponent,
    LoginComponent,
    UserListComponent,
        UserOverviewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'user-account-create', component: UserAccountCreateComponent },
      { path: 'business-create', component: BusinessCreateComponent },
      { path: 'business-overview', component: BusinessOverviewComponent },
      { path: 'business-detail/:businessId', component: BusinessDetailComponent },
      { path: 'login', component: LoginComponent },
      { path: 'user-overview', component: UserOverviewComponent },
    ]),
    ToastrModule.forRoot({
        positionClass: 'bottom-right',
        closeButton: true
    }),
      BrowserAnimationsModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatFormFieldModule,
      MatOptionModule,
      MatInputModule,
      MatSelectModule,
      ReactiveFormsModule,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        },
        { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
        BusinessManagerService,
        AlertService,
        AuthService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
