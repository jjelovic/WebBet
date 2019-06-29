import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WebbetAppComponent } from './webbet-app/webbet-app.component';


const routes: Routes = [
  // {path:'webbetApp',component:WebbetAppComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
