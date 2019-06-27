import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'

import { AppComponent } from './app.component';
import { WebbetAppComponent } from './webbet-app/webbet-app.component';
import { WebbetOfferComponent } from './webbet-app/webbet-offer/webbet-offer.component';
import { WebbetTicketComponent } from './webbet-app/webbet-ticket/webbet-ticket.component';
import { WebbetAppService } from './shared/webbet-app.service';

@NgModule({
  declarations: [
    AppComponent,
    WebbetAppComponent,
    WebbetOfferComponent,
    WebbetTicketComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [WebbetAppService],
  bootstrap: [AppComponent]
})
export class AppModule { }
