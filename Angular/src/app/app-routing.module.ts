import { WebbetOfferComponent } from './webbet-app/webbet-offer/webbet-offer.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WebbetAppComponent } from './webbet-app/webbet-app.component';


const routes: Routes = [
  {path:'offer',component:WebbetOfferComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
