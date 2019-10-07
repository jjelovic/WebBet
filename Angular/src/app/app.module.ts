
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WebbetAppComponent } from './webbet-app/webbet-app.component';
import { WebbetOfferComponent } from './webbet-app/webbet-offer/webbet-offer.component';
import { WebbetTicketComponent } from './webbet-app/webbet-ticket/webbet-ticket.component';
import { WebbetAppService } from './shared/webbet-app.service';
import { WebbetWalletComponent } from './webbet-app/webbet-wallet/webbet-wallet.component';
import { WebbetWalletDepositComponent } from './webbet-app/webbet-wallet/webbet-wallet-deposit.component';
import { MatButtonToggleModule } from '@angular/material';
import { WebbetTicketListComponent } from './webbet-app/webbet-ticket/webbet-ticket-list.component';
import { WebbetTicketPreviewComponent } from './webbet-app/webbet-ticket/webbet-ticket-preview.component';
import { WebbetUserComponent } from './webbet-app/webbet-user/webbet-user.component';
import { RegistrationComponent } from './webbet-app/webbet-user/registration/registration.component';
import { WebbetUserService } from './shared/webbet-user.service';
import { LoginComponent } from './webbet-app/webbet-user/login/login.component';
import { AuthInterceptor } from './auth/auth.interceptor';



@NgModule({
  declarations: [
    AppComponent,
    WebbetAppComponent,
    WebbetOfferComponent,
    WebbetTicketComponent,
    WebbetWalletComponent,
    WebbetWalletDepositComponent,
    WebbetTicketListComponent,
    WebbetTicketPreviewComponent,
    WebbetUserComponent,
    RegistrationComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatButtonToggleModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }),
    ReactiveFormsModule,
    AppRoutingModule
  ],
  entryComponents:[WebbetWalletDepositComponent, WebbetTicketListComponent],
  providers: [
    WebbetAppService, 
    WebbetUserService, 
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
