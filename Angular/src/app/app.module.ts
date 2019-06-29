
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';


import { AppComponent } from './app.component';
import { WebbetAppComponent } from './webbet-app/webbet-app.component';
import { WebbetOfferComponent } from './webbet-app/webbet-offer/webbet-offer.component';
import { WebbetTicketComponent } from './webbet-app/webbet-ticket/webbet-ticket.component';
import { WebbetAppService } from './shared/webbet-app.service';
import { WebbetWalletComponent } from './webbet-app/webbet-wallet/webbet-wallet.component';
import { WebbetWalletDepositComponent } from './webbet-app/webbet-wallet/webbet-wallet-deposit.component';
import { MatButtonToggleModule } from '@angular/material';



@NgModule({
  declarations: [
    AppComponent,
    WebbetAppComponent,
    WebbetOfferComponent,
    WebbetTicketComponent,
    WebbetWalletComponent,
    WebbetWalletDepositComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatButtonToggleModule
  ],
  entryComponents:[WebbetWalletDepositComponent],
  providers: [WebbetAppService],
  bootstrap: [AppComponent]
})
export class AppModule { }
