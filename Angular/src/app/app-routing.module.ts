import { WebbetUserComponent } from './webbet-app/webbet-user/webbet-user.component';
import { WebbetOfferComponent } from './webbet-app/webbet-offer/webbet-offer.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WebbetAppComponent } from './webbet-app/webbet-app.component';
import { RegistrationComponent } from './webbet-app/webbet-user/registration/registration.component';


const routes: Routes = [
  { path: '', redirectTo:'/user/registration', pathMatch: 'full' },
  { path: 'user', component: WebbetUserComponent,
    children:[
      { path: 'registration', component: RegistrationComponent }
    ] },
  { path: 'offer', component: WebbetOfferComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
