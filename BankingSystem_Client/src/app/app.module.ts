import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientJsonpModule } from '@angular/common/http';
import { GoogleMapsModule } from '@angular/google-maps';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CustomerDashboardComponent } from './customerdashboard/customerdashboard.component';
import { ManagerdashboardComponent } from './managerdashboard/managerdashboard.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { ApprovalListComponent } from './approval-list/approval-list.component';
import { ManagerInterceptor } from './interceptors/manager.interceptor';
import { AboutComponent } from './about/about.component';
import { SupportComponent } from './support/support.component';
import { ContactComponent } from './contact/contact.component';
import { BankserviceComponent } from './bankservice/bankservice.component';
import { MapComponent } from './map/map.component';
import { MainNavbarComponent } from './navbars/main-navbar/main-navbar.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SessionService } from './services/session.service';
import { SessionExpiredPopupComponent } from './sessionexpireddialog/sessionexpireddialog.component';
import { ApplyaccountComponent } from './applyaccount/applyaccount.component';
import { CustomerService } from './services/customer.service';
import { AccountDetailsComponent } from './account-details/account-details.component';
import {AccountService } from './services/account.service';
import { AccountSummaryComponent } from './account-summary/account-summary.component';
import { AccountSummaryService } from './services/accountsummary.service';
import { AccountStatementComponent } from './account-statement/account-statement.component';
import { AccountStatementService } from './services/accountstatement.service';
import { ViewBalanceComponent } from './view-balance/view-balance.component';
import { ViewbalanceService } from './services/viewbalance.service';
import { CreateCustomerComponent } from './create-customer/create-customer.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PagenotfoundComponent,
    RegisterComponent,
    LoginComponent,
    CustomerDashboardComponent,
    ManagerdashboardComponent,
    CustomerListComponent,
    ApprovalListComponent,
    AboutComponent,
    SupportComponent,
    ContactComponent,
    BankserviceComponent,
    MapComponent,
    MainNavbarComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
    SessionExpiredPopupComponent,
    ApplyaccountComponent,
    AccountDetailsComponent,
    AccountSummaryComponent,
    AccountStatementComponent,
    ViewBalanceComponent,
    CreateCustomerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    HttpClientJsonpModule,
    GoogleMapsModule,
    NgbModule
  ],
  providers: [SessionService, CustomerService,AccountService,AccountSummaryService,AccountStatementService,ViewbalanceService, {
    
    provide: HTTP_INTERCEPTORS,
    useClass: ManagerInterceptor,
    multi: true
  },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
