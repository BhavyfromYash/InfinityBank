import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { CustomerdashboardComponent } from './customerdashboard/customerdashboard.component';
import { ManagerdashboardComponent } from './managerdashboard/managerdashboard.component';
import { userGuard } from './guards/user.guard';
import { ApprovalListComponent } from './approval-list/approval-list.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { AboutComponent } from './about/about.component';
import { SupportComponent } from './support/support.component';
import { ContactComponent } from './contact/contact.component';
import { BankserviceComponent } from './bankservice/bankservice.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
  { path: "", redirectTo: "home", pathMatch: "full" },
  { path: "home", component: HomeComponent },
  { path: "register", component: RegisterComponent },
  { path: "login", component: LoginComponent },
  { path: "about", component: AboutComponent },
  { path: "bankservice", component: BankserviceComponent },
  { path: "contact", component: ContactComponent },
  { path: "support", component: SupportComponent },
  { path: "forgot-password", component: ForgotPasswordComponent },
  { path: "reset-password", component: ResetPasswordComponent },
  { path: "customerdashboard", component: CustomerdashboardComponent, canActivate: [userGuard] },
  { path: "managerdashboard", component: ManagerdashboardComponent, canActivate: [userGuard] },
  { path: "approval-list", component: ApprovalListComponent },
  { path: "customer-list", component: CustomerListComponent },
  { path: "**", component: PagenotfoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
